using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ITU.Nlp.Tools.Model
{
    [DataContract]
    public class Sentence
    {
        [DataMember]
        public string RawSentence { get; protected set; }

        [DataMember]
        public List<Token> Tokens { get; protected set; }

        public Sentence(string rawSentence)
        {
            RawSentence = rawSentence;
        }

        protected Sentence()
        { }

        public static Sentence CreateFromMorphologicalAnalysis(string morphAnalysis)
        {
            if (string.IsNullOrEmpty(morphAnalysis))
                return null;
            morphAnalysis = morphAnalysis.Replace("\r\n", "\n");
            var tokens = morphAnalysis.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            var output = new Sentence();
            var rawSentenceBuilder = new StringBuilder();
            output.Tokens = new List<Token>();
            foreach (var tokenAnalysis in tokens)
            {
                var originalWord = tokenAnalysis.Split('\n')[0];
                rawSentenceBuilder.Append(originalWord + " ");
                Token token = Token.CreateFromMorphologicalAnalysis(tokenAnalysis);
                output.Tokens.Add(token);
            }
            output.RawSentence = rawSentenceBuilder.ToString().Trim();
            return output;
        }

        public static List<Sentence> CreateListFromMorphologicalAnalysis(string morphAnalysis)
        {
            var result = new List<Sentence>();
            var sentences = morphAnalysis.Split(new[] { "\r\n\r\n\r\n", "\n\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var sentence in sentences)
            {
                var sentenceObject = CreateFromMorphologicalAnalysis(sentence);
                result.Add(sentenceObject);
            }
            return result;
        }
    }
}