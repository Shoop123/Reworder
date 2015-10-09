namespace Anti_Plagiarism
{
    class IndexCounter
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int Index { get; set; }

        public IndexCounter(int start, int end, int index)
        {
            this.Start = start;
            this.End = end;
            this.Index = index;
        }
    }
}
