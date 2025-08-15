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
        private const string SequenceNodeName = "SequencePoint";

        private readonly DescendantReader _descendantReader;
        
        public AltCoverReportParser(DescendantReader descendantReader)
        {
            _descendantReader = descendantReader;
        }

        /// <summary>
        /// Parses the coverage report from the specified XML file path.
        /// </summary>
        /// <param name="coverageFilePath">The file path to the XML coverage report.</param>
        /// <returns>
        /// An enumerable collection of <see cref="FileCoverage"/> objects, where each object represents a file
        /// and its associated coverable lines with their coverage status.
        /// </returns>
        /// <remarks>
        /// This method reads the coverage report XML file and extracts information about files and their sequence points.
        /// Each sequence point is mapped to its corresponding file, and the lines covered or uncovered are added to the
        /// <see cref="FileCoverage"/> object. The method uses a <see cref="DescendantReader"/> to deserialize XML nodes
        /// into strongly-typed objects.
        ///
        /// The following XML structure is expected (since it's based on AltCover schema):
        /// <example>
        /// <![CDATA[
        /// <CoverageSession>
        ///   <Files>
        ///     <File uid="1" fullPath="path/to/file.cs" />
        ///   </Files>
        ///   <SequencePoints>
        ///     <SequencePoint vc="1" sl="10" el="20" fileid="1" />
        ///   </SequencePoints>
        /// </CoverageSession>
        /// ]]>
        /// </example>
        /// </remarks>
        public IEnumerable<FileCoverage> ParseCoverageReport(string coverageFilePath)
        {
            var coverageReport = XDocument.Load(coverageFilePath);

            var filesSubjectedToCoverage = _descendantReader
                .ReadDocumentDescendants<File>(coverageReport, FileNodeName)
                .ToDictionary(
                    file => file.UId,
                    file => new FileCoverage(file.UId, file.FullPath)
                );

            var sequencePoints = _descendantReader
                .ReadDocumentDescendants<SequencePoint>(coverageReport, SequenceNodeName);
            
            foreach (var sequencePoint in sequencePoints)
            {
                for (var startLine = sequencePoint.StartLine; startLine <= sequencePoint.EndLine; startLine++)
                {
                    if (filesSubjectedToCoverage.TryGetValue(sequencePoint.FileId, out var file))
                    {
                        file.Lines.Add(new CoverableLine(startLine, sequencePoint.VisitCount));
                    }
                }
            }

            return filesSubjectedToCoverage.Values;
        }
    }
}