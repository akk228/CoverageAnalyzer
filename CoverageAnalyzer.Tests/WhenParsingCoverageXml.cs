using System.Collections.Generic;
using CoverageAnalyzer.CoverageReportParser.Entitites;

namespace CoverageAnalyzer.Tests;

public class WhenParsingCoverageXml
{
    public static IEnumerable<object[]> TestFilesData()
    {
        yield return new object[] {
            new List<FileCoverage>
            {
                new FileCoverage { UId = 1, FileName = "TestProject1.cs" },
                new FileCoverage { UId = 2, FileName = "TestProject2.cs" },
                new FileCoverage { UId = 3, FileName = "TestProject3.cs" }
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
