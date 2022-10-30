using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileManager fileManager;
        TaskManager taskManager;

        public MainWindow()
        {
            taskManager = new TaskManager();
            //fileManager = new FileManager(taskManager);
            InitializeComponent();
            InitializeGui();
        }

        private void InitializeGui()
        {
            cmbPriority.ItemsSource = Enum.GetValues(typeof(PriorityType));
            cmbPriority.SelectedIndex = (int)PriorityType.Normal;
            UpdateList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task();
            if(ValidateInputs(ref task))
            {
                taskManager.AddToTaskList(task);
                UpdateList();
            }
            
        }

        private void cmbPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private bool ValidateInputs(ref Task task)
        {
            if (ValidateDatePick(ref task) && ValidateTimePick(ref task) && ValidateToDo(ref task))
            {
                task.PriorityType = (PriorityType)cmbPriority.SelectedIndex;
                return true;
            }
            else
                return false;
        }
        private bool ValidateDatePick(ref Task task)
        {
            if(dpickDate.SelectedDate.HasValue)
            {
                ReadDatePick(ref task);
                return true;
            }
            else
            {
                MessageBox.Show("You need to select good date.");
                return false;
            }
        }
        
        private bool ValidateTimePick(ref Task task)
        {
            // Winforms has dateandtimepick in wpf we dont have that so i needed to make custom time picker
            if (Regex.IsMatch(txtTime.Text, @"\A\d\d:\d\d\z"))
            {
                ReadTimePick(ref task);
                return true;
            }
            else
                MessageBox.Show("Wrong time! the patter is NN:NN where N is a number. Remember to add also semicolon ':'");
                return false;
        }

        private bool ValidateToDo(ref Task task)
        {
            if(!string.IsNullOrEmpty(txtToDo.Text))
            {
                ReadToDo(ref task);
                return true;
            }
            else
            {
                MessageBox.Show("You need to write to do!");
                return false;
            }
        }

        private Task ReadDatePick(ref Task task)
        {
            task.DateTime = dpickDate.SelectedDate.Value;
            return task;
        }

        private Task ReadTimePick(ref Task task)
        {
            // Winforms has dateandtimepick in wpf we dont have that so i needed to make custom time picker
            task.Time = txtTime.Text;
            return task;
        }

        private Task ReadToDo(ref Task task)
        {
            task.TaskName = txtToDo.Text;
            return task;
        }

        private void UpdateList()
        {
            if (taskManager.Count >= 0) // checks if there are items in taskmanager
            {
                lstTasks.Items.Clear();
                for (int i = 0; i < taskManager.Count; i++)
                {
                    lstTasks.Items.Add(taskManager.GetTaskAtIndex(i).ToString());
                }
            }

            // disables buttons if there is no items in list
            if(lstTasks.Items.Count != 0)
            {
                btnDelete.IsEnabled = true;
                btnChange.IsEnabled = true;
            }
            else
            {
                btnDelete.IsEnabled = false;
                btnChange.IsEnabled = false;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
                int index = lstTasks.SelectedIndex;
                taskManager.RemoveTaskAtIndex(index);
                UpdateList();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            int index = lstTasks.SelectedIndex;
            Task task = new Task();
            if(ValidateInputs(ref task))
            {
                taskManager.ChangeTaskAtIndex(task, index);
                UpdateList();
                EmptyInputs();
                MessageBox.Show("Task has been changed!");
            }
        }

        private void lstTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lstTasks.SelectedIndex;
            if (taskManager.GetTaskAtIndex(index) != null)
            {
                txtToDo.Text = taskManager.GetTaskAtIndex(index).TaskName;
                dpickDate.SelectedDate = taskManager.GetTaskAtIndex(index).DateTime;
                txtTime.Text = taskManager.GetTaskAtIndex(index).Time;
                cmbPriority.SelectedIndex = (int)taskManager.GetTaskAtIndex(index).PriorityType;
            }
        }

        private void EmptyInputs()
        {
            txtToDo.Text = String.Empty;
            dpickDate.SelectedDate = default;
            txtTime.Text = String.Empty;
            cmbPriority.SelectedIndex = (int)PriorityType.Normal;

        }

        private void SaveFileClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
