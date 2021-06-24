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
        private string[] _words;

        public OperationsHandler(string[] words)
        {
            _words = words;
        }

        public async Task HandleAllOperations()
        {
            FileOperator fileOperator = new FileOperator();
            int substitutions;

            _words = new[] { "visualfafa", "fghj" };

            //var filesInfo = fileOperator.GetFilesInfo("txt");

            //var matchedFiles = fileOperator.FindWordsInFiles(words, filesInfo);

            //fileOperator.CopyFilesWithWords(matchedFiles);

            //fileOperator.OverwriteBadWordsWithAsteriks(words, matchedFiles, out substitutions);

            //fileOperator.CreateReportFile(matchedFiles, substitutions);


            var filesInfo = await fileOperator.GetFilesInfo("txt").ConfigureAwait(false);

            var matchedFiles = await fileOperator.FindWordsInFiles(_words, filesInfo).ConfigureAwait(false);

            fileOperator.CopyFilesWithWords(matchedFiles);

            fileOperator.OverwriteBadWordsWithAsteriks(_words, matchedFiles, out substitutions);

            await fileOperator.CreateReportFile(matchedFiles, substitutions).ConfigureAwait(false);
        }

    }
}
