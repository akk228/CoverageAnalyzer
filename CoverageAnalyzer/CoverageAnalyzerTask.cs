using System.Collections.Generic;
using System.Linq;
using System.Resources;
using CoverageAnalyzer.ReportParser;
using CoverageAnalyzer.ReportParser.Entitites;
using CoverageAnalyzer.ReportParser.Implementations.AltCover;
using CoverageAnalyzer.ReportParser.XmlReader;
using Microsoft.Build.Framework;
// using Microsoft.Build.Utilities;
using Task = Microsoft.Build.Utilities.Task;

namespace CoverageAnalyzer
{

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

            LogStartMessage();

            IReportParser reportParser = new AltCoverReportParser(new DescendantReader());

            var fileCoverages = reportParser.ParseCoverageReport(CoverageFilePath);
            
            LogCoveredLines(fileCoverages);

            return true;
        }

        private void LogStartMessage()
        {
            BuildEngine.LogMessageEvent(new BuildMessageEventArgs(
                $"Analyzing coverage differences between branches {ReferenceBranch} and {TargetBranch} using file {CoverageFilePath}.",
                "", "CoverageAnalyzerTask", MessageImportance.High));
        }
        
        private void LogCoveredLines(IEnumerable<FileCoverage> fileCoverages)
        {
            var lineNUmberColumnLength = "Line number ".Length;
            var isCoveredColumnLength = "Is covered".Length;

            foreach (var fileCoverage in fileCoverages)
            {
                var coveredLines = "Line number | Is covered\n" + "------------------------\n" +
                    string.Join(
                        "\n",
                        fileCoverage
                            .Lines
                            .Select(l =>
                                $"{l.LineNumber} ".PadRight(lineNUmberColumnLength) +
                                "| " +
                                $"{l.IsCovered}".PadRight(isCoveredColumnLength)));

                BuildEngine.LogMessageEvent(new BuildMessageEventArgs(
                    $"File: \n\t{fileCoverage.FileName}, ID: \n\t{fileCoverage.UId}\nLines:\n{coveredLines}",
                    "", "CoverageAnalyzerTask", MessageImportance.High));
            }
        }
    }
}