using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WPF_SystemProgrmming
{
    public class ReportModel
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string FileName { get; set; }
        [JsonProperty]
        public string FilePath { get; set; }
        [JsonProperty]
        public long SizeInBytes { get; set; }
        [JsonProperty]
        public DateTime DateCreated { get; set; }

    }

    public class Additionaldata
    {
        [JsonProperty]
        public int Substitutions { get; set; }
        [JsonProperty]
        public List<ReportModel> ReportModels { get; set; }
    }
}
