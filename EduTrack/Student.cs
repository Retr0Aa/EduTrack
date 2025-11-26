using System.Collections.Generic;

namespace EduTrack
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Grade { get; set; }
        public Dictionary<string, List<double>> Subjects { get; set; } = new();

        public override string ToString()
        {
            return $"{FirstName} {LastName} | Grade: {Grade}";
        }

        public void AddGrade(string subject, double grade)
        {
            if (!Subjects.ContainsKey(subject))
                Subjects[subject] = new List<double>();

            Subjects[subject].Add(grade);
        }

        public void RemoveGrade(string subject, int index)
        {
            if (Subjects.ContainsKey(subject) && index >= 0 && index < Subjects[subject].Count)
                Subjects[subject].RemoveAt(index);
        }
    }
}
