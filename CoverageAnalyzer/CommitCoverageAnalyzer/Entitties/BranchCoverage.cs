using System;
using System.Collections.Generic;

namespace CoverageAnalyzer.CommitCoverageAnalyzer.Entitties
{
    public class BranchCoverage
    {
        public string TargetBranch { get; set; }
        public string ReferenceBranch { get; set; }
        public double LineCoveragePercentage { get; set; }
        public IList<FileCoverage> FileCoverages { get; set; }
    }
}
