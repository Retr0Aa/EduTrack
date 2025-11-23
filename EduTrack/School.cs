using System.Collections.Generic;
using System.Linq;

namespace EduTrack
{
    public class School
    {
        private Database db;

        public School(Database database)
        {
            db = database;
        }

        public List<Student> GetStudents() => db.Students;

        public void AddStudent(string first, string last, int grade)
        {
            db.Students.Add(new Student
            {
                FirstName = first,
                LastName = last,
                Grade = grade
            });

            db.Save();
        }

        public void DeleteStudent(int index)
        {
            if (index >= 0 && index < db.Students.Count)
            {
                db.Students.RemoveAt(index);
                db.Save();
            }
        }

        public Student Find(string first, string last)
        {
            return db.Students.FirstOrDefault(s =>
                s.FirstName == first && s.LastName == last);
        }

        public void Save() => db.Save();
    }
}
