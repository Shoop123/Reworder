using LAIR.ResourceAPIs.WordNet;

namespace RelevanceModel
{
    internal class SimilarityModel
    {
        private WordNetSimilarityModel wnsm = new WordNetSimilarityModel(StaticHelper.wordNetEngine);
        private WordNetEngine.SynSetRelation[] relation = new WordNetEngine.SynSetRelation[] { WordNetEngine.SynSetRelation.SimilarTo, WordNetEngine.SynSetRelation.AlsoSee, WordNetEngine.SynSetRelation.Hypernym, WordNetEngine.SynSetRelation.Hyponym };

        internal double GetSimilarity(string one, string two)
        {
            double[] wuPalmerSimilarities = new double[] { WuPalmer1994Average(one, two), WuPalmer1994Maximum(one, two), WuPalmer1994Minimum(one, two), WuPalmer1994MostCommon(one, two) };
            return StaticHelper.Average(wuPalmerSimilarities);
        }

        private double WuPalmer1994Average(string one, string two)
        {
            WordNetEngine.POS posOne = StaticHelper.GetWordNetEnginePOS(one);
            WordNetEngine.POS posTwo = StaticHelper.GetWordNetEnginePOS(two);

            if (posTwo != WordNetEngine.POS.None && posOne != WordNetEngine.POS.None)
            {
                WordNetSimilarityModel.Strategy strategy = WordNetSimilarityModel.Strategy.WuPalmer1994Average;

                double similarity = wnsm.GetSimilarity(one, posOne, two, posTwo, strategy, relation) * 100.0;
                return similarity;
            }
            else return 0;
        }

        private double WuPalmer1994Maximum(string one, string two)
        {
            WordNetEngine.POS posOne = StaticHelper.GetWordNetEnginePOS(one);
            WordNetEngine.POS posTwo = StaticHelper.GetWordNetEnginePOS(two);

            if (posTwo != WordNetEngine.POS.None && posOne != WordNetEngine.POS.None)
            {
                WordNetSimilarityModel.Strategy strategy = WordNetSimilarityModel.Strategy.WuPalmer1994Maximum;

                double similarity = wnsm.GetSimilarity(one, posOne, two, posTwo, strategy, relation) * 100.0;
                return similarity;
            }
            else return 0;
        }

        private double WuPalmer1994Minimum(string one, string two)
        {
            WordNetEngine.POS posOne = StaticHelper.GetWordNetEnginePOS(one);
            WordNetEngine.POS posTwo = StaticHelper.GetWordNetEnginePOS(two);

            if (posTwo != WordNetEngine.POS.None && posOne != WordNetEngine.POS.None)
            {
                WordNetSimilarityModel.Strategy strategy = WordNetSimilarityModel.Strategy.WuPalmer1994Minimum;

                double similarity = wnsm.GetSimilarity(one, posOne, two, posTwo, strategy, relation) * 100.0;
                return similarity;
            }
            else return 0;
        }

        private double WuPalmer1994MostCommon(string one, string two)
        {
            WordNetEngine.POS posOne = StaticHelper.GetWordNetEnginePOS(one);
            WordNetEngine.POS posTwo = StaticHelper.GetWordNetEnginePOS(two);

            if (posTwo != WordNetEngine.POS.None && posOne != WordNetEngine.POS.None)
            {
                WordNetSimilarityModel.Strategy strat = WordNetSimilarityModel.Strategy.WuPalmer1994MostCommon;

                double similarity = wnsm.GetSimilarity(one, posOne, two, posTwo, strat, relation) * 100.0;
                return similarity;
            }
            else return 0;
        }
    }
}
