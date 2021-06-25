using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;
using WPF_SystemProgramming.Common;
using WPF_SystemProgrmming;
using WPF_SystemProgrmming.Models;

namespace WPF_SystemProgramming
{
    public partial class MainWindow : Window
    {
        private CancellationTokenSource cts;
        public MainWindow()
        {
            InitializeComponent();

            txtInput.Select(0, 0);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            HandleFlow(txtInput.Text);
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.Filter = "TXT Files (*.txt)|*.txt";

            if (dialog.ShowDialog() == true)
            {
                var fileContent = File.ReadAllText(dialog.FileName);
                PrintResults(fileContent);

                HandleFlow(fileContent);
            }
        }

        private async void HandleFlow(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                PrintResults($"Yours input empty please re-do.");
            }
            else
            {
                cts = new CancellationTokenSource();
                Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
                progress.ProgressChanged += ReportProgress;

                var watch = System.Diagnostics.Stopwatch.StartNew();
                var _words = HandleInput(input);

                try
                {
                    FileOperator fileOperator = new FileOperator();
                    int substitutions;

                    PrintResults("\nGetting all files...");
                    var filesInfo = await fileOperator.GetFilesInfo("txt", progress, cts.Token).ConfigureAwait(false);


                    PrintResults("\nFinding words in files...");
                    var matchedFiles = await fileOperator.FindWordsInFiles(_words, filesInfo, cts.Token).ConfigureAwait(false);


                    PrintResults($"\nCopying founded files to {Constants.targetPath}...");
                    fileOperator.CopyFilesWithWords(matchedFiles);


                    PrintResults("\nOverwriting Bad Words to Asteriks ...");
                    fileOperator.OverwriteBadWordsWithAsteriks(_words, matchedFiles, out substitutions);


                    PrintResults("\nCreating report file...");
                    var reportFile = await fileOperator.CreateReportFile(matchedFiles, substitutions).ConfigureAwait(false);

                    PrintResults(reportFile.ToString());

                }
                catch (OperationCanceledException)
                {
                    PrintResults($"The async download was cancelled. {Environment.NewLine}");
                    cts.Dispose();
                }

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

                PrintResults($"Total execution time: {elapsedMs}");
            }
        }

        private string[] HandleInput(string input)
        {
            input = Regex.Replace(input, @"\s+", "").ToLower();

            var words = input.Split(',');

            return words;
        }

        private void ReportProgress(object sender, ProgressReportModel e)
        {
            dashboardProgress.Value = e.PercentageComplete;
        }

        private void PrintResults(string msg, IList<FileInfo> results = null)
        {
            if (results == null && msg != null)
            {
                resultsWindow.Dispatcher.Invoke(() => resultsWindow.Text += $"{msg} { Environment.NewLine}");
            }
            else
            {
                resultsWindow.Dispatcher.Invoke(() =>
                {
                    resultsWindow.Text += msg;
                    foreach (var item in results)
                    {
                        resultsWindow.Text +=
                            $"{item.DirectoryName} {Environment.NewLine}";
                    }
                });
            }
        }

        private void cancelOperation_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }
    }
}
