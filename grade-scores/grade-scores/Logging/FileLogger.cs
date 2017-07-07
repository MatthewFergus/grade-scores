using System;
using System.IO;

namespace GradeScores.Logging
{
    public class FileLogger : LoggerBase
    {
        public FileLogger(string directoryPath)
        {
            filePath = Path.Combine(directoryPath, LogFileName);
        }

        private const string LogFileName = "grade-scores-log.log";

        private readonly string filePath;

        public override void Log(string message)
        {
            using (var fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine(DateTime.Now);
                writer.WriteLine(message);
            }
        }
    }
}
