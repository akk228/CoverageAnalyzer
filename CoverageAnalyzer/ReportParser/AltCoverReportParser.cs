using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CoverageAnalyzer.ReportParser.Entitites;

namespace CoverageAnalyzer.ReportParser
{
    public class AltCoverReportParser : IReportParser
    {
        private const string UIdAttributeName = "uid";
        private const string FullPathAttributeName = "fullPath";
        private const string FileNodeName = "File";

        public IEnumerable<FileCoverage> ParseCoverageReport(string coverageFilePath)
        {
            var coverageReport = XDocument.Load(coverageFilePath);
            var fileCoverage = GetFiles(coverageReport);

            return fileCoverage;
        }

        private IEnumerable<FileCoverage> GetFiles(XDocument document)
        {
            // TODO: Handle potential null values and exceptions
            return document
                .Descendants(FileNodeName)
                .Select(file => new FileCoverage(
                    int.Parse(file.Attribute(UIdAttributeName).Value),
                    file.Attribute(FullPathAttributeName).Value
                )).ToList();
        }
    }
}