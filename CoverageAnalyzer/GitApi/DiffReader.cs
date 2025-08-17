using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoverageAnalyzer.GitApi.Entities;
using CoverageAnalyzer.GitApi.Mappers;
using LibGit2Sharp;

namespace CoverageAnalyzer.GitApi
{
    public class DiffReader
    {
        private readonly IMapper _mapper;

        public DiffReader(IMapper mapper)
        {
            _mapper = mapper;
        }

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
                var referenceTip = repo.Branches[referenceBranch]?.Tip ?? repo.Head.Tip;
                var targetTip = (repo.Branches[targetBranch]?.Tip) ?? throw new ArgumentException($"Target branch '{targetBranch}' does not exist.", nameof(targetBranch));
                var diff = repo.Diff.Compare<Patch>(referenceTip.Tree, targetTip.Tree, new CompareOptions { ContextLines = 0 });

                return MapPatchEntriesToGitDiffs(diff);
            }
        }

        // TODO : remove that and use automapper
        private IEnumerable<GitDiff> MapPatchEntriesToGitDiffs(IEnumerable<PatchEntryChanges> patchEntries)
        {
            return patchEntries.Select(patchEntry => new GitDiff
            {
                FilePath = patchEntry.Path,
                ChangeType = patchEntry.Status.ToString(),
                DiffContent = patchEntry.Patch,
                LinesAdded = patchEntry.AddedLines.Select(line => new DiffLine
                {
                    LineNumber = line.LineNumber,
                    Content = line.Content,
                }),
                LinesDeleted = patchEntry.DeletedLines.Select(line => new DiffLine
                {
                    LineNumber = line.LineNumber,
                    Content = line.Content,
                })
            });
        }
    }
}