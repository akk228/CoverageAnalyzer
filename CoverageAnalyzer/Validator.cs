using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;

public class Validator
{
    private readonly IBuildEngine _buildEngine;

    public Validator(IBuildEngine buildEngine)
    {
        _buildEngine = buildEngine;
    }

    public bool Validate(string coverageFilePath, string targetBranch = "", string referenceBranch = "")
    {
        var errors = new List<BuildErrorEventArgs>();

        errors.AddRange(ValidatePath(coverageFilePath));

        foreach (var error in errors)
        {
            _buildEngine.LogErrorEvent(error);
        }

        return !errors.Any(); // All parameters are valid
    }

    private IEnumerable<BuildErrorEventArgs> ValidatePath(string path)
    {
        var errors = new List<BuildErrorEventArgs>();

        if (string.IsNullOrWhiteSpace(path))
        {
            errors.Add(new BuildErrorEventArgs(
                "CoverageAnalyzer",
                "InvalidPath",
                null,
                0,
                0,
                0,
                0,
                "The path to the coverage file cannot be null or empty.",
                "",
                "CoverageAnalyzerTask"));
        }

        if (!File.Exists(path))
        {
            errors.Add(new BuildErrorEventArgs(
                "CoverageAnalyzer",
                "InvalidPath",
                null,
                0,
                0,
                0,
                0,
                $"The path '{Path.GetDirectoryName(path)}' to the file '{Path.GetFileName(path)}' is invalid or does not exist.",
                "",
                "CoverageAnalyzerTask"));
        }

        return errors;
    }
}
