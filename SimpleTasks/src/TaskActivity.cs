using Android.App;
using Android.Content;
using Android.OS;
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
            taskTitleView = (TextView)FindViewById(Resource.Id.ViewTask_Title);
            taskView = (TextView)FindViewById(Resource.Id.ViewTask_Text);

            //Retrieve the task title and actual task
            string taskTitle = Intent.GetStringExtra("TaskTitle") ?? "Task title couldn't be retrieved.";
            string task = Intent.GetStringExtra("Task") ?? "Task couldn't be retrieved.";

            //Display information to the user
            taskTitleView.Text = taskTitle;
            taskView.Text = task;
        }
    }
}