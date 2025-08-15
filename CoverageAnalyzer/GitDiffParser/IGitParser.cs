using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace CoverageAnalyzer.GitDiffParser
{
    public interface IGitParser
    {
        FileDiff ParseFileDiff(string targetBranch, string referenceBranch = "");

        FileDiff ParseFileDiff(IEnumerable<string> commitIds);
    }
}

