using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WPF_SystemProgrmming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            txtInput.Select(0,0);

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {

            FileOperator fileOperator = new FileOperator();

            //var words =  HandleInput(txtInput.Text);

            string[] words = new[] {"visualfafa", "fghj"};

            var filesInfo = fileOperator.GetFilesInfo("txt");

            var matchedFiles = fileOperator.GetMatchingFiles(words, filesInfo);


        }

        private string[] HandleInput(string input)
        {
            input = Regex.Replace(input, @"\s+", "");

            var words = input.Split(',');

            return words;
        }
    }
}
