using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_SystemProgramming;

namespace WPF_SystemProgrmming
{
    /// <summary>
    /// Interaction logic for ProgresBar.xaml
    /// </summary>
    public partial class ProgresBar : Window//, INotifyPropertyChanged
    {
        //private BackgroundWorker _bgWorker = new BackgroundWorker();
        //private int _workerState;
        //public int WorkerState
        //{
        //    get { return _workerState; }
        //    set
        //    {
        //        _workerState = value;
        //        if (PropertyChanged != null)
        //            PropertyChanged(this, new PropertyChangedEventArgs("WorkerState"));
        //    }
        //}


        public ProgresBar()
        {
            InitializeComponent();

            //DataContext = this;

            //_bgWorker.DoWork += (s, e) =>
            //{
            //    for (int i = 0; i <= 100; i++)
            //    {
            //        System.Threading.Thread.Sleep(100);
            //        WorkerState = i;
            //    }

            //    MessageBox.Show("Work is done");
            //};
            //_bgWorker.RunWorkerAsync();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.DoWork += worker_DoWork;
            bgWorker.ProgressChanged += worker_ProgressChanged;
            bgWorker.RunWorkerCompleted += bw_RunWorkerCompleted;

            bgWorker.RunWorkerAsync();
        }

        private async void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] words = new[] { "visualfafa", "fghj" };
            //for (int i = 0; i < 100; i++)
            //{
            //    (sender as BackgroundWorker).ReportProgress(i);
            //    Thread.Sleep(100);
            //}
            for (int i = 1; i <= 100; i++)
            {
                
                (sender as BackgroundWorker).ReportProgress(i);
            }

        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }

        static void bw_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                Console.WriteLine("You canceled!");
            else if (e.Error != null)
                Console.WriteLine("Worker exception: " + e.Error.ToString());
            else
                Console.WriteLine("Complete: " + e.Result);      // from DoWork
        }


        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
