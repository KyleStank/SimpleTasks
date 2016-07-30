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
using Android.Text;

namespace SimpleTasks {
    [Activity(Label = "Simple Tasks", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class MainActivity : Activity {
        //Widgets
        Button newTaskBtn;
        TextView noTaskTitle;

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            //Set our view
            SetContentView(Resource.Layout.MainActivity);

            //Assign widgets
            newTaskBtn = (Button)FindViewById(Resource.Id.newTaskMenuButton);
            noTaskTitle = (TextView)FindViewById(Resource.Id.noTaskTitle);
            //taskInput = (TextView)FindViewById(Resource.Id.taskInput);
            
            //Load all of the tasks
            TaskList.tasks = TaskList.LoadTaskList(Paths.taskListPath);
            
            //If there are no tasks
            if(TaskList.tasks.Count <= 0) {
                string text = GetString(Resource.String.NoTaskText);
                noTaskTitle.SetText(Html.FromHtml(text), TextView.BufferType.Spannable);
            }
            
            //Display all tasks
            foreach(Task task in TaskList.tasks)
                AddTaskToLayout(task);

            //Assign onClicks
            //"+"/new task button
            newTaskBtn.Click += delegate {
                StartActivity(typeof(NewTaskActivity));
            };
        }
        
        void AddTaskToLayout(Task task) { //Adds a task to the layout
            //Add task to the layout
            FragmentTransaction trans = FragmentManager.BeginTransaction();
            trans.Add(Resource.Id.taskListLayout, task, "Task_" + task.info.id);
            trans.Commit();
        }
    }
}
