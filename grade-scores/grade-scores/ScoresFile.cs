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
            File.WriteAllLines(_filePath, Scores.Select(FormatCsvString));
        }

        private IStudentScore ReadScoreFromLine(string line)
        {
            try
            {
                var parts = line.Split(',').Select(p => p.Trim()).ToArray();
                return new StudentScore(parts[0], parts[1], uint.Parse(parts[2]));
            }
            catch
            {
                // handle with DomainException ie InvalidScoresFileException - pass in innerexception
                throw;
            }
        }

        private static string FormatCsvString(IStudentScore score)
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
