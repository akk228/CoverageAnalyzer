using System;
using System.Collections.Generic;

namespace CoverageAnalyzer.GitApi.Entities
{
    public class GitDiff
    {
        public string FilePath { get; set; }
        public string ChangeType { get; set; } // Added, Modified, Deleted
        public string DiffContent { get; set; }

        public IEnumerable<DiffLine> LinesAdded { get; set; }
        public IEnumerable<DiffLine> LinesDeleted { get; set; }
        // public GitDiff(string filePath, string changeType, string diffContent)
        // {
        //     if (string.IsNullOrWhiteSpace(filePath))
        //     {
        //         throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
        //     }

        //     if (string.IsNullOrWhiteSpace(changeType))
        //     {
        //         throw new ArgumentException("Change type cannot be null or empty.", nameof(changeType));
        //     }

        //     FilePath = filePath;
        //     ChangeType = changeType;
        //     DiffContent = diffContent;
        // }
    }
}