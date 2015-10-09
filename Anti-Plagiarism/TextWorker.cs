using System.Windows.Controls;

namespace Anti_Plagiarism
{
    class TextWorker : Label
    {

        public int startIndex = 0;
        public int endIndex = 0;

        public TextWorker(int start, int end) : base()
        {
            this.startIndex = start;
            this.endIndex = end;
        }

    }
}
