using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WPF_SystemProgrmming;
using System.Threading.Tasks;


namespace WPF_SystemProgrmming
{
    class FileOperator
    {
        public void CreateReportFile()
        {

        }


        public void OverwriteBadWordsWithAsteriks(string[] searchwords, IList<FileInfo> files)
        {
            bool isWritten = false;
            string destFile = null;
            Directory.CreateDirectory(Constants.targetPath);

            foreach (var f in files)
            {
                foreach (var word in searchwords)
                {
                    if (!isWritten)
                    {
                        var fileText = GetFileText(f.FullName).Replace(word, "*******");
                        destFile = Path.Combine(Constants.targetPath, "rw_" + f.Name);

                        File.WriteAllText(destFile, fileText);
                        isWritten = true;
                    }
                    else
                    {
                        var fileText = GetFileText(destFile).Replace(word, "*******");

                        File.WriteAllText(destFile, fileText);
                        isWritten = false;
                    }

                    Debug.Write($"Overwritten files : {destFile}");
                }
            }
        }


        public void CopyFilesWithWords(IList<FileInfo> filesInfos)
        {
            string destFile = null;
            int countName = 0;

            // Create a new target folder.
            // If the directory already exists, this method does not create a new directory.
            Directory.CreateDirectory(Constants.targetPath);

            foreach (var file in filesInfos)
            {
                destFile = Path.Combine(Constants.targetPath, file.Name);

                if (File.Exists(destFile))
                {
                    var fName = Path.GetFileNameWithoutExtension(destFile);
                    var fExtension = Path.GetExtension(destFile);

                    File.Copy(file.FullName, Path.Combine(Constants.targetPath, fName + $"_{countName++}" + fExtension), true);
                }
                else
                {
                    File.Copy(file.FullName, destFile, false);
                }
            }
        }


        public IList<FileInfo> FindWordsInFiles(string[] searchwords, IList<FileInfo> files)
        {
            var matchedFiles = new List<FileInfo>();
            foreach (var word in searchwords)
            {
                var queryMatchingFiles =
                    from file in files
                    let fileText = GetFileText(file.FullName)
                    where fileText.Contains(word)
                    select file;

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

