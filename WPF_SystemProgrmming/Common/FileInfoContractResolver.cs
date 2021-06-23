using System;
using System.IO;
using Newtonsoft.Json.Serialization;

namespace WPF_SystemProgrmming.Common
{
    public class FileInfoContractResolver : DefaultContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            if (objectType == typeof(FileInfo))
            {
                return CreateISerializableContract(objectType);
            }

            var contract = base.CreateContract(objectType);
            return contract;
        }
    }
}
