using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CoverageAnalyzer.ReportParser.Entitites;

namespace CoverageAnalyzer.ReportParser
{
    public class CoverageReportParser
    {
        public List<FileCoverage> ParseCoverageReport(string coverageFilePath)
        {
            var coverageReport = XDocument.Load(coverageFilePath);
            var fileCoverage = GetFiles(coverageReport);

            return fileCoverage;
        }

        private List<FileCoverage> GetFiles(XDocument document)
        {
            return document
                .Descendants("File")
                .Select(file => new FileCoverage
                {
                    FileName = file.Attribute("fullPath")?.Value,
                    UId = int.Parse(file.Attribute("uid")?.Value),
                })
                .ToList();
        }
    }
}