using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Spelletje
{
    public class GameMenu
    {
        private string _screenText;
        private string[] _options;
        private bool _menuDone;

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
                    _screenText += $"{option} \n";
                }
            }
            else
            {
                foreach (string option in _options)
                {
                    if (option != _options[2] || option != _options[3])
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
                    _screenText = "Starting New Game";
                    Console.WriteLine(_screenText);
                    break;
                //Load Game
                case "2":

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
            return "Done";
        }


    }
}