using System;
using System.Collections.Generic;
using LibGit2Sharp;

namespace CoverageAnalyzer.GitApi
{
    public class DiffReader
    {
        public IEnumerable<string> GetDiff(string repositoryPath, string targetBranch, string referenceBranch = "HEAD")
        {
            if (string.IsNullOrWhiteSpace(repositoryPath))
            {
                throw new ArgumentException("Repository path cannot be null or empty.", nameof(repositoryPath));
            }

            if (string.IsNullOrWhiteSpace(targetBranch))
            {
                throw new ArgumentException("Target branch cannot be null or empty.", nameof(targetBranch));
            }

            if (!Repository.IsValid(repositoryPath))
            {
                throw new ArgumentException($"The path '{repositoryPath}' is not a valid Git repository.", nameof(repositoryPath));
            }
            
            using (var repo = new Repository(repositoryPath))
            {
                var referenceCommit = repo.Branches[referenceBranch]?.Tip ?? repo.Head.Tip;
                var targetCommit = repo.Branches[targetBranch]?.Tip;

                if (targetCommit == null)
                {
                    throw new ArgumentException($"Target branch '{targetBranch}' does not exist.", nameof(targetBranch));
                }

                var diff = repo.Diff.Compare<Patch>(referenceCommit.Tree, targetCommit.Tree, new CompareOptions
                {
                    ContextLines = 0
                });

                var diffLines = new List<string>();

                foreach (var patchEntry in diff)
                {
                    diffLines.Add(patchEntry.Patch);
                }

                return diffLines;
            }
        }
    }
}