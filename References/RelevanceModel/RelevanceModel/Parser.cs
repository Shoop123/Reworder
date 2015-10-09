using LAIR.Collections.Generic;
using LAIR.ResourceAPIs.WordNet;
using OpenNLP.Tools.PosTagger;
using OpenNLP.Tools.SentenceDetect;
using OpenNLP.Tools.Tokenize;

namespace RelevanceModel
{
    public class Parser
    {
        private EnglishMaximumEntropyPosTagger posTagger = new EnglishMaximumEntropyPosTagger(StaticHelper.MODEL_PATH + "EnglishPOS.nbin");

        private EnglishMaximumEntropyTokenizer tokenizer = new EnglishMaximumEntropyTokenizer(StaticHelper.MODEL_PATH + "EnglishTok.nbin");

        private EnglishMaximumEntropySentenceDetector sentenceDetector = new EnglishMaximumEntropySentenceDetector(StaticHelper.MODEL_PATH + "EnglishSD.nbin");

        public SynSet[] GetSynSets(string word)
        {
            WordNetEngine.POS pos = StaticHelper.GetWordNetEnginePOS(word);

            if (pos == WordNetEngine.POS.None) return null;

            Set<SynSet> synsets = StaticHelper.wordNetEngine.GetSynSets(word, pos);

            return synsets.ToArray();
        }

        public string GetGetMostCommonReplacement(string word)
        {
            WordNetEngine.POS pos = StaticHelper.GetWordNetEnginePOS(word);

            if (pos == WordNetEngine.POS.None) return word;

            SynSet synset = StaticHelper.wordNetEngine.GetMostCommonSynSet(word, pos);

            if(synset != null && synset.Words.Count > 0)
                return synset.Words[0];

            return word;
        }

        public string[] Tokenize(string text)
        {
            return tokenizer.Tokenize(text);
        }

        public string[] Tag(string[] words)
        {
            return posTagger.Tag(words);
        }

        public string[] SentenceDetect(string text)
        {
            return sentenceDetector.SentenceDetect(text);
        }
    }
}
