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

        /// <summary>
        /// Extracts file coverage information from the XML document.
        /// This method retrieves all <File> elements and constructs FileCoverage objects for each.
        /// </summary>
        /// <param name="document">Xml with code coverage report</param>
        /// <returns>enunmerable colection of files that are subjected to coverage</returns>
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