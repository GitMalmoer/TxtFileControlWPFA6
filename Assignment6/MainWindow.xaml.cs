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
            InitializeComponent();
            InitializeGui();
            fileManager = new FileManager();
            taskManager = new TaskManager();
        }

        private void InitializeGui()
        {
            cmbPriority.ItemsSource = Enum.GetValues(typeof(PriorityType));
            cmbPriority.SelectedIndex = (int)PriorityType.Normal;
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
            lstTasks.Items.Clear();
            for(int i = 0; i<taskManager.Count;i++)
            {
                lstTasks.Items.Add(taskManager.GetTaskAtIndex(i).ToString());
            }
        }


    }
}
