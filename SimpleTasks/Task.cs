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

namespace SimpleTasks {
    public class TaskInfo {
        public int id;
        public string task;
        public bool isCompleted;

        public TaskInfo(int id, string task, bool completed) {
            this.id = id;
            this.task = task;
            isCompleted = completed;
        }
    }

    public class Task : Fragment {
        public TaskInfo info;

        //Widgets
        CheckBox taskCheckbox;

        string filePath;

        public Task(int id, string task, bool completed) { //Main construtor
            info = new TaskInfo(id, task, completed);
        }

        public Task(TaskInfo info) { //If we already have information that can be used
            this.info = info;
        }

        public override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            //Set up saving strings
            filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            filePath = Path.Combine(filePath, "tasks.json");
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            View v = inflater.Inflate(Resource.Layout.BottomFragment, container, false);

            //Set u checkbox
            taskCheckbox = (CheckBox)v.FindViewById(Resource.Id.task);
            if(taskCheckbox != null) {
                taskCheckbox.Text = info.task;

                //When it gets pressed
                taskCheckbox.Click += (object sender, EventArgs e) => {
                    RemoveTask();
                };
            }

            return v;
        }

        void RemoveTask() { //Removes this task from the list and the layout
            //Remove task from the list
            TaskList.tasks.Remove(this);

            //Remove task from the layout
            FragmentTransaction trans = FragmentManager.BeginTransaction();
            trans.Remove(this);
            trans.Commit();

            TaskList.SaveTaskList(filePath);
        }
    }
}