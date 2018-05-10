using System;
using System.IO;

namespace Spelletje
{
    public class GameMenu
    {
        public string ScreenText { get; set; }
        private string _saveFile;
        private string[] _options;

        public string UpdateText(bool isRunning)
        {
            string message = $"DankSouls: Menu\n";
            if (isRunning)
            {
                message += $"Load Game\nSave Game\nClose Menu\n";
            }
            else
            {
                message += $"New Game\nLoad Game\n";
            }

            return message;
        }

        public bool NewGame()
        {
            Console.Clear();
            Console.WriteLine("Input Save Name");

            string saveName = Console.ReadLine();

            string path =
                $@"{Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))}\DankSouls";
            Directory.CreateDirectory(path);

            if (!File.Exists(path))
            {
                string filePath = path + $@"\{saveName}.txt";

                File.Create(filePath);

                _saveFile = $@"{saveName}.txt";
                return true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("File Name Already Exists, Try Again");
                return false;
            }
        }

        public bool LoadFile()
        {
            Console.Clear();
            ScreenText = "Choose A File:\n";
            string path =
                $@"{Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))}\DankSouls";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string[] fileList = Directory.GetFiles(path, "*.txt");

            for (int i = 1; i < (fileList.Length + 1); i++)
            {
                ScreenText += $"{i}: {Path.GetFileName(fileList[i - 1])} \n";
            }

            Console.WriteLine(ScreenText);

            string input = Console.ReadLine();
            if (input != string.Empty)
            {
                int index = Int32.Parse(input);
                if (index >= 1 && index < fileList.Length + 1)
                {
                    _saveFile = fileList[index - 1];
                    return true;
                }
            }
            return false;
        }
    }
}