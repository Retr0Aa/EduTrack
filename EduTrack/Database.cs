using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EduTrack
{
    public class Database
    {
        private string filePath;
        public List<Student> Students { get; set; }

        public Database(string filePath)
        {
            this.filePath = filePath;

            if (File.Exists(filePath))
            {
                Students = JsonSerializer.Deserialize<List<Student>>(File.ReadAllText(filePath));
            }
            else
            {
                Students = new List<Student>();
                Save();
            }
        }

        public void Save()
        {
            File.WriteAllText(filePath,
                JsonSerializer.Serialize(Students, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
