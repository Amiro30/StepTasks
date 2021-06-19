using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_SystemProgrmming
{
    class FileOperator
    {
        public IList<string> GetMatchingFiles(string[] searchwords, IList<FileInfo> files)
        {
            var matchedFiles = new List<string>();
            foreach (var word in searchwords)
            {
                var queryMatchingFiles =
                    from file in files
                    let fileText = GetFileText(file.FullName)
                    where fileText.Contains(word)
                    select file.FullName;

                matchedFiles.AddRange(queryMatchingFiles);
            }

            return matchedFiles;
        }

        public IList<FileInfo> GetFilesInfo(string fileExtension)
        {
            //search in all drives  and all directories to search for all .txt files
            List<FileInfo> filesInfos = new List<FileInfo>();

            foreach (DriveInfo drive in DriveInfo.GetDrives().Where(x => x.IsReady))
            {
                var dirs = drive.RootDirectory.GetDirectories();
                foreach (var dir in dirs)
                {
                    try
                    {
                        var fileList = dir.GetFiles("*." + $"{fileExtension}", System.IO.SearchOption.AllDirectories);
                        filesInfos.AddRange(fileList);
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        Debug.WriteLine($"Folder inaccessible due to permissions {e.Message}");
                    }
                }
            }
            return filesInfos;
        }

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

