using System.IO;

namespace SimpleTasks {
    public static class Paths {
        //Task list path
        static string taskListFileName = "tasks.json";
        public static string taskListPath = Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), taskListFileName);
    }
}