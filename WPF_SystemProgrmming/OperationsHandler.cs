using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_SystemProgramming;

namespace WPF_SystemProgramming
{
    public class OperationsHandler
    {
        public OperationsHandler(string[] words)
        {
            FileOperator fileOperator = new FileOperator();
            int substitutions;

            words = new[] { "visualfafa", "fghj" };

            var filesInfo = fileOperator.GetFilesInfo("txt");
            var matchedFiles = fileOperator.FindWordsInFiles(words, filesInfo);

            fileOperator.CopyFilesWithWords(matchedFiles);

            fileOperator.OverwriteBadWordsWithAsteriks(words, matchedFiles, out substitutions);

            fileOperator.CreateReportFile(matchedFiles, substitutions);

        }

        //public async Task 

    }
}
