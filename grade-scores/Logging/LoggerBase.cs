using System;

namespace GradeScores.Logging
{
    public abstract class LoggerBase : ILogger
    {
        public abstract void Log(string message);

        public virtual void Log(string message, Exception ex)
        {
            Log($"{message}{Environment.NewLine}{ex}");
        }
    }
}
