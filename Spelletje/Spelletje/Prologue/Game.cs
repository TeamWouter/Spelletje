using System;
using System.Diagnostics;
using System.Drawing;
using Spelletje.Menu;

namespace Spelletje
{
    public class Game
    {
        private readonly GameMenu _menu;
        private readonly Map.Map _map;
        private readonly Prologue _prologue;
        private readonly ChatSystem _chatSystem;

        //State System 
        private int _state;
        private readonly bool _firstRun;

        public Game()
        {
            _menu = new GameMenu();
            _map = new Map.Map();
            _prologue = new Prologue();
            _chatSystem = new ChatSystem();

            //TEST
            _state = 0;
            _firstRun = true;

            Run();
        }

        public void Run()
        {
            while (true)
            {
                switch (_state)
                {
                    //Start Menu
                    case 0:
                        Menu();
                        break;

                    //World
                    case 1:
                        World();
                        break;

                    //Prologue
                    case 2:
                        Prologue();
                        break;

                    //Chatting
                    case 3:
                        Chatting();
                        break;
                }
            }
        }

        private void Menu()
        {
            Draw(_menu.UpdateText());
            _menu.WaitInput();
            if (_menu.MenuIndex == 5 && _firstRun)
            {
                _state = 2;

                //Load Start Point
                _map.BuildMap(new Point(0, 0));
                _chatSystem.CreateAllNpcs(_map);
            }
            else if(_menu.MenuIndex == 5)
            {
                _state = 1;
                _menu.MenuIndex = 1;
            }
        }

        private void World()
        {
            Draw(_map.UpdateText());
            _map.WaitInput();
            if (_map.GameIndex == 7)
            {
                _state = 3;
                _map.GameIndex = 0;
            }
        }

        private void Prologue()
        {
            Draw(_prologue.UpdateText());
            _prologue.WaitInput();
            if (_prologue.PrologueIndex == -2)
            {
                _state = 1;
            }
        }

        private void Chatting()
        {
            Draw(_chatSystem.UpdateText(_map.Name));
            _chatSystem.WaitInput();
            if (_chatSystem.ChatState == 4)
            {
                _state = 1;
                _chatSystem.ChatState = 0;
            }
        }

        private void Draw(string text)
        {
            Console.Clear();
            Console.WriteLine(text);
        }
    }
}