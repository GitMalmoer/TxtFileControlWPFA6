using System;

namespace Assignment6
{
    public class Task
    {
        private DateTime _dateTime;
        private string _time;
        private PriorityType _priorityType;
        private string _taskName;

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }

        public string Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public PriorityType PriorityType
        {
            get { return _priorityType; }
            set { _priorityType = value; }
        }

        public string TaskName
        {
            get { return _taskName; }
            set { _taskName = value; }
        }

        public override string ToString()
        {
            string TaskToString = string.Format("{0,-50} {1,-50} {2,-50} {3}", _dateTime.ToString("d"), _time, _priorityType, _taskName);
            return TaskToString;
        }

    }
}
