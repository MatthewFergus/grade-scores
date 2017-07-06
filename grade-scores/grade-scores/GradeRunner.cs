using GradeScores.Model;

namespace GradeScores
{
    public class GradeRunner
    {
        public GradeRunner(string inputFilePath, IStudentScoreGrader grader)
        {
            _inputFilePath = inputFilePath;
            _grader = grader;
        }

        private readonly string _inputFilePath;
        private readonly IStudentScoreGrader _grader;

        public void Run()
        {
            // try catch. catch domain/io ex separately
            var inputFile = new ScoresFile(_inputFilePath);
            var outputFile = new ScoresFile(ScoresFile.ApplyGradedSuffix(_inputFilePath));

            try
            {
                inputFile.Read();
            }
            catch
            {

            }

            var grader = new ByHighestScoreGrader();
            outputFile.Scores = grader.Grade(inputFile.Scores);

            try
            {
                outputFile.Write();
            }
            catch
            {

            }
        }
    }
}