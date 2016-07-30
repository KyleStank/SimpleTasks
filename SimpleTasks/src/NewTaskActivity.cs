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

namespace SimpleTasks {
    [Activity(Label = "New Task")]
    public class NewTaskActivity : Activity {
        //Widgets
        Button addTaskBtn;
        EditText taskTitleField;
        EditText taskField;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            //Set our view
            SetContentView(Resource.Layout.NewTaskActivity);

            //Assign widgets
            addTaskBtn = (Button)FindViewById(Resource.Id.addTaskButton);
            taskTitleField = (EditText)FindViewById(Resource.Id.taskTitle);
            taskField = (EditText)FindViewById(Resource.Id.taskLong);

            //Assign onClicks
            //Add task button
            addTaskBtn.Click += delegate {
                string taskTitle;
                string task;

                //Get the task title and the actual task
                taskTitle = taskTitleField.Text;
                task = taskField.Text;

                //If title and task aren't empty
                if(!string.IsNullOrWhiteSpace(taskTitle) && !string.IsNullOrWhiteSpace(task)) {
                    //Add task to the list
                    TaskList.tasks.Add(new Task(taskTitle, task));

                    //Save the task list
                    TaskList.SaveTaskList(Paths.taskListPath);

                    //Go back to the main activity
                    StartActivity(typeof(MainActivity));
                }
            };
        }
    }
}