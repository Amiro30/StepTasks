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
using WPF_SystemProgrmming;

namespace WPF_SystemProgramming
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

        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            
            //ProgresBar win2 = new ProgresBar();
            //win2.Show();
           


            //var words =  HandleInput(txtInput.Text);

            //string[] words = new[] {"visualfafa", "fghj"};

            //OperationsHandler operationsHandler = new OperationsHandler(words);

            //await operationsHandler.HandleAllOperations();


        }

        private string[] HandleInput(string input)
        {
            input = Regex.Replace(input, @"\s+", "");

            var words = input.Split(',');

            return words;
        }

    }
}
