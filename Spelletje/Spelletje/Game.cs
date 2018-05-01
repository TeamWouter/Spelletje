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
                if (input.ToLower().Contains(key.ToLower()))
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
            //North West == 0.0
            _rooms = new List<Room>();

            //Y = 0
            CreateRoom("Old Windmill", "", new Point(0, 0), new[] { 2, 3});
            CreateRoom("Riverside", "", new Point(1, 0), new[] { 2, 4});
            CreateRoom("Graveyard", "", new Point(2, 0), new[] { 3, 4});
            CreateRoom("", "", new Point(3, 0), new[] { 2});
            CreateRoom("", "", new Point(4, 0), new[] { 2, 4});
            CreateRoom("", "", new Point(5, 0), new[] { 2, 4});
            CreateRoom("", "", new Point(6, 0), new[] { 2, 4});
            CreateRoom("", "", new Point(7, 0), new[] { 2, 4});
            CreateRoom("", "", new Point(8, 0), new[] { 3, 4});

            CreateRoom("", "", new Point(0, 1), new[] { 1, 3});
            CreateRoom("Home", "", new Point(1, 1), new[] { 3});
            CreateRoom("", "", new Point(2, 1), new[] { 1, 2});
            CreateRoom("", "", new Point(3, 1), new[] { 3, 4});
            CreateRoom("", "", new Point(4, 1), new[] { 2, 3});
            CreateRoom("", "", new Point(5, 1), new[] { 4});
            CreateRoom("", "", new Point(6, 1), new[] { 2});
            CreateRoom("", "", new Point(7, 1), new[] { 3, 4});
            CreateRoom("", "", new Point(8, 1), new[] { 1, 3});

            CreateRoom("", "", new Point(0, 2), new[] { 1, 3});
            CreateRoom("", "", new Point(1, 2), new[] { 1, 2, 3});
            CreateRoom("", "", new Point(2, 2), new[] { 2, 4});
            CreateRoom("", "", new Point(3, 2), new[] { 1, 2, 3});
            CreateRoom("", "", new Point(4, 2), new[] { 1, 2, 3, 4});
            CreateRoom("", "", new Point(5, 2), new[] { 2, 4});
            CreateRoom("", "", new Point(6, 2), new[] { 2, 3, 4});
            CreateRoom("", "", new Point(7, 2), new[] { 1, 2, 4});
            CreateRoom("", "", new Point(8, 2), new[] { 1, 3, 4});

            CreateRoom("", "", new Point(0, 3), new[] { 1, 2});
            CreateRoom("", "", new Point(1, 3), new[] { 1, 4});
            CreateRoom("", "", new Point(2, 3), new[] { 3});
            CreateRoom("", "", new Point(3, 3), new[] { 3});
            CreateRoom("", "", new Point(4, 3), new[] { 1, 3});
            CreateRoom("", "", new Point(5, 3), new[] { 3});
            CreateRoom("", "", new Point(6, 3), new[] { 1, 3});
            CreateRoom("", "", new Point(7, 3), new[] { 3});
            CreateRoom("", "", new Point(8, 3), new[] { 1, 2, 3});
            CreateRoom("", "", new Point(9, 3), new[] { 4});

            CreateRoom("", "", new Point(0, 4), new[] { 2});
            CreateRoom("", "", new Point(1, 4), new[] { 2, 4});
            CreateRoom("", "", new Point(2, 4), new[] { 1, 2, 3, 4});
            CreateRoom("", "", new Point(3, 4), new[] { 1, 2, 3, 4});
            CreateRoom("", "", new Point(4, 4), new[] { 1, 3, 4});
            CreateRoom("", "", new Point(5, 4), new[] { 1, 2});
            CreateRoom("", "", new Point(6, 4), new[] { 1, 2, 3, 4});
            CreateRoom("", "", new Point(7, 4), new[] { 1, 4});
            CreateRoom("", "", new Point(8, 4), new[] { 1, 3});


            CreateRoom("", "", new Point(2, 5), new[] { 1});
            CreateRoom("", "", new Point(3, 5), new[] { 1});
            CreateRoom("", "", new Point(4, 5), new[] { 1});
            CreateRoom("", "", new Point(6, 5), new[] { 1, 3});
            CreateRoom("", "", new Point(7, 5), new[] { 2});
            CreateRoom("", "", new Point(8, 5), new[] { 1, 4});

            CreateRoom("", "", new Point(6, 6), new[] { 2, 3});
            CreateRoom("", "", new Point(7, 6), new[] { 2, 3});
            CreateRoom("", "", new Point(8, 6), new[] { 2, 3});

            CreateRoom("", "", new Point(5, 7), new[] { 2, 3});
            CreateRoom("", "", new Point(6, 7), new[] { 2, 3});
            CreateRoom("", "", new Point(8, 7), new[] { 2, 3});
            CreateRoom("", "", new Point(10, 7), new[] { 1, 3});

            CreateRoom("", "", new Point(5, 8), new[] { 2, 3});
            CreateRoom("", "", new Point(6, 8), new[] { 2, 3});
            CreateRoom("", "", new Point(7, 8), new[] { 2, 3});
            CreateRoom("", "", new Point(8, 8), new[] { 2, 3});
            CreateRoom("", "", new Point(10, 8), new[] { 1, 3});

            CreateRoom("", "", new Point(7, 9), new[] { 2, 3});
            CreateRoom("", "", new Point(8, 9), new[] { 2, 3});
            CreateRoom("", "", new Point(9, 9), new[] { 2, 3});
            CreateRoom("", "", new Point(10, 9), new[] { 1, 3, 4});

            CreateRoom("", "", new Point(10, 10), new[] { 1});

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

            _actions.Add("N", 1);
            _actions.Add("E", 2);
            _actions.Add("S", 3);
            _actions.Add("W", 4);


            //Basic Actions
            _actions.Add("Open", 5);
            _actions.Add("Close", 6);
            _actions.Add("Talk to", 7);
            _actions.Add("Examine", 8);
            _actions.Add("Look", 9);
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

        private void CreateRoom(string name, string info, Point point, int[] commands)
        {
            Room room = new Room(name, info, point);
            foreach (int i in commands)
            {
                room._commands.Add(i);
            }
            _rooms.Add(room);

        }
    }
}