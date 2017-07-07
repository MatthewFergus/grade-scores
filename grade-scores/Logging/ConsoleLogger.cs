using System;

namespace GradeScores.Logging
{
    public class ConsoleLogger : LoggerBase
    {
        public override void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
