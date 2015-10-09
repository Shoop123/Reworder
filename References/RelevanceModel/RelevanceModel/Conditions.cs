using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelevanceModel
{
    abstract class Conditions
    {
        private static readonly string[] MODELS = new string[] { "/ORGANIZATION", "/LOCATION", "/PERSON" };

        public static bool IsInBrackets(string[] words, int index)
        {
            if (words.Contains("(") || words.Contains("[") || words.Contains("{"))
            {
                int indexOfFirstBracket = Array.IndexOf(words, "(");
                int indexOfSecondBacket = Array.IndexOf(words, ")", indexOfFirstBracket);

                if (index > indexOfFirstBracket && index < indexOfSecondBacket) return true;

                indexOfFirstBracket = Array.IndexOf(words, "[");
                indexOfSecondBacket = Array.IndexOf(words, "]", indexOfFirstBracket);

                if (index > indexOfFirstBracket && index < indexOfSecondBacket) return true;

                indexOfFirstBracket = Array.IndexOf(words, "{");
                indexOfSecondBacket = Array.IndexOf(words, "}", indexOfFirstBracket);

                if (index > indexOfFirstBracket && index < indexOfSecondBacket) return true;
            }

            return false;
        }

        public static bool IsInQuotes(string[] words, int index)
        {
            if (words.Contains("\'") || words.Contains("\""))
            {
                int indexOfFirstQuote = Array.IndexOf(words, "\"");
                int indexOfSecondQuote = Array.IndexOf(words, "\"", indexOfFirstQuote);

                if (index > indexOfFirstQuote && index < indexOfSecondQuote) return true;

                indexOfFirstQuote = Array.IndexOf(words, "\'");
                if (indexOfFirstQuote < 0) return false;
                indexOfSecondQuote = Array.IndexOf(words, "\'", indexOfFirstQuote);

                if (index > indexOfFirstQuote && index < indexOfSecondQuote) return true;
            }

            return false;
        }

        public static bool CanModify(string[] words, int index)
        {
            string originalWord = words[index];

            string word = StaticHelper.FirstLetterToUpper(originalWord);

            string sentence = StaticHelper.composer.ComposeSentence(words);

            string formattedSentence = StaticHelper.classifier.classifyToString(sentence);

            if (formattedSentence.Contains(word + MODELS[0]) || formattedSentence.Contains(word + MODELS[1]) || formattedSentence.Contains(word + MODELS[2]) ||
                Conditions.IsInQuotes(words, index) || Conditions.IsInBrackets(words, index))
            {
                words[index] = originalWord;
                return false;
            }
            else
            {
                words[index] = originalWord;
                return true;
            }
        }
    }
}
