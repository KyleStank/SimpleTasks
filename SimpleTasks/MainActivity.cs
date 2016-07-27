using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using System.IO;
using Newtonsoft.Json;

namespace SimpleTasks {
    [Activity(Label = "Simple Tasks", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity {
        //Widgets
        Button addTaskBtn;
        TextView taskInput;

        string filePath;

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Set up saving strings
            filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            filePath = Path.Combine(filePath, "tasks.json");

            //Load all of the tasks
            TaskList.tasks = TaskList.LoadTaskList(filePath);
            
            //Display all tasks
            foreach(Task task in TaskList.tasks)
                AddTaskToLayout(task);

            //Assign widgets
            addTaskBtn = (Button)FindViewById(Resource.Id.addTaskButton);
            taskInput = (TextView)FindViewById(Resource.Id.taskInput);

            //Assign onClicks
            //Add task button
            if(addTaskBtn != null)
                addTaskBtn.Click += (object sender, EventArgs e) => {
                    if(taskInput != null) {
                        //Get the task's text
                        string text = taskInput.Text;

                        //Makes sure that an empty task isn't being added
                        if(!string.IsNullOrWhiteSpace(text)) {
                            //Create an id
                            int id = (TaskList.tasks.Count + 1);

                            //Create task
                            Task task = new Task(id, text, false);

                            //Add task
                            TaskList.tasks.Add(task);
                            AddTaskToLayout(task);
                            TaskList.SaveTaskList(filePath);

                            //Reset the input text
                            taskInput.Text = "";
                        }
                    }
                };
        }

        void AddTaskToLayout(Task task) { //Adds a task to the layout
            //Add task to the list
            //TaskList.tasks.Add(task);

            //Add task to the layout
            FragmentTransaction trans = FragmentManager.BeginTransaction();
            trans.Add(Resource.Id.bottomLayout, task, "Task_" + task.info.id);
            trans.Commit();
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

