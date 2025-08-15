using System.Collections.Generic;
using System.Linq;
using CoverageAnalyzer.ReportParser;
using CoverageAnalyzer.ReportParser.Entitites;
using CoverageAnalyzer.ReportParser.Implementations.AltCover;
using CoverageAnalyzer.ReportParser.XmlReader;

namespace CoverageAnalyzer.Tests;

public class WhenParsingCoverageXml
{
    private readonly IReportParser _reportParser;

    public static IEnumerable<object[]> TestFilesData()
    {
        yield return new object[] {
            new List<FileCoverage>
            {
                new FileCoverage(1, @"c:\Users\andre\Documents\Studying\CodeCoverageTrial\src\SampleLibrary\Calculator.cs"),
            },
            "TestFiles/test-xunit-coverage.xml"
        };
    }

    public WhenParsingCoverageXml()
    {
        _reportParser = new AltCoverReportParser(new DescendantReader());
    }

    [Theory]
    [MemberData(nameof(TestFilesData))]
    public void GetCorrectFileNamesAndIds(IEnumerable<FileCoverage> expectedFileCoverages, string coverageFilePath)
    {
        var actualFileCoverages = _reportParser.ParseCoverageReport(coverageFilePath);

        Assert.Equal(expectedFileCoverages.Count(), actualFileCoverages.Count());

        foreach (var expected in expectedFileCoverages)
        {
            var actual = actualFileCoverages.FirstOrDefault(fc => fc.UId == expected.UId);
            Assert.NotNull(actual);
            Assert.Equal(expected.FileName, actual.FileName);
        }
    }

    public static IEnumerable<object[]> TestLinesData()
    {
        yield return new object[] {
            new List<CoverableLine>
            {
                new CoverableLine(8, 1),
                new CoverableLine(9, 1),
                new CoverableLine(10, 0),
                new CoverableLine(11, 0),
                new CoverableLine(14, 1),
                new CoverableLine(16, 1),
                new CoverableLine(17, 1),
            },
            "TestFiles/test-xunit-coverage.xml"
        };
    }

    [Theory]
    [MemberData(nameof(TestLinesData))]
    public void GetCorrectLines(IEnumerable<CoverableLine> expectedLines, string coverageFilePath)
    {
        var actualFileCoverages = _reportParser.ParseCoverageReport(coverageFilePath);

        var actualLines = actualFileCoverages
            .SelectMany(fc => fc.Lines)
            .OrderBy(line => line.LineNumber)
            .ToList();

        Assert.Equal(expectedLines.Count(), actualLines.Count);

        foreach (var expected in expectedLines)
        {
            var actual = actualLines.FirstOrDefault(line => line.LineNumber == expected.LineNumber);
            Assert.NotNull(actual);
            Assert.Equal(expected.IsCovered, actual.IsCovered);
        }
    }
}
