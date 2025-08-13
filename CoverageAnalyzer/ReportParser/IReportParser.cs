using System.Collections.Generic;
using CoverageAnalyzer.ReportParser.Entitites;

namespace CoverageAnalyzer.ReportParser
{
   public interface IReportParser
    {
        IEnumerable<FileCoverage> ParseCoverageReport(string coverageFilePath);
    }
}