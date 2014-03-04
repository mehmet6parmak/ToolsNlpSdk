using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;

namespace ITU.Nlp.Tools.Utilities
{
    public static class TurkishLanguageUtils
    {
        public static readonly CultureInfo Turkey = new CultureInfo("tr-TR");
        private static HashSet<string> _stopWords;

        public async static Task<HashSet<string>> StopWordsAsync()
        {
            if (_stopWords == null)
            {
                _stopWords = new HashSet<string>();
                IFile file = await FileSystem.Current.GetFileFromPathAsync("Data/stop_words.txt");
                var stream = await file.OpenAsync(FileAccess.Read);
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                        _stopWords.Add(line);
                }
            }
            return _stopWords;
        }

        public async static Task<bool> IsStopWord(string input)
        {
            return (await StopWordsAsync()).Contains(input);
        }

    }
}
