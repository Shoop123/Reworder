using LAIR.ResourceAPIs.WordNet;
using OpenNLP.Tools.Tokenize;

namespace RelevanceModel
{
    internal class ContextualReferenceModel
    {
        private EnglishMaximumEntropyTokenizer tokenizer = new EnglishMaximumEntropyTokenizer(StaticHelper.MODEL_PATH + "EnglishTok.nbin");

        public double Compare(string[] words, int index, string newWord)
        {
            if (index < 0 || index >= words.Length) return 0.0;

            double[] similarities = new double[3];

            string word = words[index];

            similarities[0] = ShallowComparison(word, newWord);

            similarities[1] = RootComparison(words, index, newWord);

            similarities[2] = DeepRootComparison(words, index, newWord);

            return StaticHelper.Average(similarities);
        }

        private double DeepRootComparison(string[] words, int index, string newWord)
        {
            if (index < 0 || index >= words.Length) return 0.0;

            double[] similarities = new double[words.Length];

            for (int j = 0; j < words.Length; j++)
            {
                if (!StaticHelper.IsImportantWord(words[j]) || words[j].Equals(newWord, System.StringComparison.CurrentCultureIgnoreCase)) continue;

                string[] def1 = tokenizer.Tokenize(GetDefinition(words[j]));

                double[] stats = new double[def1.Length];

                for (int i = 0; i < def1.Length; i++)
                {
                    double similarity = ShallowComparison(def1[i], newWord);
                    stats[i] = similarity;
                }

                similarities[j] = StaticHelper.Average(stats);
            }

            return StaticHelper.Average(similarities);
        }

        private double RootComparison(string[] words, int index, string newWord)
        {
            if (index < 0 || index >= words.Length) return 0.0;

            int sameWords = 0;

            double[] similarities = new double[words.Length];

            for (int i = 0; i < words.Length; i++)
            {
                if (!StaticHelper.IsImportantWord(words[i]) || words[i].Equals(newWord, System.StringComparison.CurrentCultureIgnoreCase)) continue;

                string[] def = tokenizer.Tokenize(GetDefinition(words[i]));

                foreach (string defWord in def)
                {
                    if (!StaticHelper.IsImportantWord(defWord)) continue;

                    if (defWord.Equals(newWord, System.StringComparison.CurrentCultureIgnoreCase)) sameWords++;
                }

                double similarity = (sameWords / StaticHelper.GetImportantWordsCount(def)) * 100.0;
                similarities[i] = similarity;
            }

            return StaticHelper.Average(similarities);
        }

        private double ShallowComparison(string word1, string word2)
        {
            string[] def1 = tokenizer.Tokenize(GetDefinition(word1));
            string[] def2 = tokenizer.Tokenize(GetDefinition(word2));

            int sameWords = 0;

            foreach(string word in def1)
            {
                if (!StaticHelper.IsImportantWord(word)) continue;

                foreach(string otherWord in def2)
                {
                    if (!StaticHelper.IsImportantWord(word)) continue;

                    if (word.Equals(otherWord, System.StringComparison.CurrentCultureIgnoreCase)) sameWords++;
                }
            }

            double similarity = (sameWords / StaticHelper.GetImportantWordsCount(def1)) * 100.0;

            return similarity;
        }

        private string GetDefinition(string word)
        {
            WordNetEngine.POS wordNetEnginePOS = StaticHelper.GetWordNetEnginePOS(word);

            SynSet mostCommonSynSet = null;

            if(wordNetEnginePOS != WordNetEngine.POS.None)
                mostCommonSynSet = StaticHelper.wordNetEngine.GetMostCommonSynSet(word, wordNetEnginePOS);

            if (mostCommonSynSet == null) return word;

            string definition = mostCommonSynSet.Gloss.Split(';')[0];

            return definition;
        }
    }
}
