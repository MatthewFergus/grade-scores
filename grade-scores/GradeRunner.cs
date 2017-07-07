using System.IO;
using GradeScores.IO;
using GradeScores.Logging;

namespace GradeScores
{
    public class GradeRunner
    {
        public GradeRunner(string inputFilePath, IStudentScoreGrader grader, ILogger logger)
        {
            this.inputFilePath = inputFilePath;
            this.grader = grader;
            this.logger = logger;
        }

        private readonly string inputFilePath;
        private readonly IStudentScoreGrader grader;
        private readonly ILogger logger;

        public void Run()
        {
            var inputFile = new ScoresFile(inputFilePath, new StudentScoreCsvParser());
            var outputFile = new ScoresFile(ScoresFile.ApplyGradedSuffix(inputFilePath), new StudentScoreCsvParser());

            try
            {
                inputFile.Read();
            }
            catch (StudentScoreParsingException)
            {
                logger.Log("Error parsing scores file, check file format.");
                return;
            }
            catch (IOException ex)
            {
                logger.Log("Error accessing scores file.", ex);
                return;
            }
            
            outputFile.Scores = grader.Grade(inputFile.Scores);

            try
            {
                outputFile.Write();
            }
            catch (IOException ex)
            {
                logger.Log("Error writing graded scores file", ex);
            }
        }
    }
}