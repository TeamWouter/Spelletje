using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Spelletje.Map
{
    public class Map
    {
        private Dictionary<string, int> Actions { get; set; }
        private string Text { get; set; }
        public int GameIndex { get; set; }
        public string Name { get; set; }
        private bool CommandFailed { get; set; }
        private List<Room> _rooms;
        private Room CurrentRoom { get; set; }

        public Map()
        {
            FillActions();
        }

        private void CreateRoom(string name, string info, Point point, int[] commands, string[] npcs)
        {
            Room room = new Room(name, info, point);
            foreach (int i in commands)
            {
                room.Commands.Add(i);
            }
            foreach (string npc in npcs)
            {
                if( npc != String.Empty)
                {
                    room.Npcs.Add(npc);
                }          
            }

            _rooms.Add(room);
        }

        private Room GetRoomByIndex(Point pos)
        {
            foreach (Room room in _rooms)
            {
                if (room.Location.Equals(pos))
                    return room;
            }

            return null;
        }

        public void BuildMap(Point start)
        {
            //North West == 0.0
            _rooms = new List<Room>();

            //Y = 0
            //World
            CreateRoom("Room (0, 0)", "You can walk North", new Point(0, 0), new[] {1}, new[] {"Bart"});
            CreateRoom("Room (0, 1)", "You can walk South", new Point(0, 1), new[] {3}, new[] { String.Empty });


            CurrentRoom = GetRoomByIndex(start);
        }
        
        public string UpdateText()
        {
            Text = CurrentRoom.ToString();
            if (CommandFailed)
            {
                Text += $"I dont think i can do that.";
            }
            else
            {
                Text += $"What should i do.";
            }
            return Text;
        }

        public void WaitInput()
        {
            string input = Console.ReadLine();

            int checkInt = CheckInput(input);
            if (checkInt != -1)
            {
                CommandFailed = false;
                if (CurrentRoom.Commands.Contains(checkInt))
                {
                    CommandFailed = false;
                    Point newPosition = CurrentRoom.Location;

                    switch (checkInt)
                    {
                        //North
                        case 1:
                            newPosition.Y = CurrentRoom.Location.Y + 1;
                            CurrentRoom = GetRoomByIndex(newPosition);
                            break;

                        //East
                        case 2:
                            newPosition.X = CurrentRoom.Location.X + 1;
                            CurrentRoom = GetRoomByIndex(newPosition);
                            break;

                        //South
                        case 3:
                            newPosition.Y = CurrentRoom.Location.Y - 1;
                            CurrentRoom = GetRoomByIndex(newPosition);
                            break;

                        //West
                        case 4:
                            newPosition.X = CurrentRoom.Location.X - 1;
                            CurrentRoom = GetRoomByIndex(newPosition);
                            break;
                            
                        //Talk To
                        case 7:
                            Name = input.Split(' ').Last();
                            if (CurrentRoom.Npcs.Contains(Name))
                            {
                                CommandFailed = false;
                                GameIndex = 7;
                            }
                            else
                            {
                                CommandFailed = true;
                            }
                            break;
                    }
                }
                else
                {
                    CommandFailed = true;
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
                if (input.ToLower().Contains(key.ToLower()))
                {
                    return Actions[key];
                }
            }

            return -1;
        }

        private void FillActions()
        {
            Actions = new Dictionary<string, int>();

            Actions.Add("Menu", 0);

            //Directions
            Actions.Add("North", 1);
            Actions.Add("East", 2);
            Actions.Add("South", 3);
            Actions.Add("West", 4);

            Actions.Add("N", 1);
            Actions.Add("E", 2);
            Actions.Add("S", 3);
            Actions.Add("W", 4);


            //Basic Actions
            Actions.Add("Open", 5);
            Actions.Add("Close", 6);
            Actions.Add("Talk to", 7);
            Actions.Add("Examine", 8);
            Actions.Add("Look", 9);
        }

        public List<Room> GetAllRooms()
        {
            return _rooms;
        }
    }
}