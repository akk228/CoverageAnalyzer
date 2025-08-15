namespace CoverageAnalyzer.ReportParser.Entitites
{
    public class CoverableLine
    {
        public CoverableLine(int lineNumber, int hitCount)
        {
            LineNumber = lineNumber;
            IsCovered = hitCount > 0;
        }

        public int LineNumber { get; }
        public bool IsCovered { get; }
    }
}