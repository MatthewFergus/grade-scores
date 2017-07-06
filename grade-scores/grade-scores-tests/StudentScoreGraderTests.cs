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
            CheckScoresEquivalent(graderOutput, gradedScores).Should().BeTrue();
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
            CheckScoresEquivalent(graderOutput, gradedScores).Should().BeTrue();
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
            CheckScoresEquivalent(graderOutput, gradedScores).Should().BeTrue();
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
            CheckScoresEquivalent(graderOutput, gradedScores).Should().BeTrue();
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
            CheckScoresEquivalent(graderOutput, gradedScores).Should().BeTrue();
        }

        /// <summary>
        /// Returns true if scores and otherScores contain equivalent IStudentScore objects in the same order
        /// </summary>
        private bool CheckScoresEquivalent(IEnumerable<IStudentScore> scores, IEnumerable<IStudentScore> otherScores)
        {
            var scoresArray = scores.ToArray();
            var otherScoresArray = otherScores.ToArray();

            if (scoresArray.Length != otherScoresArray.Length)
            {
                return false;
            }

            var index = 0;
            foreach (var score in scoresArray)
            {
                var otherScore = otherScoresArray[index];
                if (score.Score != otherScore.Score
                    || !string.Equals(score.FirstName, otherScore.FirstName)
                    || !string.Equals(score.Surname, otherScore.Surname))
                {
                    return false;
                }

                index++;
            }

            return true;
        }
    }
}
