using LAIR.ResourceAPIs.WordNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wnlib;
using WordsMatching;

namespace RelevanceModel
{
    public class RelevanceReceiver
    {
        private const double THRESHOLD = 0.8;

        private ContextualReferenceModel crm = new ContextualReferenceModel();
        private SimilarityModel sm = new SimilarityModel();

        private WordNetEngine wne = new WordNetEngine(@"C:\Users\danie\Documents\Visual Studio 2015\Projects\Anti-Plagiarism\References\WordNetAPI-master\WordNetAPI-master\resources\", false);

        private List<string> wordsChanged = new List<string>();

        private int calls = 0;

        private List<ChangedWord> changedWords = new List<ChangedWord>();

        private string previousText = String.Empty;

        private int FindIndex(string[] words, bool findLast, List<int> cantChange, List<string> wordsChanged)
        {
            List<string> tempWordsChanged = wordsChanged;

            string wordToRemove = String.Empty;

            WordNetEngine.POS[] pos = { WordNetEngine.POS.Noun, WordNetEngine.POS.Verb };

            if(!findLast)
            {
                for (int i = 0; i < words.Length; i++)
                {
                    if (cantChange.Contains(i) || tempWordsChanged.Contains(words[i]))
                    {
                        wordToRemove = words[i];
                        continue;
                    }

                    foreach (ChangedWord temp in changedWords)
                    {
                        if (temp.index == i && temp.word == words[i]) continue;
                    }

                    WordNetEngine.POS wnepos = StaticHelper.GetWordNetEnginePOS(words[i]);

                    if (Conditions.CanModify(words, i) && wnepos == pos[0] || wnepos == pos[1]) return i;

                    if(i == words.Length - 1)
                    {
                        if (tempWordsChanged.Contains(wordToRemove))
                        {
                            tempWordsChanged.Remove(wordToRemove);

                            if (tempWordsChanged.Count == 0)
                            {
                                pos[0] = WordNetEngine.POS.Adjective;
                                pos[1] = WordNetEngine.POS.Adverb;
                            }

                            i = -1;

                            wordToRemove = String.Empty;
                        }
                        else if (tempWordsChanged.Count > 0 || !tempWordsChanged.Contains(wordToRemove)) break;
                    }
                }
            }
            else
            {
                for(int i = words.Length - 1; i >= 0; i--)
                {
                    if (cantChange.Contains(i) || tempWordsChanged.Contains(words[i]))
                    {
                        wordToRemove = words[i];
                        continue;
                    }

                    foreach(ChangedWord temp in changedWords)
                    {
                        if (temp.index == i && temp.word == words[i]) continue;
                    }

                    WordNetEngine.POS wnepos = StaticHelper.GetWordNetEnginePOS(words[i]);

                    if (Conditions.CanModify(words, i) && wnepos == pos[0] || wnepos == pos[1]) return i;

                    if (i == 0)
                    {
                        if (tempWordsChanged.Contains(wordToRemove))
                        {
                            tempWordsChanged.Remove(wordToRemove);

                            if (tempWordsChanged.Count == 0)
                            {
                                pos[0] = WordNetEngine.POS.Adjective;
                                pos[1] = WordNetEngine.POS.Adverb;
                            }

                            i = words.Length;

                            wordToRemove = String.Empty;
                        }
                        else if (tempWordsChanged.Count > 0 || !tempWordsChanged.Contains(wordToRemove)) break;
                    }
                }
            }

            return -1;
        }

        public string GetNewText(string text)
        {
            Parser parser = new Parser();

            Composer composer = new Composer();

            string[] sentences = parser.SentenceDetect(text);
            string[] words = parser.Tokenize(text);

            CombineConjunctions(ref words);

            if(words.Length == 1) return Replace(words, 0);

            for (int i = 0; i < sentences.Length; i++)
            {
                string[] currentWords = parser.Tokenize(sentences[i]);
                string[] pos = parser.Tag(currentWords);

                CombineConjunctions(ref currentWords);

                words = Change(currentWords, sentences.Length - 1, text);

                sentences[i] = composer.ComposeSentence(words);
            }
            
            return composer.ComposeText(text, sentences);
        }

        private void CombineConjunctions(ref string[] words)
        {
            ArrayList listOfIndices = new ArrayList();

            for (int i = 0; i < words.Length; i++)
            {
                if (i > 0 && words[i].Contains('\'') && words[i].Length > 1)
                {
                    words[i - 1] += words[i];
                    listOfIndices.Add(i - listOfIndices.Count);
                }
            }

            List<string> temp = words.ToList();

            foreach (int index in listOfIndices)
            {
                temp.RemoveAt(index);
            }

            words = temp.ToArray();
        }

        private int TotalModifyable(string[] words)
        {
            int counter = 0;
            
            for(int i = 0; i < words.Length; i++)
            {
                WordNetEngine.POS wnepos = StaticHelper.GetWordNetEnginePOS(words[i]);

                if ((wnepos == WordNetEngine.POS.Verb || wnepos == WordNetEngine.POS.Noun) && Conditions.CanModify(words, i)) counter++;
            }

            return counter;
        }

        private string[] Change(string[] words, int amountOfCalls, string text)
        {
            List<int> cantChange = new List<int>();

            int totalModifyable = TotalModifyable(words);

            bool findLast = false;

            for(int i = 0; i < 2; i++)
            {
                if (i == 0) findLast = false;
                else findLast = true;

                int index = FindIndex(words, findLast, cantChange, wordsChanged);
                cantChange.Add(index);

                if (index == -1 || cantChange.Count >= (words.Length - totalModifyable)) break;
                string newWord = Replace(words, index);

                if (words[index] == newWord) i--;
                else
                {
                    wordsChanged.Add(words[index]);
                    changedWords.Add(new ChangedWord(words[index], index));
                    words[index] = newWord;
                }
            }

            calls++;

            if(calls >= amountOfCalls)
            {
                if (previousText != text)
                {
                    changedWords.Clear();
                    previousText = text;
                }

                calls = 0;
                wordsChanged.Clear();
            }

            return words;
        }

        private string Replace(string[] words, int index)
        {
            string word = words[index];

            WordNetEngine.POS wnepos = StaticHelper.GetWordNetEnginePOS(word);

            if (wnepos == WordNetEngine.POS.None) return word;

            PartsOfSpeech wnlibpos = StaticHelper.GetWnlibPOSFromWordNetEnginePOS(wnepos);

            LAIR.ResourceAPIs.WordNet.SynSet[] synsets = wne.GetSynSets(word, wnepos).ToArray();

            double currentSimilarity = THRESHOLD;
            List<string> possibleWords = new List<string>();

            string sentence = StaticHelper.composer.ComposeSentence(words);

            string[] tempWords = words;

            foreach (LAIR.ResourceAPIs.WordNet.SynSet synset in synsets)
            {
                foreach(string possibleWord in synset.Words)
                {
                    if (word.Equals(possibleWord, StringComparison.CurrentCultureIgnoreCase)) continue;

                    tempWords[index] = StaticHelper.WithoutUnderScore(possibleWord);
                    string newSentence = StaticHelper.composer.ComposeSentence(tempWords);

                    SentenceSimilarity ss = new SentenceSimilarity();

                    double similarity = ss.GetScore(sentence, newSentence);

                    if(similarity >= currentSimilarity)
                    {
                        possibleWords.Add(StaticHelper.WithoutUnderScore(possibleWord));
                    }
                }
            }

            words[index] = word;

            if (possibleWords.Count > 1)
            {
                WordSimilarity ws = new WordSimilarity();

                HierarchicalWordData defaultWord = new HierarchicalWordData(new MyWordInfo(word, StaticHelper.GetWnlibPOS(word)));

                currentSimilarity = 0;
                string replacement = word;

                foreach(string possibleWord in possibleWords)
                {
                    HierarchicalWordData newWord = new HierarchicalWordData(new MyWordInfo(possibleWord, wnlibpos));

                    double similarity = ws.GetSimilarity(defaultWord, newWord);

                    if(similarity > currentSimilarity)
                    {
                        replacement = possibleWord;
                        currentSimilarity = similarity;
                    }
                }

                return replacement;
            }
            else if (possibleWords.Count == 1) return possibleWords.ToArray()[0];
            else
            {
                return word;
            }
        }

        private bool CheckReplacementForTwo(string word1, string word2)
        {
            string words = word1 + " " + word2;
            WordNetEngine.POS wnepos = StaticHelper.GetWordNetEnginePOS(words);

            if (wnepos == WordNetEngine.POS.None) return false;

            return true;
        }

    }
}
