using LAIR.Collections.Generic;
using LAIR.ResourceAPIs.WordNet;
using OpenNLP.Tools.PosTagger;
using OpenNLP.Tools.SentenceDetect;
using OpenNLP.Tools.Tokenize;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using RelevanceModel;
using System.Collections;
using System.Collections.Generic;

namespace Anti_Plagiarism
{
    class Reword
    {
        //detect apostrophes ('), slashes (/), emails and phone numbers

        private static char[] _punctuation = new char[11] { ',', '?', '!', '(', ')', '\"', '-', '[', ']', '{', '}' };

        private string _text = String.Empty;

        private WordNetEngine _wordNetEngine;

        private string[] _conjunctions = new string[] { "CC", "IN", "TO" };

        private string[] _nouns = new string[] { "NN", "NNP", "NNPS", "NNS" };
        private string[] _verbs = new string[] { "VB", "VBD", "VBG", "VBN", "VBP", "VBZ", };
        private string[] _adjectives = new string[] { "JJ", "JJR", "JJS" };
        private string[] _adverbs = new string[] { "RB", "RBR", "RBS", "NNS" };

        private const int NOUN = 0;
        private const int VERB = 1;
        private const int ADJECTIVE = 2;
        private const int ADVERB = 3;

        private ArticleChecker checker = new ArticleChecker();

        private RelevanceReceiver receiver = new RelevanceReceiver();

        private Parser parser = new Parser();

        private Composer composer = new Composer();

        private string[] _words;

        private string[] _sentences;

        public Reword()
        {
            _wordNetEngine = new WordNetEngine(@"C:\Users\Daniel\Google Drive\Programming\C#\Anti-Plagiarism\References\WordNetAPI-master\WordNetAPI-master\resources\", false);
        }

        private void Initialize()
        {
            _sentences = parser.SentenceDetect(_text);

            _words = parser.Tokenize(_text);
        }

        private bool IsWord(string word)
        {
            foreach(char c in word)
            {
                if (!char.IsLetter(c)) return false;
            }

            return true;
        }

        private void CheckNewLines(string text, string[] sentences)
        {
            if (text.Contains("\n"))
            {
                int start = 0;

                while (start < sentences.Length)
                {
                    int index = text.IndexOf('\n', start);

                    string newText = text.Substring(start, index);

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

                        if (currentCount == count)
                        {
                            temp.Insert(i, Environment.NewLine);
                            break;
                        }
                    }

                    if (start == index) start = sentences.Length;
                    else start = index;
                }
            }
        }

        private void CombineConjunctions(ref string[] words)
        {
            ArrayList listOfIndices = new ArrayList();

            for(int i = 0; i < words.Length; i++)
            {
                if(i > 0 && words[i].Contains('\'') && words[i].Length > 1)
                {
                    words[i - 1] += words[i];
                    listOfIndices.Add(i - listOfIndices.Count);
                }
            }

            List<string> temp = words.ToList();

            foreach(int index in listOfIndices)
            {
                temp.RemoveAt(index);
            }

            words = temp.ToArray();
        }

        public void ChangeText(string text)
        {
            Modification.Disable();
            ThreadPool.QueueUserWorkItem(GetNewText, text);
        }

        private void GetNewText(object o)
        {
            this._text = o.ToString();

            Initialize();

            CombineConjunctions(ref _words);

            double progressIncrementAmount = 100.0 / _sentences.Length;

            for(int i = 0; i < _sentences.Length; i++)
            {
                string[] words = parser.Tokenize(_sentences[i]);
                string[] pos = parser.Tag(words);

                CombineConjunctions(ref words);

                words = receiver.Change(words, _sentences.Length - 1);

                _sentences[i] = composer.ComposeSentence(words);
                Modification.Progress(progressIncrementAmount);
            }
            _text = composer.ComposeText(_text, _sentences);
            Modification.SetNewText(_text);
        }

        private void CheckArticle(int index)
        {
            if (index < 1) return;

            if (_words[index - 1].Equals("a", StringComparison.CurrentCultureIgnoreCase) || _words[index - 1].Equals("an", StringComparison.CurrentCultureIgnoreCase))
            {
                _words[index - 1] = checker.UseAOrAn(_words[index]);
            }
        }

        private bool IsAbbreviation(string nextWord)
        {
            if (nextWord.Contains("\'") || nextWord == "s" || nextWord == "nd" || nextWord == "st") return true;
            else return false;
        }
    }
}
