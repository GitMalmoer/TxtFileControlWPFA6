using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6
{
    public class FileManager
    {
        TaskManager _taskManager;
        string _token = "1.0";

        public FileManager(TaskManager taskManager)
        {
            _taskManager = taskManager;
        }


    }
}
