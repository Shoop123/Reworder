using OpenNLP.Tools.PosTagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelevanceModel
{
    public class Composer
    {
        private EnglishMaximumEntropyPosTagger posTagger = new EnglishMaximumEntropyPosTagger(StaticHelper.MODEL_PATH + "EnglishPOS.nbin");
        
        private string[] _noSpacePunctuation = new string[] { ",", ".", ":", "-RRB-" };

        private string[] _reverseSpacePunctuation = new string[] { "``", "-LRB-", "''", "$", "#" };

        internal string ComposeSentenceIgnorePuntuation(string[] words)
        {
            StringBuilder text = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                if (!char.IsPunctuation(words[i][0]))
                {
                    if (i > 0)
                        text.Append(" " + words[i]);
                    else
                        text.Append(words[i]);
                }
            }

            string newText = text.ToString().Trim();
            return newText;
        }

        public string ComposeSentence(string[] words)
        {
            StringBuilder text = new StringBuilder();
            bool hasSpace = true;

            for (int i = 0; i < words.Length; i++)
            {
                if (IsNoSpacePunctuation(words[i])) text.Append(words[i]);
                else if (IsReverseSpacePunctuation(words[i]))
                {
                    hasSpace = false;
                    text.Append(" " + words[i]);
                }
                else if (i == 0)
                {
                    text.Append(StaticHelper.FirstLetterToUpper(words[i]));
                }
                else
                {
                    if (words[i - 1] == ".") words[i] = StaticHelper.FirstLetterToUpper(words[i]);

                    if (hasSpace) text.Append(" " + words[i]);
                    else text.Append(words[i]);
                    hasSpace = true;
                }
            }

            string newText = text.ToString().Trim();
            return newText;
        }

        private bool IsReverseSpacePunctuation(string word)
        {
            if (word.Length > 1 || !char.IsPunctuation(word[0])) return false;

            string pos = posTagger.Tag(new string[] { word })[0];
            foreach (string punctuation in _reverseSpacePunctuation)
            {
                if (pos == punctuation) return true;
            }

            return false;
        }

        private bool IsNoSpacePunctuation(string word)
        {
            if (word.Length > 1 || !char.IsPunctuation(word[0])) return false;

            string pos = posTagger.Tag(new string[] { word.Trim() })[0];
            foreach (string punctuation in _noSpacePunctuation)
            {
                if (pos == punctuation) return true;
            }

            return false;
        }

        public string ComposeText(string originalText, string[] sentences)
        {
            CheckNewLines(originalText, ref sentences);

            StringBuilder newText = new StringBuilder();

            for (int i = 0; i < sentences.Length; i++)
            {
                if (sentences[i].Contains(Environment.NewLine)) newText.Append(sentences[i]);
                else
                {
                    if (i + 1 < sentences.Length && sentences[i + 1].Contains(Environment.NewLine))
                        newText.Append(sentences[i]);
                    else 
                        newText.Append(sentences[i] + " ");
                }
            }

            return newText.ToString().Trim();
        }

        private int AmountOfConsecutiveNewLines(string text, int startIndex)
        {
            int amount = 0;

            for(int i = startIndex; i < text.Length; i+=Environment.NewLine.Length)
            {
                string part = text.Substring(i, Environment.NewLine.Length);
                if (part == Environment.NewLine) amount++;
                else break;
            }

            return amount;
        }

        private void CheckNewLines(string text, ref string[] sentences)
        {
            if (text.Contains(Environment.NewLine))
            {
                int start = 0;
                int lastIndex = 0;

                while (start < sentences.Length)
                {
                    int index = text.IndexOf(Environment.NewLine, lastIndex);

                    if (index < 0) break;

                    string newText = text.Substring(lastIndex, index - lastIndex);

                    int amountToAdd = AmountOfConsecutiveNewLines(text, index);
                    lastIndex = index + (Environment.NewLine.Length * amountToAdd);

                    int count = 0;

                    foreach (char c in newText) if (c == '.') count++;

                    List<string> temp = sentences.ToList();

                    int currentCount = 0;

                    for (int i = start; i < temp.Count; i++)
                    {
                        if (sentences[i].Contains("."))
                            foreach (char c in sentences[i]) 
                                if (c == '.') 
                                    currentCount++;

                        start = i;

                        if (currentCount == count && count != 0)
                        {
                            string newLines = "";
                            for (int j = 0; j < amountToAdd; j++) newLines += Environment.NewLine;
                            temp.Insert(i + 1, newLines);
                            start = i + 1;
                            break;
                        }
                    }

                    sentences = temp.ToArray();
                }
            }
        }
    }
}
