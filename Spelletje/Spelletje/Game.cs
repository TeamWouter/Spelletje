using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Spelletje
{
    public class Game
    {
        private bool _running = false;
        private GameMenu _menu;

        private List<Room> _rooms;
        private Room _currentRoom;
        private string _screenText;


        private bool _commandFailed = false;
        private Dictionary<string, int> _actions;

        public Game()
        {
            _menu = new GameMenu();
            FillActions();
            RunGame();
        }

        public void RunGame()
        {
            _menu.OpenMenu(_running);
            string SavePath = _menu.Done();

            if (SavePath != string.Empty)
                _running = true;
                BuildMap();


            //Focred new game
            _currentRoom = GetRoomByIndex(new Point(1, 0));
            
            while (_running)
            {
                Console.Clear();
                _screenText = _currentRoom._roomInfo + "\n";

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
                if (!DoInput(input))
                {
                    _commandFailed = true;
                }

                
            }
        }

        private int CheckInput(string input)
        {
            foreach (var key in _actions.Keys)
            {
                if (input.Contains(key))
                {
                    return _actions[key];
                }
            }

            return -1;
        }

        private bool DoInput(string input)
        {
            int actionIndex = CheckInput(input);

            if (actionIndex >= 0)
            {
                if (_currentRoom._commands.Contains(actionIndex))
                {
                    Point newPosition = _currentRoom._location;

                    switch (actionIndex)
                    {
                        //North
                        case 1:
                            newPosition.Y = _currentRoom._location.Y + 1;
                            _currentRoom = GetRoomByIndex(newPosition);
                            break;
                        
                        //East
                        case 2:
                            newPosition.X = _currentRoom._location.X + 1;
                            _currentRoom = GetRoomByIndex(newPosition);
                            break;

                        //South
                        case 3:
                            newPosition.Y = _currentRoom._location.Y - 1;
                            _currentRoom = GetRoomByIndex(newPosition);
                            break;

                        //West
                        case 4:
                            newPosition.X = _currentRoom._location.X - 1;
                            _currentRoom = GetRoomByIndex(newPosition);
                            break;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        private void BuildMap()
        {
            //South West == 0.0
            _rooms = new List<Room>();

            Room room = new Room("Spawn", new Point(1, 0));
            room._commands.Add(1);
            _rooms.Add(room);

            //
            room = new Room("Monster|Key", new Point(0, 1));
            room._commands.Add(1);
            room._commands.Add(2);
            _rooms.Add(room);

            room = new Room("SaveRoom", new Point(1, 1));
            room._commands.Add(1);
            room._commands.Add(2);
            room._commands.Add(3);
            room._commands.Add(4);
            _rooms.Add(room);

            room = new Room("PitTrap", new Point(2, 1));
            room._commands.Add(1);
            room._commands.Add(3);
            _rooms.Add(room);

            //
            room = new Room("Loot", new Point(0, 2));
            room._commands.Add(3);
            room._commands.Add(2);
            _rooms.Add(room);

            room = new Room("NPC", new Point(1, 2));
            room._commands.Add(3);
            room._commands.Add(2);
            room._commands.Add(4);
            _rooms.Add(room);

            room = new Room("Table|Item", new Point(2, 2));
            room._commands.Add(3);
            room._commands.Add(4);
            _rooms.Add(room);
        }

        private void FillActions()
        {
            _actions = new Dictionary<string, int>();

            _actions.Add("Menu", 0);

            //Directions
            _actions.Add("North", 1);
            _actions.Add("East", 2);
            _actions.Add("South", 3);
            _actions.Add("West", 4);

            //Basic Actions
            _actions.Add("Open", 5);
            _actions.Add("Close", 6);
            _actions.Add("Talk to", 7);
            _actions.Add("Examine", 8);
        }

        private Room GetRoomByIndex(Point pos)
        {
            foreach (Room room in _rooms)
            {
                if (room._location.Equals(pos))
                    return room;
            }

            return null;
        }
    }
}