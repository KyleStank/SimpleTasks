using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.IO;
using Newtonsoft.Json;

namespace SimpleTasks {
    public class TaskInfo {
        public int id;
        public string taskTitle;
        public string task;

        public TaskInfo(string taskTitle, string task) {
            id = TaskList.tasks.Count + 1;
            this.taskTitle = taskTitle;
            this.task = task;
        }
    }

    public class Task : Fragment {
        public TaskInfo info;

        //Widgets
        CheckBox taskCheckbox;
        TextView taskSummary;

        public Task(string taskTitle, string task) { //Main construtor
            info = new TaskInfo(taskTitle, task);
        }

        public Task(TaskInfo info) { //If we already have information that can be used
            this.info = info;
        }

        public override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            View v = inflater.Inflate(Resource.Layout.TaskFragment, container, false);

            //Assign widgets
            taskCheckbox = (CheckBox)v.FindViewById(Resource.Id.taskCompleteCheckbox);
            taskSummary = (TextView)v.FindViewById(Resource.Id.taskSummaryText);

            //Display the task title
            taskSummary.Text = info.taskTitle;

            //Handle onClicks
            //Checkbox
            taskCheckbox.Click += delegate {
                RemoveTask();
            };

            //Task title
            taskSummary.Click += delegate {
                //Pass information to the Task activity
                Intent intent = new Intent(Context, typeof(TaskActivity));
                intent.PutExtra("TaskTitle", info.taskTitle);
                intent.PutExtra("Task", info.task);

                //Start task activity
                StartActivity(intent);
            };

            return v;
        }

        void RemoveTask() { //Removes this task from the list and the layout
            //Remove task from the list
            TaskList.tasks.Remove(this);

            //Remove task from the layout
            FragmentTransaction trans = FragmentManager.BeginTransaction();
            trans.Remove(this);
            trans.Commit();

            TaskList.SaveTaskList(Paths.taskListPath);
        }
    }

    public static class TaskList {
        //The task list
        public static List<Task> tasks = new List<Task>();

        public static void SaveTaskList(string filePath) { //Saves the task list
            TaskInfo[] info = new TaskInfo[tasks.Count];

            //Loop through every task
            for(int i = 0; i < tasks.Count; i++)
                info[i] = tasks[i].info;

            //Serialize task info into a JSON file
            string json = JsonConvert.SerializeObject(info, Formatting.Indented);

            //Write JSON data to file
            File.WriteAllText(filePath, json);
        }

        public static List<Task> LoadTaskList(string filePath) { //Loads the task list
            List<Task> tasks = new List<Task>();

            //If save file exists
            if(File.Exists(filePath)) {
                TaskInfo[] infoList = JsonConvert.DeserializeObject<TaskInfo[]>(File.ReadAllText(filePath));

                //Loop through all the information from the file
                foreach(TaskInfo info in infoList)
                    tasks.Add(new Task(info));
            }

            return tasks;
        }
    }
}
 