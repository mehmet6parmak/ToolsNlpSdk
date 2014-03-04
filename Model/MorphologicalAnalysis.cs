using System;
using System.Runtime.Serialization;
namespace ITU.Nlp.Tools.Model
{
    [DataContract]
    public class MorphologicalAnalysis
    {
        [DataMember]
        public string RawAnalysis { get; set; }

        public string Stem
        {
            get { return RawAnalysis.Split('+')[0]; }
        }

        public PosTag PosTag
        {
            get
            {
                var splitResult = RawAnalysis.Split(new [] { '+', '^' }, StringSplitOptions.RemoveEmptyEntries);
                if (splitResult.Length > 1)
                {
                    var postag = splitResult[1];
                    switch (postag)
                    {
                        case "noun" : 
                            return PosTag.Noun;
                        case "adj":
                            return PosTag.Adjective;
                        case "verb":
                            return PosTag.Verb;
                        case "?":
                            return PosTag.UnKnown;
                        case "guess":
                            return PosTag.Guess;
                        case "adverb":
                            return PosTag.AdVerb;
                        case "postp":
                            return PosTag.PostP;
                        case "pron":
                            return PosTag.ProNoun;
                        case "det":
                            return PosTag.Determinant;
                        case "num":
                            return PosTag.Number;
                        case "conj":
                            return PosTag.Conjunction;
                        case "ınterj":
                            return PosTag.Interj;
                        case "ques":
                            return PosTag.Question;
                        case "loc":
                            return PosTag.Location;
                        case "dup":
                            return PosTag.Duplication;
                    }
                }
                return PosTag.UnKnown;
            }
        }

        public static MorphologicalAnalysis CreateFromString(string rawAnalysis)
        {
            var morphologicalAnalysis = new MorphologicalAnalysis();
            morphologicalAnalysis.RawAnalysis = rawAnalysis;
            return morphologicalAnalysis;
        }

        public override int GetHashCode()
        {
            return RawAnalysis.GetHashCode();
        }

        public override string ToString()
        {
            return RawAnalysis;
        }

        public override bool Equals(object obj)
        {
            var analysis = obj as MorphologicalAnalysis;
            if (analysis == null)
                return false;
            return analysis.RawAnalysis == RawAnalysis;
        }
    }

    public enum PosTag
    {
        Noun = 1,
        Verb = 2,
        Adjective = 4,
        Number = 8,
        Determinant = 16,
        ProNoun = 32,
        PostP = 64,
        AdVerb = 128,
        Conjunction = 256,
        Interj = 512,
        Question = 1024,
        Location = 2048,
        Duplication = 4096,
        Guess = 32768,
        UnKnown = 65536,
        
    }
}
