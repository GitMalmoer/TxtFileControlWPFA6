using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Assignment6
{
    public class FileManager
    {
        string _token = "1.0";
        string _version = "TaskManager_v1";

        public FileManager()
        {

        }

        public bool SaveToTxt(List<Task> taskList, string exePath)
        {
            bool saveOk = true;
            StreamWriter streamWriter = null;

            try
            {
                streamWriter = new StreamWriter(exePath);
                streamWriter.WriteLine(_token);
                streamWriter.WriteLine(_version);
                streamWriter.WriteLine(taskList.Count);

                for (int i = 0; i < taskList.Count; i++)
                {
                    streamWriter.WriteLine(taskList[i].DateTime.ToString("d"));
                    streamWriter.WriteLine(taskList[i].Time);
                    streamWriter.WriteLine(taskList[i].PriorityType.ToString());
                    streamWriter.WriteLine(taskList[i].TaskName);
                }

            }
            catch
            {
                saveOk = false;
            }

            finally
            {
                streamWriter.Close();
                if (saveOk == true)
                {
                    MessageBox.Show("The file has been saved in:" + exePath);
                }
            }
            return saveOk;
        }

        public bool ReadFromTxt(List<Task> taskList, string exePath)
        {
            bool readOk = true;
            StreamReader streamReader = null;

            try
            {
                if (taskList != null)
                {
                    taskList.Clear();
                }
                else
                {
                    taskList = new List<Task>();
                }

                streamReader = new StreamReader(exePath);

                string tokenTest = streamReader.ReadLine();
                string versionTest = streamReader.ReadLine();

                if (tokenTest == _token && versionTest == _version)
                {
                    int count = int.Parse(streamReader.ReadLine()); // getting the count of all tasks saved

                    for (int i = 0; i < count; i++)
                    {
                        Task task = new Task();

                        task.DateTime = DateTime.Parse(streamReader.ReadLine());
                        task.Time = streamReader.ReadLine();
                        task.PriorityType = (PriorityType)Enum.Parse(typeof(PriorityType), streamReader.ReadLine());
                        task.TaskName = streamReader.ReadLine();
                        taskList.Add(task);
                    }
                    return readOk;
                }
                else
                {
                    readOk = false;
                    MessageBox.Show("Wrong version");
                    return readOk;
                }
            }
            catch
            {
                readOk = false;
                return readOk;
            }
            finally
            {
                streamReader.Close();
                if (readOk == true)
                {
                    MessageBox.Show("Opened file in: " + exePath);
                }
            }

        }

        public bool SaveFileAs(List<Task> taskList)
        {
            bool saveAsOk = true;

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
                

                if (saveFileDialog.ShowDialog() == true)
                {
                    string exePath = Path.GetFullPath(saveFileDialog.FileName);
                    SaveToTxt(taskList, exePath);
                    saveAsOk = true;
                }
                return saveAsOk;

            }
            catch (Exception e)
            {
                saveAsOk = false;
                MessageBox.Show(e.Message);
                return saveAsOk;
            }
        }

        public bool OpenExistingFile(List<Task> taskList)
        {
            bool openOk = true;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;

                if (openFileDialog.ShowDialog() == true)
                {
                    ReadFromTxt(taskList, Path.GetFileName(openFileDialog.FileName));
                }
                return openOk;
            }
            catch (Exception e)
            {
                openOk = false;
                MessageBox.Show(e.Message);
                return openOk;
            }
        }


    }
}
