using System;
using System.Drawing;

namespace Spelletje
{
    public class Game
    {
        //Start Menu 
        private bool _running;
        private GameMenu _menu;
        private Controller _controller;

        //While Running
        private string _screenText;
        private Map.Map _map;
        private ChatSystem _chatSystem;
        


        private bool _commandFailed = false;

        public Game()
        {
            _running = false;
            _menu = new GameMenu();
            _controller = new Controller();
            _map = new Map.Map();
            _chatSystem = new ChatSystem();
            StartMenu();
        }

        public void StartMenu()
        {
            Console.WriteLine(_menu.UpdateText(_running));
            string input = Console.ReadLine();
            if (!_controller.MenuInput(input, _menu, _running))
            {
                _commandFailed = true;
            }
            else
            {
                _running = true;
                _map.BuildMap();
                RunGame();
            }




        }




        public void RunGame()
        {
            //Focred new game
            _map.CurrentRoom = _map.GetRoomByIndex(new Point(0, 0));
            
            while (_running)
            {
                Console.Clear();

                if (_chatSystem._isChatting)
                {
                    _chatSystem.UpdateMessage();
                    _screenText = _chatSystem._message;

                    if (_commandFailed)
                    {
                        _screenText += "\nI rather not say that.";
                    }
                    else
                    {
                        _screenText += "\nWhat to say..";
                    }

                    Console.WriteLine(_screenText);
                    string input = Console.ReadLine();
                    if (!_controller.ChatInput(input, _chatSystem))
                    {
                        _commandFailed = true;
                    }

                }
                else
                {
                    _screenText = _map.CurrentRoom + "\n";

                    if (_commandFailed)
                    {
                        _screenText += "\nCan't Do That. (Try ?)";
                    }
                    else
                    {
                        _screenText += "\nWhat Should I Do?";
                    }

                    Console.WriteLine(_screenText);
                    string input = Console.ReadLine();
                    if (!_controller.DoInput(input, _map, _chatSystem))
                    {
                        _commandFailed = true;
                    }
                }
            }
        }

        




        




    }
}