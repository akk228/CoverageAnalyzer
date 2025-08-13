using System.Xml.Serialization;

namespace CoverageAnalyzer.ReportParser.Impementations.AltCover.XmlSchema
{
    [XmlType("SequencePoint")]
    public class SequencePoint
    {
        [XmlAttribute("vc")]
        public int VisitCount { get; set; }

        [XmlAttribute("sl")]
        public int StartLine { get; set; }

        [XmlAttribute("sc")]
        public int StartColumn { get; set; }

        [XmlAttribute("el")]
        public int EndLine { get; set; }

        [XmlAttribute("ec")]
        public int EndColumn { get; set; }

        [XmlAttribute("fileid")]
        public string FileId { get; set; }
    }
}
