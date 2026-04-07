# EduTrack

EduTrack is a simple C# console application for managing students and their grades. It lets you add and remove students, assign grades by subject, view saved grades, and persist everything in a local JSON file.

## Features

- Add students with first name, last name, and class grade
- View all saved students
- Delete existing students
- Add grades for multiple school subjects
- Remove grades from a subject
- Store data locally in `students.json`
- Navigate the app through a keyboard-driven console menu

## Built With

- C#
- .NET 10 console application
- `System.Text.Json` for local data storage

## Subjects Included

The application currently supports these subjects:

- Bulgarian
- Maths
- Geography
- Biology
- Physics
- Science
- History
- Programming
- Third Language
- Sports

## Getting Started

### Prerequisites

- .NET SDK 10.0 or newer installed

### Run the project

From the repository root:

```bash
dotnet run --project EduTrack
```

## How It Works

When the app starts, it loads student data from `students.json`. If the file does not exist yet, the application creates it automatically.

In the main menu you can:

- Show all students
- Add a new student
- Delete a student
- Manage grades for a selected student
- Exit the program

Inside the grade management menu you can:

- Add a grade for a subject
- Remove an existing grade
- View all saved grades for the selected student

Use the arrow keys to move through the menus and press `Enter` to choose an option.

## Data Storage

Student records are saved in a local `students.json` file in the current working directory when the app runs. This makes the project easy to test and use without a separate database.

## Project Structure

```text
EduTrack/
├── README.md
├── EduTrack.slnx
└── EduTrack/
    ├── Program.cs
    ├── ConsoleUI.cs
    ├── School.cs
    ├── Student.cs
    ├── Database.cs
    └── EduTrack.csproj
```

## Notes

- Grades are expected to be in the range `2` to `6`
- Student and grade changes are saved to the JSON file
- This project uses a console interface, so it is best run in a terminal

## Future Improvements

- Input validation for all user fields
- Average grade calculation per student
- Search students by name
- Better error handling for invalid grade input
- Edit student details

## License

This project is for educational purposes.
