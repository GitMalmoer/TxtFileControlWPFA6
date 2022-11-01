using System;
using System.Collections.Generic;
using System.Windows;

namespace Assignment6
{
    public class TaskManager
    {
        List<Task> _taskList;
        private string _exePath = Environment.CurrentDirectory + @"\Tasks.txt";

        public TaskManager()
        {
            _taskList = new List<Task>();
        }


        public void AddToTaskList(Task task)
        {
            if (task != null)
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
            if (CheckIndex(index))
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
            if (CheckIndex(index) && task != null)
            {
                _taskList[index] = task;
            }
            else if (task == null)
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

        public bool WriteDataToFile()
        {
            FileManager fileManager = new FileManager();
            return fileManager.SaveToTxt(_taskList, _exePath);
        }


        public bool ReadDataFromFile()
        {
            FileManager fileManager = new FileManager();
            return fileManager.ReadFromTxt(_taskList, _exePath);
        }

        public void NewTaskList()
        {
            if (_taskList != null)
            {
                _taskList.Clear();
            }
            else
            {
                _taskList = new List<Task>();
            }
        }

        public bool SaveFileAs()
        {
            FileManager fileManager = new FileManager();
            return fileManager.SaveFileAs(_taskList);
        }

        public bool OpenExistingFile()
        {
            FileManager fileManager = new FileManager();
            return fileManager.OpenExistingFile(_taskList);
        }

    }
}
