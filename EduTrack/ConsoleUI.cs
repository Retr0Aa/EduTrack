using System;

namespace EduTrack
{
    public class ConsoleUI
    {
        private School school;
        private string[] subjects;

        public ConsoleUI(School school, string[] subjects)
        {
            this.school = school;
            this.subjects = subjects;
        }

        public void Start()
        {
            string[] main = { "Show Students", "Add Student", "Delete Student", "Manage Grades", "Exit" };

            while (true)
            {
                int c = Menu("=== EduTrack ===", main);
                Console.Clear();

                if (c == 0) ShowStudents();
                if (c == 1) AddStudent();
                if (c == 2) DeleteStudent();
                if (c == 3) ManageGrades();
                if (c == 4) return;
            }
        }

        private void ShowStudents()
        {
            var list = school.GetStudents();
            Console.WriteLine("--- Students ---\n");

            if (list.Count == 0) Console.WriteLine("No students.");
            else
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine($"{i + 1}. {list[i]}");

            Console.ReadKey();
        }

        private void AddStudent()
        {
            Console.Write("First Name: ");
            string fn = Console.ReadLine();

            Console.Write("Last Name: ");
            string ln = Console.ReadLine();

            Console.Write("Grade: ");
            int gr = int.Parse(Console.ReadLine());

            school.AddStudent(fn, ln, gr);
            Console.WriteLine("Added!");
            Console.ReadKey();
        }

        private void DeleteStudent()
        {
            var list = school.GetStudents();
            if (list.Count == 0)
            {
                Console.WriteLine("No students.");
                Console.ReadKey();
                return;
            }

            string[] opts = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                opts[i] = $"{list[i]}";

            int selected = Menu("Choose student:", opts);
            school.DeleteStudent(selected);

            Console.WriteLine("Removed!");
            Console.ReadKey();
        }

        private void ManageGrades()
        {
            var list = school.GetStudents();
            if (list.Count == 0)
            {
                Console.WriteLine("No students.");
                Console.ReadKey();
                return;
            }

            string[] opts = new string[list.Count];
            for (int i = 0; i < list.Count; i++) opts[i] = list[i].ToString();

            int sIndex = Menu("Select student:", opts);
            var student = list[sIndex];

            string[] gradeMenu = { "Add Grade", "Remove Grade", "View Grades", "Back" };
            while (true)
            {
                int c = Menu("Grades Menu", gradeMenu);
                Console.Clear();

                if (c == 0) AddGrade(student);
                if (c == 1) RemoveGrade(student);
                if (c == 2) ViewGrades(student);
                if (c == 3) return;
            }
        }

        private void AddGrade(Student s)
        {
            int sub = Menu("Choose subject:", subjects);
            Console.Write("Grade (1–6): ");
            int g = int.Parse(Console.ReadLine());

            s.AddGrade(subjects[sub], g);
            school.Save();
        }

        private void RemoveGrade(Student s)
        {
            int sub = Menu("Choose subject:", subjects);

            if (!s.Subjects.ContainsKey(subjects[sub]) ||
                s.Subjects[subjects[sub]].Count == 0)
            {
                Console.WriteLine("No grades.");
                Console.ReadKey();
                return;
            }

            var list = s.Subjects[subjects[sub]];
            string[] opt = new string[list.Count];
            for (int i = 0; i < list.Count; i++) opt[i] = list[i].ToString();

            int index = Menu("Choose grade to remove:", opt);

            s.RemoveGrade(subjects[sub], index);
            school.Save();
        }

        private void ViewGrades(Student s)
        {
            foreach (var pair in s.Subjects)
            {
                Console.Write($"{pair.Key}: ");
                Console.WriteLine(string.Join(", ", pair.Value));
            }
            Console.ReadKey();
        }

        private int Menu(string title, string[] items)
        {
            int index = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine(title + "\n");

                for (int i = 0; i < items.Length; i++)
                {
                    if (i == index)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(items[i]);
                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && index > 0) index--;
                if (key == ConsoleKey.DownArrow && index < items.Length - 1) index++;

            } while (key != ConsoleKey.Enter);

            return index;
        }
    }
}
