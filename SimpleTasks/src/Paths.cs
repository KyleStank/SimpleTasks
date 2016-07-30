using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;

namespace SimpleTasks {
    public static class Paths {
        //Task list path
        static string taskListFileName = "tasks.json";
        public static string taskListPath = Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), taskListFileName);
    }
}