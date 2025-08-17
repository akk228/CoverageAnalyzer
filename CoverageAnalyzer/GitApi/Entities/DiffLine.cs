namespace CoverageAnalyzer.GitApi.Entities
{
    /// <summary>
    /// Represents a line in a Git diff.
    /// </summary>
    public class DiffLine
    {
        /// <summary>
        /// Gets or sets the line number in the file.
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the content of the line.
        /// </summary>
        public string Content { get; set;  }

        /// <summary>
        /// Gets or sets a value indicating whether the line is covered by tests.
        /// </summary>
        public bool IsCovered { get; set; }
    }
}
