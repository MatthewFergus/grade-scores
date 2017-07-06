using System.IO;

namespace GradeScores
{
    public class FileLogger : ILogger
    {
        public FileLogger(string directoryPath)
        {
            _filePath = Path.Combine(directoryPath, LogFileName);
        }

        private const string LogFileName = "grade-scores-log.log";

        private string _filePath;

        public void Log(string message)
        {
            using (FileStream fs = new FileStream(_filePath, FileMode.Append, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine(message);
            }
        }
    }
}
