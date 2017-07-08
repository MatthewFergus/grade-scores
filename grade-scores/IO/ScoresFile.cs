using System.Collections.Generic;
using System.IO;
using System.Linq;
using GradeScores.Model;

namespace GradeScores.IO
{
    public class ScoresFile
    {
        public ScoresFile(string filePath, IStudentScoreParser studentScoreParser)
        {
            this.filePath = filePath;
            this.studentScoreParser = studentScoreParser;
        }

        private readonly IStudentScoreParser studentScoreParser;
        private readonly string filePath;

        public IEnumerable<IStudentScore> Scores { get; set; }

        public void Read()
        {
            Scores = File.ReadAllLines(filePath)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(studentScoreParser.ReadScoreFromLine)
                .ToArray();
        }

        public void Write()
        {
            File.WriteAllLines(filePath, Scores.Select(studentScoreParser.WriteScoreToLine));
        }

        private const string GradedSuffix = "-graded.txt";

        public static string ApplyGradedSuffix(string path)
        {
            var removeIndex = path.LastIndexOf('.');
            return removeIndex == -1 ? path + GradedSuffix : path.Remove(removeIndex) + GradedSuffix;
        }
    }
}
