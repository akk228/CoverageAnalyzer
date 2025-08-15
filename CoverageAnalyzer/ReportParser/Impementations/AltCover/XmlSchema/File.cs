using System.Xml.Serialization;

namespace CoverageAnalyzer.ReportParser.Impementations.AltCover.XmlSchema
{
    public class File
    {
        [XmlAttribute("uid")]
        public int UId { get; set; }

        [XmlAttribute("fullPath")]
        public string FullPath { get; set; }
    }
}