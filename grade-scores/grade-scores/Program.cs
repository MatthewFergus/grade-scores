﻿using System;
using System.Diagnostics;
using System.Linq;

namespace GradeScores
{
    static class Program
    {
        private static void Main(string[] args)
        {
            ILogger logger;
            if (InDebugMode())
            {
                logger = new ConsoleLogger();
            }
            else
            {
                logger = new FileLogger(AppDomain.CurrentDomain.BaseDirectory);
            }

            var filePath = args.FirstOrDefault();

            if (string.IsNullOrEmpty(filePath))
            {
                logger.Log("Usage: grade-scores.exe PathToScoresFile");
                return;
            }

            var runner = new GradeRunner(filePath, new ByHighestScoreGrader(), logger);
            try
            {
                runner.Run();
            }
            catch (Exception ex)
            {
                logger.Log($"Unhandled exception occured. Details: {ex.Message}");
            }
        }

        private static bool InDebugMode()
        {
            var debugMode = false;
            Debug.Assert(debugMode = true);
            return debugMode;
        }
    }
}
