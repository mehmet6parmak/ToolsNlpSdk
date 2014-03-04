using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ITU.Nlp.Tools.Model;
using PCLStorage;

namespace ITU.Nlp.Tools.Utilities
{
    public static class GeneralUtility
    {
        public static async Task<List<Sentence>> LoadFromFile(string fileName, Encoding encoding = null)
        {
            IFile file = await FileSystem.Current.GetFileFromPathAsync(fileName);
            Stream stream = await file.OpenAsync(FileAccess.Read);
            string fileContent;
            using (var reader = new StreamReader(stream, encoding ?? Encoding.UTF8))
            {
                fileContent = await reader.ReadToEndAsync();
            }
            return Sentence.CreateListFromMorphologicalAnalysis(fileContent);
        }
    }
}