using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;

namespace WPF_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            Button b = (Button)sender;
            
            var icon = (PackIconKind)Enum
                .Parse (typeof(PackIconKind), $"{(b.Content as PackIcon).Kind}");
            var buttonContent = PackIconConverter.GetIconValue(icon);

            txtInput.Text += buttonContent;
         }

        private void Result_click(object sender, RoutedEventArgs e)
        {
            try
            {
                result();
                
            }
            catch (Exception exc)
            {
                txtInput.Text = "Error!";
            }
        }

        private void result()
        {
            String op;
            int iOp = 0;
            if (txtInput.Text.Contains("+"))
            {
                iOp = txtInput.Text.IndexOf("+");
            }
            else if (txtInput.Text.Contains("-"))
            {
                iOp = txtInput.Text.IndexOf("-");
            }
            else if (txtInput.Text.Contains("*"))
            {
                iOp = txtInput.Text.IndexOf("*");
            }
            else if (txtInput.Text.Contains("/"))
            {
                iOp = txtInput.Text.IndexOf("/");
            }
            else
            {
                //error    
            }

            op = txtInput.Text.Substring(iOp, 1);
            double op1 = Convert.ToDouble(txtInput.Text.Substring(0, iOp));
            double op2 = Convert.ToDouble(txtInput.Text.Substring(iOp + 1, txtInput.Text.Length - iOp - 1));

            double result = 0;
            if (op == "+")
            {
                result = op1 + op2;
            }
            else if (op == "-")
            {
                result = op1 - op2;
            }
            else if (op == "*")
            {
                result = op1 * op2;
            }
            else
            {
                result = op1 / op2;
            }
            previousOp.Text = txtInput.Text;
            resultOp.Text += "" + result;
            txtInput.Text = "";
        }

        private void Off_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Text = "";
        }

        private void R_Click(object sender, RoutedEventArgs e)
        {
            if (txtInput.Text.Length > 0)
            {
                txtInput.Text = txtInput.Text.Substring(0, txtInput.Text.Length - 1);
            }
        }

        private void Del_All_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Text = "";
            resultOp.Text = "=";
            previousOp.Text = "";
        }
    }
}
