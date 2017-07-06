using GradeScores.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GradeScores
{
    public class ScoresFile
    {
        public ScoresFile(string filePath, IScoreParser scoreParser)
        {
            _filePath = filePath;
            _scoreParser = scoreParser;
        }

        private IScoreParser _scoreParser;
        private string _filePath;

        public IEnumerable<IStudentScore> Scores { get; set; }

        public void Read()
        {
            Scores = File.ReadAllLines(_filePath)
                .Where(l => !String.IsNullOrWhiteSpace(l))
                .Select(_scoreParser.ReadScoreFromLine)
                .ToArray();
        }

        public void Write()
        {
            //Check scores for null, throw exception
            File.WriteAllLines(_filePath, Scores.Select(_scoreParser.WriteScoreToLine));
        }

        private const string GradedSuffix = "-graded.txt";

        public static string ApplyGradedSuffix(string path)
        {
            return path.Remove(path.LastIndexOf('.')) + GradedSuffix;
        }
    }
}
