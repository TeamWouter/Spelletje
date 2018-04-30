using System;
using System.IO;

namespace Spelletje
{
    public class GameMenu
    {
        private string _screenText;
        private string _savePath;
        private string[] _options;

        public GameMenu()
        {
            LoadOptions();
            _screenText = "Welcome to DankSouls\n";
        }

        //For Opening Menu
        public void OpenMenu(bool running)
        {
            if (running)
            {
                foreach (string option in _options)
                {
                    if (option != _options[1])
                    {
                        _screenText += $"{option} \n";
                    }
                }
            }
            else
            {
                foreach (string option in _options)
                {
                    if (option != _options[2] && option != _options[3])
                    {
                        _screenText += $"{option} \n";
                    }          
                }
            }

            _screenText += "\nPlease Input Option";
            Console.WriteLine(_screenText);

            string input = Console.ReadLine();
            MenuInput(input, running);
        }

        //Sets Menu Options
        private void LoadOptions()
        {
            _options = new[] {"1: New Game", "2: Load Game", "3: Save Game", "4: Close Menu" };
        }

        //Read Menu Input's
        private void MenuInput(string input, bool running)
        {
            switch (input)
            {
                //New Game
                case "1":
                    Console.Clear();
                    _screenText = "Input Save Name";
                    Console.WriteLine(_screenText);

                    string saveName = Console.ReadLine();

                    if (!NewGame(saveName))
                    {
                        _screenText = "File Name Already Exists, Try Again";
                        Console.WriteLine(_screenText);

                        saveName = Console.ReadLine();
                        if (!NewGame(saveName))
                        {
                            Console.Clear();
                            OpenMenu(running);
                        }
                    }
                    break;
                //Load Game
                case "2":
                    Console.Clear();
                    _screenText = "Choose A File:\n";
                    string path = $@"{Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))}\DankSouls";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    
                    string[] fileList = Directory.GetFiles(path, "*.txt");

                    for(int i = 1; i < (fileList.Length +1); i++)
                    {
                        _screenText += $"{i}: {Path.GetFileName(fileList[i - 1])} \n";
                    }

                    _screenText += "\nInput File ID";
                    Console.WriteLine(_screenText);

                    input = Console.ReadLine();

                    if (IsDigitsOnly(input))
                    {
                        int fileIndex = Int32.Parse(input) - 1;

                        if (fileIndex < fileList.Length)
                        {
                            _savePath = path + fileList[fileIndex];
                            Console.WriteLine(_savePath);
                        }
                    }
                    else
                    {
                        _screenText = "Error: Input Was No Int";
                        Console.WriteLine(_screenText);
                    }

                    break;
                //Save Game
                case "3":
                    if (!running)
                    {
                        Console.WriteLine("Please give an option number");
                        break;
                    }
                    else
                    {
                        break;
                    }

                default:
                    Console.WriteLine("Please give an option number");
                    break;
               
            }
        }

        //Return Current Save Path
        public string Done()
        {
            return _savePath;
        }

        public bool NewGame(string saveName)
        {
            string path = $@"{Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))}\DankSouls";
            Directory.CreateDirectory(path);

            if (!File.Exists(path))
            {
                string filePath = path + $@"\{saveName}.txt";

                File.Create(filePath);

                _savePath = filePath;
                return true;
            }
            return false;
        }


        public string LoadFile()
        {
            return String.Empty;
        }

        public bool SaveFile()
        {
            return true;
        }

        bool IsDigitsOnly(string str)
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