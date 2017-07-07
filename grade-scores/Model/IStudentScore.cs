namespace GradeScores.Model
{
    public interface IStudentScore
    {
        string FirstName { get; }
        string Surname { get; }
        uint Score { get; }
    }
}