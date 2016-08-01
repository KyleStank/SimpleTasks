using System.Collections.Generic;

using Android.Content;
using Android.Views;
using Android.Widget;

namespace SimpleTasks {
    public class TaskListAdapter : BaseAdapter<Task> {
        Context context;
        List<Task> list = new List<Task>();

        public TaskListAdapter(Context context, List<Task> list) {
            this.context = context;
            this.list = list;
        }

        public override int Count {
            get {
                return TaskList.tasks.Count;
            }
        }

        public override long GetItemId(int position) {
            return position;
        }

        public override Task this[int position] {
            get {
                return list[position];
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent) {
            View row = convertView;

            //Makes sure that item exists
            if(row == null)
                row = LayoutInflater.From(context).Inflate(Resource.Layout.TaskRow, null, false);

            //Set the title
            TextView title = (TextView)row.FindViewById(Resource.Id.TaskList_Title);
            title.Text = list[position].info.taskTitle;

            return row;
        }
    }
}