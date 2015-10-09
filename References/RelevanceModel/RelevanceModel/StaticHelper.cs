using edu.stanford.nlp.ie.crf;
using LAIR.ResourceAPIs.WordNet;
using OpenNLP.Tools.PosTagger;
using System;
using System.Linq;
using System.Text;
using Wnlib;

namespace RelevanceModel
{
    public class StaticHelper
    {
        private static EnglishMaximumEntropyPosTagger posTagger = new EnglishMaximumEntropyPosTagger(StaticHelper.MODEL_PATH + "EnglishPOS.nbin");

        public const string MODEL_PATH = @"C:\Users\danie\Documents\Visual Studio 2015\Projects\Anti-Plagiarism\References\Models\";

        public static WordNetEngine wordNetEngine = new WordNetEngine(@"C:\Users\danie\Documents\Visual Studio 2015\Projects\Anti-Plagiarism\References\WordNetAPI-master\WordNetAPI-master\resources\", false);

        public static CRFClassifier classifier = CRFClassifier.getClassifierNoExceptions(@"C:\Users\danie\Documents\Visual Studio 2015\Projects\Anti-Plagiarism\References\english.all.3class.distsim.crf.ser.gz");

        public static Composer composer = new Composer();

        private const int NONE = -1;
        private const int NOUN = 0;
        private const int VERB = 1;
        private const int ADJECTIVE = 2;
        private const int ADVERB = 3;

        private static readonly string[] NOUNS = { "NN", "NNP", "NNPS", "NNS" };
        private static readonly string[] VERBS = { "VB", "VBD", "VBG", "VBN", "VBP", "VBZ", };
        private static readonly string[] ADJECTIVES = { "JJ", "JJR", "JJS" };
        private static readonly string[] ADVERBS = { "RB", "RBR", "RBS", "NNS" };

        private static readonly string[] CONJUNCTIONS = { "CC", "IN", "TO" };

        public static WordNetEngine.POS GetWordNetEnginePOS(string word)
        {
            int partOfSpeech = GetPos(Tag(word));

            if (partOfSpeech == NOUN) return WordNetEngine.POS.Noun;
            else if (partOfSpeech == VERB) return WordNetEngine.POS.Verb;
            else if (partOfSpeech == ADJECTIVE) return WordNetEngine.POS.Adjective;
            else if (partOfSpeech == ADVERB) return WordNetEngine.POS.Adverb;
            else return WordNetEngine.POS.None;
        }

        public static PartsOfSpeech GetWnlibPOSFromWordNetEnginePOS(WordNetEngine.POS pos)
        {
            if (pos == WordNetEngine.POS.Adjective) return PartsOfSpeech.Adj;
            else if (pos == WordNetEngine.POS.Adverb) return PartsOfSpeech.Adv;
            else if (pos == WordNetEngine.POS.Noun) return PartsOfSpeech.Noun;
            else if (pos == WordNetEngine.POS.Verb) return PartsOfSpeech.Verb;
            else return PartsOfSpeech.Unknown;
        }

        public static PartsOfSpeech GetWnlibPOS(string word)
        {
            int partOfSpeech = GetPos(Tag(word));

            if (partOfSpeech == NOUN) return PartsOfSpeech.Noun;
            else if (partOfSpeech == VERB) return PartsOfSpeech.Verb;
            else if (partOfSpeech == ADJECTIVE) return PartsOfSpeech.Adj;
            else if (partOfSpeech == ADVERB) return PartsOfSpeech.Adv;
            else return PartsOfSpeech.Unknown;
        }

        private static int GetPos(string pos)
        {
            if (NOUNS.Contains(pos)) return NOUN;
            else if (VERBS.Contains(pos)) return VERB;
            else if (ADJECTIVES.Contains(pos)) return ADJECTIVE;
            else if (ADVERBS.Contains(pos)) return ADVERB;
            else return NONE;
        }

        private static string Tag(string word)
        {
            return posTagger.Tag(new string[] { word })[0];
        }

        public static bool IsConjunction(string word)
        {
            string[] pos = posTagger.Tag(new string[] { word });

            if (CONJUNCTIONS.Contains(pos[0])) return true;

            return false;
        }

        public static string WithoutUnderScore(string word)
        {
            if (word.Contains('_'))
            {
                return word.Replace('_', ' ');
            }

            return word;
        }

        public static string FirstLetterToUpper(string word)
        {
            StringBuilder newWord = new StringBuilder();

            newWord.Append(char.ToUpper(word.Substring(0, 1).ToCharArray()[0]));
            newWord.Append(word.Substring(1));

            return newWord.ToString();
        }

        public static double Average(double[] nums)
        {
            double sum = 0;

            foreach (double d in nums) sum += d;

            double average = sum / nums.Length;

            return average;
        }

        public static bool IsImportantWord(string word)
        {
            return !char.IsPunctuation(word[0]) && !IsConjunction(word);
        }

        public static int GetImportantWordsCount(string[] words)
        {
            int importantWords = 0;

            foreach (string word in words)
            {
                if (IsImportantWord(word)) importantWords++;
            }

            return (importantWords > 0) ? importantWords : 1;
        }
    }
}
