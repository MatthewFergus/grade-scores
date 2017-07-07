using System;

namespace GradeScores.IO
{
    public class StudentScoreParsingException : Exception
    {
        public StudentScoreParsingException()
        {
        }

        public StudentScoreParsingException(string message)
            : base(message)
        {
        }

        public StudentScoreParsingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
