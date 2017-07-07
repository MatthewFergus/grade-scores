using GradeScores;
using FluentAssertions;
using Xunit;
using System.Collections.Generic;
using GradeScores.Model;
using System.Linq;

namespace GradeScoresTests
{
    public class ByHighestScoreGraderTests
    {
        [Fact]
        public void TestGraderSortsByScoreDescending()
        {
            IStudentScore[] scores =
                {
                    new StudentScore("FRANCIS","SMITH",85),
                    new StudentScore("CHARLES","KANE",90),
                    new StudentScore("MADISON","KING",83)
                };

            IStudentScore[] gradedScores =
                {
                    new StudentScore("CHARLES","KANE",90),
                    new StudentScore("FRANCIS","SMITH",85),
                    new StudentScore("MADISON","KING",83)
                };

            var grader = new ByHighestScoreGrader();
            var graderOutput = grader.Grade(scores);
            CheckScoresEquivalent(graderOutput, gradedScores);
        }

        [Fact]
        public void TestGraderSortsBySurnameAscendingWhenScoreEqual()
        {
            IStudentScore[] scores =
                {
                    new StudentScore("CHARLES","KANE",83),
                    new StudentScore("MADISON","AYLER",83),
                    new StudentScore("TED","BUNDY",83)
                };

            IStudentScore[] gradedScores =
                {
                    new StudentScore("MADISON","AYLER",83),
                    new StudentScore("TED","BUNDY",83),
                    new StudentScore("CHARLES","KANE",83)
                };

            var grader = new ByHighestScoreGrader();
            var graderOutput = grader.Grade(scores);
            CheckScoresEquivalent(graderOutput, gradedScores);
        }

        [Fact]
        public void TestGraderSortsByFirstNameAscendingWhenSurnameAndScoreEqual()
        {
            IStudentScore[] scores =
                {
                    new StudentScore("FRANCIS","SMITH",85),
                    new StudentScore("ALLAN","SMITH",85),
                    new StudentScore("CHARLES","SMITH",85)
                };

            IStudentScore[] gradedScores =
                {
                    new StudentScore("ALLAN","SMITH",85),
                    new StudentScore("CHARLES","SMITH",85),
                    new StudentScore("FRANCIS","SMITH",85)
                };

            var grader = new ByHighestScoreGrader();
            var graderOutput = grader.Grade(scores);
            CheckScoresEquivalent(graderOutput, gradedScores);
        }

        [Fact]
        public void TestGraderFirstNameCaseInsensitive()
        {
            IStudentScore[] scores =
                {
                    new StudentScore("FRANCIS","SMITH",88),
                    new StudentScore("allan","SMITH",88)
                };

            IStudentScore[] gradedScores =
                {
                    new StudentScore("allan","SMITH",88),
                    new StudentScore("FRANCIS","SMITH",88)
                };

            var grader = new ByHighestScoreGrader();
            var graderOutput = grader.Grade(scores);
            CheckScoresEquivalent(graderOutput, gradedScores);
        }

        [Fact]
        public void TestGraderSurnameCaseInsensitive()
        {
            IStudentScore[] scores =
                {
                    new StudentScore("FRANCIS","SMITH",88),
                    new StudentScore("allan","ayler",88)
                };

            IStudentScore[] gradedScores =
                {
                    new StudentScore("allan","ayler",88),
                    new StudentScore("FRANCIS","SMITH",88)
                };

            var grader = new ByHighestScoreGrader();
            var graderOutput = grader.Grade(scores);
            CheckScoresEquivalent(graderOutput, gradedScores);
        }

        private static void CheckScoresEquivalent(IEnumerable<IStudentScore> scores, IEnumerable<IStudentScore> otherScores)
        {
            var scoresArray = scores.ToArray();
            var otherScoresArray = otherScores.ToArray();

            scoresArray.Length.Should().Be(otherScoresArray.Length);

            var index = 0;
            foreach (var score in scoresArray)
            {
                var otherScore = otherScoresArray[index];
                score.ShouldBeEquivalentTo(otherScore);
                index++;
            }
        }
    }
}
