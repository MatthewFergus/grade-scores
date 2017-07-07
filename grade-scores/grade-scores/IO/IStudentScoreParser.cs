using GradeScores.Model;

namespace GradeScores.IO
{
    public interface IStudentScoreParser
    {
        IStudentScore ReadScoreFromLine(string line);

        string WriteScoreToLine(IStudentScore score);
    }
}
