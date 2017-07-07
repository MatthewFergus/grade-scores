using System;
using Xunit;
using FluentAssertions;
using GradeScores.IO;
using GradeScores.Model;

namespace GradeScoresTests
{
    public class StudentScoreCsvParserTests
    {
        [Theory]
        [InlineData("CHARLES,KANE,50")]
        public void TestRead(string input)
        {
            var parser = new StudentScoreCsvParser();

            var score = parser.ReadScoreFromLine(input);

            score.Score.Should().Be(50);
            score.FirstName.Should().Be("CHARLES");
            score.Surname.Should().Be("KANE");
        }

        [Theory]
        [InlineData("CHARLES, KANE, 50")]
        public void TestReadStripsWhitespace(string input)
        {
            var parser = new StudentScoreCsvParser();

            var score = parser.ReadScoreFromLine(input);

            score.Score.Should().Be(50);
            score.FirstName.Should().Be("CHARLES");
            score.Surname.Should().Be("KANE");
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
            var score = new StudentScore("CHARLES", "KANE", 50);
            var parser = new StudentScoreCsvParser();

            parser.WriteScoreToLine(score).Should().Be("KANE, CHARLES, 50");
        }
    }
}
