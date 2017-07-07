using System.Linq;
using GradeScores.Model;

namespace GradeScores.IO
{
    public class StudentScoreCsvParser : IStudentScoreParser
    {
        public IStudentScore ReadScoreFromLine(string line)
        {
            var parts = line?.Split(',').Select(p => p.Trim()).ToArray();

            if (parts == null || parts.Length != 3)
            {
                throw new StudentScoreParsingException();
            }

            var firstName = parts[0];
            var surname = parts[1];
            var scoreParsed = uint.TryParse(parts[2], out var score);

            if (string.IsNullOrEmpty(firstName)
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
