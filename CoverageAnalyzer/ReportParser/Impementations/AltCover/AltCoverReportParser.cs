using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using CoverageAnalyzer.ReportParser.Entitites;
using CoverageAnalyzer.ReportParser.Impementations.AltCover.XmlSchema;

namespace CoverageAnalyzer.ReportParser.Implementations.AltCover
{
    public class AltCoverReportParser : IReportParser
    {
        private const string UIdAttributeName = "uid";
        private const string FullPathAttributeName = "fullPath";
        private const string FileNodeName = "File";
        private const string SequenceNodeName = "SequencePoints";

        public IEnumerable<FileCoverage> ParseCoverageReport(string coverageFilePath)
        {
            var coverageReport = XDocument.Load(coverageFilePath);
            var fileCoverage = GetFiles(coverageReport);
            var fileIds = fileCoverage.ToDictionary(f => f.UId, f => f);

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
                ));
        }

        /// <summary>
        /// Extracts sequence points from the XML document.
        /// This method retrieves all <SequencePoints> elements and deserializes them into SequencePoint objects.
        /// </summary>
        /// <param name="document">Xml with code coverage report</param>
        /// <returns>enunmerable colection of sequence points</returns>
        private IEnumerable<SequencePoint> GetSequencePoints(XDocument document)
        {
            var sequencePointSerializer = new XmlSerializer(typeof(SequencePoint));
            return document
                .Descendants(SequenceNodeName)
                .Select(sq =>
                {
                    using (var reader = sq.CreateReader())
                    {
                        return (SequencePoint)sequencePointSerializer.Deserialize(reader);
                    }
                });
        }
    }
}