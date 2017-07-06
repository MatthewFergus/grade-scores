using GradeScores.Model;
using System.IO;

namespace GradeScores
{
    public class GradeRunner
    {
        public GradeRunner(string inputFilePath, IStudentScoreGrader grader, ILogger logger)
        {
            _inputFilePath = inputFilePath;
            _grader = grader;
            _logger = logger;
        }

        private readonly string _inputFilePath;
        private readonly IStudentScoreGrader _grader;
        private readonly ILogger _logger;

        public void Run()
        {
            var inputFile = new ScoresFile(_inputFilePath, new StudentScoreCsvParser());
            var outputFile = new ScoresFile(ScoresFile.ApplyGradedSuffix(_inputFilePath), new StudentScoreCsvParser());

            try
            {
                inputFile.Read();
            }
            catch (StudentScoreParsingException)
            {
                _logger.Log("Error parsing scores file, check file format.");
                return;
            }
            catch (IOException ex)
            {
                _logger.Log($"Error accessing scores file. Details: {ex.Message}");
                return;
            }

            var grader = new ByHighestScoreGrader();
            outputFile.Scores = grader.Grade(inputFile.Scores);

            try
            {
                outputFile.Write();
            }
            catch (IOException ex)
            {
                _logger.Log($"Error writing graded scores file. Details: {ex.Message}");
            }
        }
    }
}