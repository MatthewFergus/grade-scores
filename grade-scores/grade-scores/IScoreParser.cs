using GradeScores.Model;

namespace GradeScores
{
    public interface IScoreParser
    {
        IStudentScore ReadScoreFromLine(string line);

        string WriteScoreToLine(IStudentScore score);
    }
}
