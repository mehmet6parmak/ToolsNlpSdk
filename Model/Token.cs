using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace ITU.Nlp.Tools.Model
{
    [DataContract]
    public class Token
    {
        [DataMember]
        public string RawForm { get; set; }

        [DataMember]
        public List<MorphologicalAnalysis> MorphologicalAnalyses { get; set; }

        internal static Token CreateFromMorphologicalAnalysis(string tokenAnalysis)
        {
            var analysisLines = tokenAnalysis.Split('\n');
            var token = new Token();
            token.RawForm = analysisLines[0];
            token.MorphologicalAnalyses = new List<MorphologicalAnalysis>();
            for (int i = 1; i < analysisLines.Length; i++)
            {
                token.MorphologicalAnalyses.Add(MorphologicalAnalysis.CreateFromString(analysisLines[i]));
            }
            return token;
        }
    }
}