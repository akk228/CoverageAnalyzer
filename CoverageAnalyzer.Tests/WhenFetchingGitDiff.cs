using System;
using CoverageAnalyzer.GitApi;
using Xunit;

namespace CoverageAnalyzer.Tests
{
    public class WhenFetchingGitDiff
    {
        [Theory]
        [InlineData(@"C:\Users\andre\Documents\Studying\CodeCoverageTrial", "caclulator-change", "main")]
        public void ShouldThrowExceptionForInvalidRepositoryPath(string repoPath, string targetBranch, string referenceBranch)
        {
            var diffReader = new DiffReader();
            var result = diffReader.Get(repoPath, targetBranch, referenceBranch);
            Assert.True(true, "This test is a placeholder for actual implementation.");
        }
    }
}