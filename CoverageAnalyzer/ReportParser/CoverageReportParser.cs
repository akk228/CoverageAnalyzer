using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CoverageAnalyzer.CoverageReportParser.Entitites;

namespace CoverageAnalyzer.CoverageReportParser
{
    public class CoverageReportParser
    {
        public List<FileCoverage> ParseCoverageReport(string coverageFilePath)
        {
            var coverageReport = XDocument.Load(coverageFilePath);
            var fileCoverage = new List<FileCoverage>();
            
            return fileCoverage;
        }
    }
}