using System;
using System.Text;

namespace Anti_Plagiarism
{
    public class ArticleChecker
    {
        private string _vowels = "aeiouAEIOU";
        private string[] _silentHs = new string[] { "hour", "honour", "honest", "heir", "hours", "honoured", "honourable", "heirs" };
        private string[] _articleExceptions = new string[] { "union", "united", "used", "use", "unicorn" };

        public string UseAOrAn(string word)
        {
            if (IsException(word)) return "a";

            char firstLetter = word.Substring(0, 1).ToCharArray()[0];
            bool isVowel = _vowels.IndexOf(firstLetter) >= 0;

            if (isVowel) return "an";

            string lowerWord = ToLower(word);

            foreach (string silentH in _silentHs)
            {
                if (word.Equals(silentH, StringComparison.CurrentCultureIgnoreCase)) return "an";
            }

            return "a";
        }

        private bool IsException(string word)
        {
            foreach (string exception in _articleExceptions)
            {
                if (word.Equals(exception, StringComparison.CurrentCultureIgnoreCase)) return true;
            }

            return false;
        }

        public static string ToLower(string word)
        {
            StringBuilder newWord = new StringBuilder();

            for (int i = 0; i < word.Length; i++)
            {
                newWord.Append(char.ToLower(word[i]));
            }

            return newWord.ToString();
        }
    }
}
