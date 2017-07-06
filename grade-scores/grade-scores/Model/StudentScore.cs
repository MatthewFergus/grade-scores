using System;

namespace GradeScores.Model
{
    public class StudentScore : IStudentScore
    {
        public StudentScore(string firstName, string surname, uint score)
        {
            FirstName = firstName;
            Surname = surname;
            Score = score;
        }

        public string FirstName { get; }
        public string Surname { get; }
        public uint Score { get; }
    }
}