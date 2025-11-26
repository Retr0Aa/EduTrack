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

            if (list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No students.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine($"{i + 1}. {list[i]}");

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private void AddStudent()
        {
            Console.Write("First Name: ");
            string fn = Console.ReadLine();

            Console.Write("Last Name: ");
            string ln = Console.ReadLine();

            Console.Write("Grade: ");
            int gr = 0;
            if (int.TryParse(Console.ReadLine(), out int tempGrade))
            {
                gr = tempGrade;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid grade!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
                return;
            }

            school.AddStudent(fn, ln, gr);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Added!");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private void DeleteStudent()
        {
            var list = school.GetStudents();
            if (list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No students.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
                return;
            }

            string[] opts = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                opts[i] = $"{list[i]}";

            List<string> menuOptions = [.. opts, "Back"];
            int selected = Menu("Choose student:", menuOptions.ToArray());
            if (selected == list.Count)
                return;

            school.DeleteStudent(selected);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Removed!");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private void ManageGrades()
        {
            var list = school.GetStudents();
            if (list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No students.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\nPress any key to return...");
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

                int c = Menu($"Grades Menu - \x1b[94m{student.FirstName} {student.LastName}", gradeMenu);
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
            Console.Write("Grade (2–6): ");
            double g = double.Parse(Console.ReadLine());
            if (g < 2 || g > 6)
            {
                Console.WriteLine("Invalid grade.");
                Console.ReadKey();
                return;
            }

            s.AddGrade(subjects[sub], double.Round(g, 2));

            Console.WriteLine("\nPress any key to return...");

            school.Save();
        }

        private void RemoveGrade(Student s)
        {
            int sub = Menu("Choose subject:", subjects);

            if (!s.Subjects.ContainsKey(subjects[sub]) ||
                s.Subjects[subjects[sub]].Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No grades.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\nPress any key to return...");
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
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private int Menu(string title, string[] items)
        {
            int index = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(title + "\n");
                Console.ForegroundColor = ConsoleColor.Gray;

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
