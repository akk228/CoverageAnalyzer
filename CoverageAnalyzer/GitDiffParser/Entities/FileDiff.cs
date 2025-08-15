using System;
using System.Collections;
using System.Collections.Generic;

namespace CoverageAnalyzer.GitDiffParser
{
    public class FileDiff
    {
        public FileDiff(string filePath, int linesAdded, int linesDeleted)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            LinesAdded = new List<int>();
            LinesDeleted = new List<int>();
        }

        public string FilePath { get; set; }
        public IList<int> LinesAdded { get; set; }
        public IList<int> LinesDeleted { get; set; }
    }
}