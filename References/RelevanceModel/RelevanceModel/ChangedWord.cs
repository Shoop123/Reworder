using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelevanceModel
{
    struct ChangedWord
    {
        public string word;
        public int index;

        public ChangedWord(string word, int index)
        {
            this.word = word;
            this.index = index;
        }
    }
}
