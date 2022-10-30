using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment6
{
    public class TaskManager
    {
        List<Task> _taskList;

        public TaskManager()
        {
            _taskList = new List<Task>();
        }

        

        public void AddToTaskList(Task task)
        {
            if(task != null)
            _taskList.Add(task);
        }

        public Task GetTaskAtIndex(int index)
        {
            if (CheckIndex(index))
            {
                return _taskList[index];
            }
            else
                return null;
        }

        public void RemoveTaskAtIndex(int index)
        {
            if(CheckIndex(index))
            {
                _taskList.RemoveAt(index);
            }
            else
            {
                MessageBox.Show("Invalid selection");
            }
        }

        public void ChangeTaskAtIndex(Task task, int index)
        {
            if(CheckIndex(index) && task != null)
            {
                _taskList[index] = task;
            }
            else if(task == null)
            {
                MessageBox.Show("Error");
            }
            else
            {
                MessageBox.Show("Wrong selection");
            }
        }

        public int Count
        {
            get { return _taskList.Count; }
        }

        public bool CheckIndex(int index)
        {
            if (index >= 0 && index < Count)
            {
                return true;
            }
            else
                return false;
        }



    }
}
