using System;

namespace GradeScores.Logging
{
    public interface ILogger
    {
        void Log(string message);

        void Log(string message, Exception ex);
    }
}
