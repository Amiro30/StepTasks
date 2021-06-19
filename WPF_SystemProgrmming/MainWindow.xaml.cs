using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            var words = txtInput.Text.Split(' ');

            var fileList = GetFiles();

            string searchTerm = @"Visual";

            // Search the contents of each file.  
            // A regular expression created with the RegEx class  
            // could be used instead of the Contains method.  
            // queryMatchingFiles is an IEnumerable<string>.  
            var queryMatchingFiles =
                from file in fileList
                //where file.Extension == ".htm"
                let fileText = GetFileText(file)
                where fileText.Contains(searchTerm)
                select file;

            // Execute the query.  
            Debug.WriteLine("\nThe term \"{0}\" was found in:", searchTerm);
            foreach (string filename in queryMatchingFiles)
            {
                Debug.WriteLine(filename);
            }



        }

        private List<string> GetFiles()
        {
            //in all drives to search for all .txt files
            var files = new List<string>();


            foreach (DriveInfo drive in DriveInfo.GetDrives().Where(x => x.IsReady))
            {

                try
                {
                    var dirs = drive.RootDirectory.GetDirectories();
                    foreach (var dir in dirs)
                    {
                        files.AddRange(dir.GetFiles("*.txt").Select(x=>x.FullName));
                    }
                    
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine($"something gone wrong {e.Message}");
                }

            }

            return files;
        }


        // Read the contents of the file.  
        private string GetFileText(string name)
        {
            string fileContents = String.Empty;

            // If the file has been deleted since we took
            // the snapshot, ignore it and return the empty string.  
            if (System.IO.File.Exists(name))
            {
                fileContents = System.IO.File.ReadAllText(name);
            }
            return fileContents;
        }
    }
    
}
