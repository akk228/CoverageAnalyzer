using System.Collections.Generic;

namespace CoverageAnalyzer.CoverageReportParser.Entitites
{
    public class FileCoverage
    {
        public string FileName { get; set; }
        public IEnumerable<CoverableLine> Lines { get; set; }
    }
}