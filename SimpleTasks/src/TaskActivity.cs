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
    [Activity(Label = "Task")]
    public class TaskActivity : Activity {
        //Widgets
        TextView taskTitleView;
        TextView taskView;
        
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            //Set our view
            SetContentView(Resource.Layout.TaskActivity);

            //Assign widgets
            taskTitleView = (TextView)FindViewById(Resource.Id.taskTitleView);
            taskView = (TextView)FindViewById(Resource.Id.taskLongView);

            //Retrieve the task title and actual task
            string taskTitle = Intent.GetStringExtra("TaskTitle") ?? "Task title couldn't be retrieved.";
            string task = Intent.GetStringExtra("Task") ?? "Task couldn't be retrieved.";

            //Display information to the user
            taskTitleView.Text = taskTitle;
            taskView.Text = task;
        }
    }
}