using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConverterMarkdown
{
    class MatchIndexComparer : IComparer<Match>
    {
        public int Compare(Match x, Match y)
        {
            if (x.Index > y.Index)
                return 1;
            else if (x.Index < y.Index)
                return -1;
            else
                return 0;
        }
    }
}
