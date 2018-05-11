using System;
using System.Collections.Generic;
using System.IO;

namespace Spelletje.Menu
{
    public class GameMenu
    {
        private string Text { get; set; }
        public int MenuIndex { get; set; }
        private Dictionary<string, int> Actions { get; set; }
        private bool CommandFailed { get; set; }

        //File
        private string _path =
            $@"{Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))}\DankSouls";
        private string SaveFile { get; set; }

        public GameMenu()
        {
            MenuIndex = 0;
            CommandFailed = false;
            FillMenuActions();
        }

        public string UpdateText()
        {
            Text = $"DankSouls: ";
            switch (MenuIndex)
            {
                //Start
                case 0:
                    Text += $"Menu\nNew Game\nLoad Game\n\n";
                    if (CommandFailed)
                    {
                        Text += $"Input Not Reconised.";
                    }
                    else
                    {
                        Text += $"Input Option.";
                    }
                    break;

                //After Start
                case 1:
                    Text += $"Menu\nLoad Game\nSave Game\nClose Menu\n\n";
                    if (CommandFailed)
                    {
                        Text += $"Input Not Reconised.";
                    }
                    else
                    {
                        Text += $"Input Option.";
                    }
                    break;

                //New Game
                case 2:
                    Text += $"New Game\n\n";
                    if (CommandFailed)
                    {
                        Text += $"Save Name Already In Use. Please Try Again.";
                    }
                    else
                    {
                        Text += $"Input Save Name.";
                    }

                    break;

                //Load Game
                case 3:
                    Text += $"Load Game\n";
                    string[] fileList = Directory.GetFiles(_path, "*.txt");
                    if (fileList.Length != 0)
                    {
                        for (int i = 1; i < (fileList.Length + 1); i++)
                        {
                            Text += $"{i}: {Path.GetFileName(fileList[i - 1])} \n";
                        }
                    }

                    if (CommandFailed)
                    {
                        Text += $"\nFile Index Unknown";
                    }
                    else
                    {
                        Text += $"\nInput File Index";
                    }
                    break;

                //Save Game
                case 4:
                    Text += $"Save Game\n";
                    break;

                //Error
                case -1:
                    Text += $"Menu Error";
                    break;

            }
            return Text;
        }

        public void WaitInput()
        {
            string input = Console.ReadLine();
            int checkInt = 0;
            switch (MenuIndex)
            {
                //Start
                case 0:
                    checkInt = CheckInput(input);
                    if (checkInt != -1)
                    {
                        CommandFailed = false;
                        MenuIndex = checkInt;
                    }
                    else
                    {
                        CommandFailed = true;
                    }
                    break;

                //After Start
                case 1:
                    checkInt = CheckInput(input);
                    if (checkInt != -1)
                    {
                        CommandFailed = false;
                        MenuIndex = checkInt;
                    }
                    else
                    {
                        CommandFailed = true;
                    }
                    break;

                //New Game
                case 2:
                    NewGame(input);
                    break;

                //Load Game
                case 3:
                    LoadFile(input);
                    break;

                //Save Game
                case 4:
                    break;

                //Error
                case -1:
                    break;

            }
        }

        private void NewGame(string input)
        {
            Directory.CreateDirectory(_path);

            if (!File.Exists(_path))
            {
                SaveFile = $@"\{input}.txt";
                string filePath = _path + SaveFile;

                File.Create(filePath);
                CommandFailed = false;
                MenuIndex = 5;
            }
            else
            {
                CommandFailed = true;
            }
        }
        
        private void LoadFile(string input)
        {
            string[] fileList = Directory.GetFiles(_path, "*.txt");

            if (input != string.Empty && fileList.Length != 0 && IsDigitsOnly(input))
            {
                CommandFailed = false;
                int index = Int32.Parse(input);
                if (index >= 1 && index < fileList.Length + 1)
                {
                    SaveFile = fileList[index - 1];
                    MenuIndex = 5;
                }
            }
            else
            {
                CommandFailed = true;
            }
        }
        
        private int CheckInput(string input)
        {
            foreach (var key in Actions.Keys)
            {
                if (input.ToLower().Equals(key.ToLower()))
                {
                    return Actions[key];
                }
            }

            return -1;
        }

        private void FillMenuActions()
        {
            Actions = new Dictionary<string, int>();

            Actions.Add("New Game", 2);
            Actions.Add("Load Game", 3);
            Actions.Add("Save Game", 4);
            Actions.Add("Return", 5);
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}