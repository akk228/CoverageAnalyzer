using System.Collections.Generic;

namespace CoverageAnalyzer.ReportParser.Entitites
{
    public class FileCoverage
    {
        public FileCoverage(int uId, string fileName)
        {
            UId = uId;
            FileName = fileName;
            Lines = new List<CoverableLine>();
        }

        public int UId { get; }
        public string FileName { get; }
        public IEnumerable<CoverableLine> Lines { get; }
    }
}