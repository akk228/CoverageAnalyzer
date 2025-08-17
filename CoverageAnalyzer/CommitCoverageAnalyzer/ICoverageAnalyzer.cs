using System;
using CoverageAnalyzer.CommitCoverageAnalyzer.Entitties;
using LibGit2Sharp;

namespace CoverageAnalyzer.CommitCoverageAnalyzer
{
    public interface ICoverageAnalyzer
    {
        BranchCoverage AnalyzeBranchCoverage(string targetBranch, string referenceBranch = "HEAD");
    }
}

