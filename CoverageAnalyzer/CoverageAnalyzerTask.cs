using System.Resources;
using Microsoft.Build.Framework;
// using Microsoft.Build.Utilities;
using Task = Microsoft.Build.Utilities.Task;

public class CoverageAnalyzerTask : Task
{
    [Required]
    public string CoverageFilePath { get; set; }

    public string ReferenceBranch { get; set; }

    public string TargetBranch { get; set; }

    public override bool Execute()
    {
        #if DEBUG
            System.Diagnostics.Debugger.Launch();
        #endif

        var validator = new Validator(BuildEngine);

        if (!validator.Validate(CoverageFilePath, TargetBranch, ReferenceBranch))
        {
            return false;
        }

        BuildEngine.LogMessageEvent(new BuildMessageEventArgs(
            $"Analyzing coverage differences between branches {ReferenceBranch} and {TargetBranch} using file {CoverageFilePath}.",
            "", "CoverageAnalyzerTask", MessageImportance.High));

        // Add logic to process the coverage.xml file and compare branches here.

        return true;
    }
}