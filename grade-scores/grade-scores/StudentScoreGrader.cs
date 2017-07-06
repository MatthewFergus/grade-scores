using System;
using System.Linq;
using System.Collections.Generic;
using GradeScores.Model;

namespace GradeScores
{
    public class ByHighestScoreGrader : IStudentScoreGrader
    {
        public IEnumerable<IStudentScore> Grade(IEnumerable<IStudentScore> scores)
        {
            return scores.OrderByDescending(x => x.Score)
                .ThenBy(x => x.Surname, StringComparer.InvariantCultureIgnoreCase)
                .ThenBy(x => x.FirstName, StringComparer.InvariantCultureIgnoreCase);
        }
    }
}