using System.Collections.Generic;
using System.Linq;
using CoverageAnalyzer.ReportParser;
using CoverageAnalyzer.ReportParser.Entitites;

namespace CoverageAnalyzer.Tests;

public class WhenParsingCoverageXml
{
    public static IEnumerable<object[]> TestFilesData()
    {
        yield return new object[] {
            new List<FileCoverage>
            {
                new FileCoverage { UId = 1, FileName = @"c:\Users\andre\Documents\Studying\CodeCoverageTrial\src\SampleLibrary\Calculator.cs" },
            },
            "TestFiles/test-xunit-coverage.xml"
        };
    }

    [Theory]
    [MemberData(nameof(TestFilesData))]
    public void GetCorrectFileNamesAndIds(IEnumerable<FileCoverage> expectedFileCoverages, string coverageFilePath)
    {
        var parser = new CoverageReportParser();
        var actualFileCoverages = parser.ParseCoverageReport(coverageFilePath);

        Assert.Equal(expectedFileCoverages.Count(), actualFileCoverages.Count());

        foreach (var expected in expectedFileCoverages)
        {
            var actual = actualFileCoverages.FirstOrDefault(fc => fc.UId == expected.UId);
            Assert.NotNull(actual);
            Assert.Equal(expected.FileName, actual.FileName);
        }
    }
}
