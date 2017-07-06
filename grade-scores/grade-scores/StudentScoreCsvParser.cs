using System;
using GradeScores.Model;
using System.Linq;

namespace GradeScores
{
    class StudentScoreCsvParser : IScoreParser
    {
        public IStudentScore ReadScoreFromLine(string line)
        {
            var parts = line.Split(',').Select(p => p.Trim()).ToArray();

            var firstName = parts[0];
            var surname = parts[1];
            var scoreParsed = uint.TryParse(parts[2], out var score);

            if (parts.Length != 3
                || string.IsNullOrEmpty(firstName)
                || string.IsNullOrEmpty(surname)
                || !scoreParsed)
            {
                throw new StudentScoreParsingException();
            }

            return new StudentScore(firstName, surname, score);
        }

        public string WriteScoreToLine(IStudentScore score)
        {
            return $"{score.Surname}, {score.FirstName}, {score.Score}";
        }
    }
}
