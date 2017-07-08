using System;
using Xunit;
using FluentAssertions;
using GradeScores.IO;
using GradeScores.Model;

namespace GradeScoresTests.IO
{
    public class StudentScoreCsvParserTests
    {
        private static readonly IStudentScore testScore = new StudentScore("CHARLES", "KANE", 50);

        [Theory]
        [InlineData("CHARLES,KANE,50")]
        public void TestRead(string input)
        {
            var parser = new StudentScoreCsvParser();

            var score = parser.ReadScoreFromLine(input);

            score.ShouldBeEquivalentTo(testScore);
        }

        [Theory]
        [InlineData("CHARLES, KANE, 50")]
        public void TestReadStripsWhitespace(string input)
        {
            var parser = new StudentScoreCsvParser();

            var score = parser.ReadScoreFromLine(input);

            score.ShouldBeEquivalentTo(testScore);
        }

        [Theory]
        [InlineData("CHARLES,KANE,")]
        [InlineData("CHARLES,,50")]
        [InlineData(",KANE,50")]
        [InlineData("CHARLES")]
        [InlineData("")]
        [InlineData(null)]
        public void TestReadThrowsParsingException(string input)
        {
            var parser = new StudentScoreCsvParser();

            Action read = () => { parser.ReadScoreFromLine(input); };

            read.ShouldThrow<StudentScoreParsingException>();
        }

        [Fact]
        public void TestWrite()
        {
            var parser = new StudentScoreCsvParser();

            parser.WriteScoreToLine(testScore).Should().Be("KANE, CHARLES, 50");
        }
    }
}
