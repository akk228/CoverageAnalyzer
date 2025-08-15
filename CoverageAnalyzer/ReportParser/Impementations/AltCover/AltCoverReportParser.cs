using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using CoverageAnalyzer.ReportParser.Entitites;
using CoverageAnalyzer.ReportParser.Impementations.AltCover.XmlSchema;
using CoverageAnalyzer.ReportParser.XmlReader;

namespace CoverageAnalyzer.ReportParser.Implementations.AltCover
{
    public class AltCoverReportParser : IReportParser
    {
        private const string FileNodeName = "File";
        private const string SequenceNodeName = "SequencePoints";

        private readonly DescendantReader _descendantReader;
        
        public AltCoverReportParser(DescendantReader descendantReader)
        {
            _descendantReader = descendantReader;
        }

        public IEnumerable<FileCoverage> ParseCoverageReport(string coverageFilePath)
        {
            var coverageReport = XDocument.Load(coverageFilePath);
            var filesSubjectedToCoverage = _descendantReader.ReadDocumentDescendants<File>(coverageReport, FileNodeName);
            var sequencePoints = _descendantReader.ReadDocumentDescendants<SequencePoint>(coverageReport, SequenceNodeName);
            
            return filesSubjectedToCoverage.Select(file => new FileCoverage(file.UId, file.FullPath)); ;
        }
    }
}