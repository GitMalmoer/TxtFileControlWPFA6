using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Assignment6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TaskManager taskManager;

        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen; // Running program in center of screen
            taskManager = new TaskManager();
            InitializeComponent();
            InitializeGui();
        }

        private void InitializeGui()
        {
            this.Title = "Txt File controll by Marcin Junka";
            cmbPriority.ItemsSource = Enum.GetValues(typeof(PriorityType));
            cmbPriority.SelectedIndex = (int)PriorityType.Normal;
            EmptyInputs();
            UpdateList();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();


        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task();
            if (ValidateInputs(ref task))
            {
                taskManager.AddToTaskList(task);
                UpdateList();
            }
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
            if (dpickDate.SelectedDate.HasValue)
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
            if (Regex.IsMatch(txtTime.Text, @"\b(0[0-9]|[1][0-9]|[2][0-3]):(0[0-9]|[0-5][0-9])|24:00\b"))
            {
                ReadTimePick(ref task);
                return true;
            }
            else
                MessageBox.Show("Wrong time! the patter is NN:NN where N is a number. Remember to add also semicolon ':' use numbers from 00-24 for hours and 00-59 for minutes");
            return false;
        }

        private bool ValidateToDo(ref Task task)
        {
            if (!string.IsNullOrEmpty(txtToDo.Text))
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
            if (lstTasks.Items.Count != 0)
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
            Confirmation confirmationWindow = new Confirmation("Do you want to delete this task?");
            confirmationWindow.ShowDialog();

            if (confirmationWindow.DialogResult == true)
            {
                taskManager.RemoveTaskAtIndex(index);
                UpdateList();
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            int index = lstTasks.SelectedIndex;
            Task task = new Task();
            if (ValidateInputs(ref task))
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
            taskManager.WriteDataToFile();
        }

        private void OpenDataClick(object sender, RoutedEventArgs e)
        {
            taskManager.ReadDataFromFile();
            UpdateList();
        }

        private void NewFileClicked(object sender, RoutedEventArgs e)
        {
            taskManager.NewTaskList();
            InitializeGui();
        }

        private void SaveFileAsClick(object sender, RoutedEventArgs e)
        {
            taskManager.SaveFileAs();
        }

        private void OpenExistingClick(object sender, RoutedEventArgs e)
        {
            taskManager.OpenExistingFile();
            UpdateList();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Confirmation confirmationExit = new Confirmation("Are you sure you want to quit?");
            confirmationExit.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            confirmationExit.ShowDialog();

            if(confirmationExit.DialogResult == true)
            {
                this.Close();
            }


        }

        private void AboutClicked(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }
    }
}
