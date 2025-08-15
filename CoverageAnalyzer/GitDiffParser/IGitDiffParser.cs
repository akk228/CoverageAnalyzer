using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace CoverageAnalyzer.GitDiffParser
{
    public interface IGitDiffParser
    {
        IEnumerable<FileDiff> ParseDiff(string gitDiff);
    }
}

