using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_SystemProgrmming;

namespace WPF_SystemProgrmming
{
    public class OperationsHandler
    {
        public OperationsHandler(string[] words)
        {
            FileOperator fileOperator = new FileOperator();

            words = new[] { "visualfafa", "fghj" };

            var filesInfo = fileOperator.GetFilesInfo("txt");
            var matchedFiles = fileOperator.FindWordsInFiles(words, filesInfo);

            fileOperator.CopyFilesWithWords(matchedFiles);

            fileOperator.OverwriteBadWordsWithAsteriks(words, matchedFiles);

        }

        //public async Task 

    }
}
