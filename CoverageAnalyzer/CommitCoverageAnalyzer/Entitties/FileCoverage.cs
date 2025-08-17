using System;
using System.Collections.Generic;
using LibGit2Sharp;
using CoverageAnalyzer.GitApi.Entities;

namespace CoverageAnalyzer.CommitCoverageAnalyzer.Entitties
{
    public class FileCoverage
    {
        public string FilePath { get; set; }
        public int LinesCovered { get; set; }
        public int LinesTotal { get; set; }
        public IList<DiffLine> AddedLines { get; set; }
        public double CoveragePercentage => LinesTotal > 0 ? (double)LinesCovered / LinesTotal * 100 : 0;

        public override string ToString()
        {
            return $"{FilePath}: {CoveragePercentage:F2}% ({LinesCovered}/{LinesTotal})";
        }
    }
}
