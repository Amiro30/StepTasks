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
using WPF_SystemProgramming.Common;
using WPF_SystemProgrmming;
using WPF_SystemProgrmming.Models;

namespace WPF_SystemProgramming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource cts;
        public MainWindow()
        {
            InitializeComponent();

            txtInput.Select(0, 0);

        }

        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;
            
            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {

                var _words = HandleInput(txtInput.Text);

                //string[] _words = new[] { "visualfafa", "fghj" };
                FileOperator fileOperator = new FileOperator();
                int substitutions;

                PrintResults(null, "\nGetting all files...");
                var filesInfo = await fileOperator.GetFilesInfo("txt", progress, cts.Token).ConfigureAwait(false);


                PrintResults(null, "\nFinding words in files...");
                var matchedFiles = await fileOperator.FindWordsInFiles(_words, filesInfo).ConfigureAwait(false);


                PrintResults(null, $"\nCopying founded files to {Constants.targetPath}...");
                fileOperator.CopyFilesWithWords(matchedFiles);

                PrintResults(null, "\nOverwriting Bad Words to Asteriks ...");
                fileOperator.OverwriteBadWordsWithAsteriks(_words, matchedFiles, out substitutions);


                PrintResults(null, "\nCreating report file...");
                var reportFile = await fileOperator.CreateReportFile(matchedFiles, substitutions).ConfigureAwait(false);

                PrintResults(null, reportFile.ToString());

            }
            catch (OperationCanceledException)
            {
                PrintResults(null, $"The async download was cancelled. {Environment.NewLine}");
                cts.Dispose();
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            PrintResults(null, $"Total execution time: {elapsedMs}");

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

        private void PrintResults(IList<FileInfo> results, string msg)
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
        
        private void pause_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
