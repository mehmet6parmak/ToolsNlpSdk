using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ITU.Nlp.Tools.Model;
using Newtonsoft.Json;
using PCLStorage;

namespace ITU.Nlp.Tools.Utilities
{
    public static class JsonUtility
    {
        public static async Task<List<Sentence>> LoadFromJsonFile(string fileName, Encoding encoding = null)
        {
            IFile file = await FileSystem.Current.GetFileFromPathAsync(fileName);
            Stream stream = await file.OpenAsync(FileAccess.Read);
            string fileContent;
            using (var reader = new StreamReader(stream, encoding ?? Encoding.UTF8))
            {
                fileContent = await reader.ReadToEndAsync();
            }
            var result = new List<Sentence>();
            if (!String.IsNullOrEmpty(fileContent))
                result = JsonConvert.DeserializeObject<List<Sentence>>(fileContent);
            return result;
        }
    }
}