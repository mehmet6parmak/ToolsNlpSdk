using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ITU.Nlp.Tools.Utilities;

namespace ITU.Nlp.Tools
{
    public class ItuNlpHttpClient : HttpClient
    {
        private const string BaseUrl = "http://tools.nlp.itu.edu.tr/SimpleApi";

        private const string NamedEntityRecognizer = "ner";
        private const string Normalizer = "normalize";
        private const string TurkishCheck = "isturkish";
        private const string Tokenizer = "tokenizer";
        private const string DeAsciifier = "deasciifier";
        private const string MorphAnalyzer = "morphanalyzer";
        private const string Vocalizer = "vocalizer";
        private const string DependencyParser = "depparser";
        private const string SpellChecker = "spellcheck";
        private const string Disambiguator = "disambiguator";
        private const string PipeLine = "pipeline";

        public static List<NlpTool> Tools = new List<NlpTool>
            {
                new NlpTool { Name = "NamedEntityRecognizer", Key = "ner"},
                new NlpTool { Name = "Normalizer", Key = "normalize"},
                new NlpTool { Name = "TurkishCheck", Key = "isturkish"},
                new NlpTool { Name = "Tokenizer", Key = "tokenizer"},
                new NlpTool { Name = "DeAsciifier", Key = "deasciifier"},
                new NlpTool { Name = "MorphAnalyzer", Key = "morphanalyzer"},
                new NlpTool { Name = "Vocalizer", Key = "vocalizer"},
                new NlpTool { Name = "DependencyParser", Key = "depparser"},
                new NlpTool { Name = "SpellChecker", Key = "spellcheck"},
                new NlpTool { Name = "Disambiguator", Key = "disambiguator"},
                new NlpTool { Name = "PipeLine", Key = "pipeline"}
            };

        private const string ParameterFormat = "&token={0}&input={1}";
        private const string GeneralUrl = BaseUrl + "?tool={0}&token={1}&input={2}";
        private const string NormalizerUrl = BaseUrl + "?tool=" + Normalizer + ParameterFormat;


        public string Token { get; private set; }

        public ItuNlpHttpClient(string token)
        {
            Token = token;
        }

        public async Task<string> Normalize(string input)
        {
            var url = string.Format(NormalizerUrl, Token, input);
            var result = await PostAsync(url, null);
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> Tokenize(string input)
        {
            return await RequestNlpTools(Tokenizer, input);
        }

        public async Task<string> MorphologicallyAnaylize(string input)
        {
            String result = String.Empty;
            if (!StringUtils.ContainsEmptySpace(input))
            {
                result = input + StringUtils.NewLineWithoutCr;
                result += await RequestNlpTools(MorphAnalyzer, input);
            }
            else
            {
                var tokenized = await RequestNlpTools(Tokenizer, input);
                var tokens = tokenized.Split(StringUtils.NewLineCharacters, StringSplitOptions.RemoveEmptyEntries);

                foreach (var token in tokens)
                {
                    result += await MorphologicallyAnaylize(token) + StringUtils.NewLineWithoutCr;
                }
            }
            return result;
        }

        public async Task<string> RequestNlpTools(string tool, string input)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("token", Token));
            parameters.Add(new KeyValuePair<string, string>("tool", tool));
            //var escapedInput = Uri.EscapeDataString(input);
            parameters.Add(new KeyValuePair<string, string>("input", input));
            HttpContent content = new FormUrlEncodedContent(parameters);
            var result = await PostAsync(BaseUrl, content);
            return await result.Content.ReadAsStringAsync();
        }

        public class NlpTool
        {
            public string Name { get; set; }
            public string Key { get; set; }
        }
    }
}
