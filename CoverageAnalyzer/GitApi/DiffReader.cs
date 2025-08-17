using System;
using System.Collections.Generic;
using CoverageAnalyzer.GitApi.Entities;
using LibGit2Sharp;

namespace CoverageAnalyzer.GitApi
{
    public class DiffReader
    {
        /// <summary>
        /// Gets the differences between two branches in a Git repository.
        /// </summary>
        /// <param name="repositoryPath">The path to the Git repository.</param>
        /// <param name="targetBranch">The target branch to compare against.</param>
        /// <param name="referenceBranch">The reference branch to compare from. Defaults to "HEAD".</param>
        /// <returns>A collection of <see cref="GitDiff"/> representing the differences.</returns
        public IEnumerable<GitDiff> Get(string repositoryPath, string targetBranch, string referenceBranch = "HEAD")
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
                var targetCommit = (repo.Branches[targetBranch]?.Tip) ?? throw new ArgumentException($"Target branch '{targetBranch}' does not exist.", nameof(targetBranch));
                var diff = repo.Diff.Compare<Patch>(
                    referenceCommit.Tree,
                    targetCommit.Tree,
                    new CompareOptions { ContextLines = 0 }
                );
                var diffs = new List<GitDiff>();

                foreach (var patchEntry in diff)
                {
                    diffs.Add(new GitDiff(patchEntry.Path, patchEntry.Status.ToString(), patchEntry.Patch));
                }

                return diffs;
            }
        }
    }
}