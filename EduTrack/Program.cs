using EduTrack;
using System;

class Program
{
    public static string[] Subjects = {
        "Bulgarian",
        "Maths",
        "Geography",
        "Biology",
        "Physics",
        "Science",
        "History",
        "Programming",
        "Third Language",
        "Sports"
    };

    static void Main(string[] args)
    {
        Database db = new Database("students.json");
        School school = new School(db);
        ConsoleUI ui = new ConsoleUI(school, Subjects);

        ui.Start();
    }
}
