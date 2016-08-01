using Android.App;
using Android.Widget;
using Android.OS;
using Android.Text;
using Android.Content;
using Android.Views;

namespace SimpleTasks {
    [Activity(Label = "Simple Tasks", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class MainActivity : Activity {
        //Widgets
        TextView noTaskTitle;
        ListView taskListView;
        Button newTaskButton;

        //List view stuff
        TaskListAdapter adapter;

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            //Set our view
            SetContentView(Resource.Layout.MainActivity);

            //Assign widgets
            noTaskTitle = (TextView)FindViewById(Resource.Id.TaskListEmpty_Title);
            taskListView = (ListView)FindViewById(Resource.Id.TaskList_View);
            newTaskButton = (Button)FindViewById(Resource.Id.NewTask_Button);

            //Load all of the tasks
            TaskList.tasks = TaskList.LoadTaskList(Paths.taskListPath);

            //Set the empty task list text
            string text = GetString(Resource.String.NoTaskText);
            noTaskTitle.SetText(Html.FromHtml(text), TextView.BufferType.Spannable);
            ToggleEmptyListText(ViewStates.Invisible);

            //If there are no tasks
            if(TaskList.tasks.Count <= 0) {
                ToggleEmptyListText(ViewStates.Visible);
            } else {
                //Create the adapter
                adapter = new TaskListAdapter(this, TaskList.tasks);

                //Set the adapter
                taskListView.Adapter = adapter;

                //Set the onClick method
                taskListView.ItemClick += TaskList_OnClick;

                //Register to the context menu
                RegisterForContextMenu(taskListView);
            }

            //Assign onClicks
            //"+"/new task button
            newTaskButton.Click += delegate { AddTaskButton_OnClick(); };
        }
        
        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo) {
            if(v.Id == Resource.Id.TaskList_View) {
                //Create menu info variable
                AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo)menuInfo;

                //Get the task information
                string title = TaskList.tasks[info.Position].info.taskTitle;
                string task = TaskList.tasks[info.Position].info.task;
                int maxTitleCount = 20;

                //Make title shorter if too long
                if(title.Length > maxTitleCount)
                    title = title.Substring(0, maxTitleCount) + "...";

                //Set up the menu
                menu.SetHeaderTitle(title);
                menu.Add(Resource.String.task_fragment_context_menu_view);
                menu.Add(Resource.String.task_fragment_context_menu_delete);
            }
        }

        public override bool OnContextItemSelected(IMenuItem item) {
            //Craete menu info variable
            AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;

            //Go through the context menu items
            if(item.TitleFormatted.ToString() == GetString(Resource.String.task_fragment_context_menu_view)) {
                //View the task
                ViewTask(info.Position);
            } else {
                //Remove the task
                RemoveTask(info.Position, true);
            }

            return true;
        }

        //Task methods
        void ToggleEmptyListText(ViewStates visibility) { //Shows the task list empty text
            noTaskTitle.Visibility = visibility;
        }

        void ViewTask(int position) { //Views a task
            //Pass information to the TaskActivity
            Intent intent = new Intent(this, typeof(TaskActivity));
            intent.PutExtra("TaskTitle", TaskList.tasks[position].info.taskTitle);
            intent.PutExtra("Task", TaskList.tasks[position].info.task);

            //Start task activity
            StartActivity(intent);
        }

        void RemoveTask(int position, bool showEmptyText) { //Removes a tasks
            //Remove item from list, and then save the list
            TaskList.tasks.RemoveAt(position);
            TaskList.SaveTaskList(Paths.taskListPath);
            
            //Update the list view
            adapter.NotifyDataSetChanged();

            //If task is now empty, show no task text
            if(showEmptyText && TaskList.tasks.Count <= 0)
                ToggleEmptyListText(ViewStates.Visible);
        }

        //onClick methods
        void AddTaskButton_OnClick() { //When user wants to create a new task
            StartActivity(typeof(NewTaskActivity));
        }

        void TaskList_OnClick(object sender, AdapterView.ItemClickEventArgs e) { //If a single task gets clicked
            ViewTask(e.Position);
        }
    }
}
