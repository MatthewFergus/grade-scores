using Xunit;
using FluentAssertions;
using GradeScores.IO;

namespace GradeScoresTests.IO
{
    public class ScoresFileTests
    {
        [Theory]
        [InlineData(@"C:\test\test.txt", @"C:\test\test-graded.txt")]
        [InlineData("test.txt", "test-graded.txt")]
        [InlineData("test", "test-graded.txt")]
        public void TestGradedSuffixWorks(string filePath, string gradedFilePath)
        {
            ScoresFile.ApplyGradedSuffix(filePath).Should().Be(gradedFilePath);
        }
    }
}
