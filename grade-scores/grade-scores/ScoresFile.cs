using GradeScores.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GradeScores
{
    public class ScoresFile
    {
        public ScoresFile(string filePath)
        {
            _filePath = filePath;
        }

        private string _filePath;

        public IEnumerable<IStudentScore> Scores { get; set; }

        public void Read()
        {
            Scores = File.ReadAllLines(_filePath)
                .Where(l => !String.IsNullOrWhiteSpace(l))
                .Select(ReadScoreFromLine)
                .ToArray();
        }

        public void Write()
        {
            //Check scores for null, throw exception
            File.WriteAllLines(_filePath, Scores.Select(WriteScoreToLine));
        }

        private IStudentScore ReadScoreFromLine(string line)
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

        private static string WriteScoreToLine(IStudentScore score)
        {
            return $"{score.Surname}, {score.FirstName}, {score.Score}";
        }

        private const string GradedSuffix = "-graded.txt";

        public static string ApplyGradedSuffix(string path)
        {
            return path.Remove(path.LastIndexOf('.')) + GradedSuffix;
        }
    }
}
