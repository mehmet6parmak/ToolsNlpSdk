using System;

namespace ITU.Nlp.Tools.Utilities
{
    public static class StringUtils
    {
        public static readonly string[] NewLineCharacters = new[] {"\r\n", "\n"};
        public static readonly string NewLineWithoutCr = "\n";
        public static readonly string NewLineWithCr = "\r\n";

        public static Boolean ContainsEmptySpace(string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            input = input.Trim();
            foreach (char character in input)
            {
                if (Char.IsWhiteSpace(character))
                {
                    return true;
                }
            }
            return false;
        }
    }
}