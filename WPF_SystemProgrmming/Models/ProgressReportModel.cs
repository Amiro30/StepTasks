using System.Collections.Generic;
using System.IO;

namespace WPF_SystemProgramming.Models
{
    public class ProgressReportModel
    {
        public int PercentageComplete { get; set; } = 0;
        public IList<FileInfo> FileInfos { get; set; } = new List<FileInfo>();
    }
}
