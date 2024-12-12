using Newtonsoft.Json;
using NoteTakerServer.Models;

namespace NoteTakerServer.DataStorage
{
    public class FileStorage
    {
        private static readonly string UserFile = Path.Combine("DataStorage", "user.json");
        private static readonly string NoteFile = Path.Combine("DataStorage", "note.json");

        static FileStorage()
        {
            Directory.CreateDirectory("DataStorage");
            if (!File.Exists(UserFile))
            {
                File.WriteAllText(UserFile, JsonConvert.SerializeObject(new List<User>(), Formatting.Indented));
            }

            // Ensure notes.json exists
            if (!File.Exists(NoteFile))
            {
                File.WriteAllText(NoteFile, JsonConvert.SerializeObject(new List<Note>(), Formatting.Indented));
            }
        }
        public static List<User> LoadUsers()
        {
            var jsonData = File.ReadAllText(UserFile);
            return JsonConvert.DeserializeObject<List<User>>(jsonData) ?? new List<User>();
        }
        public static List<User>  SaveUsers(List<User> users)
        {
            var jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(UserFile, jsonData);
            return users;
        }

        public static List<Note> LoadNotes()
        {
            var jsonData = File.ReadAllText(NoteFile);
            return JsonConvert.DeserializeObject<List<Note>>(jsonData) ?? new List<Note>();
        }
        public static void SaveNotes(List<Note> notes)
        {
            var jsonData = JsonConvert.SerializeObject(notes, Formatting.Indented);
            File.WriteAllText(NoteFile, jsonData);
        }
    }
}
