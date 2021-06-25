using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_SystemProgrmming.Models
{
    public class ProgressReportModel
    {
        public int PercentageComplete { get; set; } = 0;
        public IList<FileInfo> FileInfos { get; set; } = new List<FileInfo>();
    }
}
