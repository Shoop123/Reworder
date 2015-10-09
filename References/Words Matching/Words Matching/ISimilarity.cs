using System;

namespace Words_Matching
{
    interface ISimilarity
    {
        float GetSimilarity(string string1, string string2);
    }
}
