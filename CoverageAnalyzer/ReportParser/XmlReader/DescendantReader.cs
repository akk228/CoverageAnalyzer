using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CoverageAnalyzer.ReportParser.XmlReader
{
    public class DescendantReader
    {
        public IEnumerable<T> ReadDocumentDescendants<T>(XDocument document, string nodeName) where T : class
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (string.IsNullOrWhiteSpace(nodeName))
            {
                throw new ArgumentException("Node name cannot be null or empty.", nameof(nodeName));
            }

            var sequencePointSerializer = new XmlSerializer(typeof(T));

            foreach (var element in document.Descendants(nodeName))
            {
                // var item = Activator.CreateInstance(typeof(T), element);
                // yield return item as T;
                using (var reader = element.CreateReader())
                {
                    yield return (T)sequencePointSerializer.Deserialize(reader);
                }
            }
        }
    }
}
