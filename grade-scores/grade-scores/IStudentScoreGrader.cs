using System.Collections.Generic;
using GradeScores.Model;

namespace GradeScores
{
    public interface IStudentScoreGrader
    {
        IEnumerable<IStudentScore> Grade(IEnumerable<IStudentScore> scores);
    }
}