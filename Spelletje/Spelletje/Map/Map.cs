using System;
using System.Collections.Generic;
using System.Drawing;

namespace Spelletje.Map
{
    public class Map
    {
        private List<Room> _rooms;

        public Room CurrentRoom { get; set; }

        private void CreateRoom(string name, string info, Point point, int[] commands, string[] npcs)
        {
            Room room = new Room(name, info, point);
            foreach (int i in commands)
            {
                room._commands.Add(i);
            }
            foreach (string npc in npcs)
            {
                if( npc != String.Empty)
                {
                    room._npcs.Add(npc);
                }          
            }

            _rooms.Add(room);
        }

        public Room GetRoomByIndex(Point pos)
        {
            foreach (Room room in _rooms)
            {
                if (room._location.Equals(pos))
                    return room;
            }

            return null;
        }

        public void BuildMap()
        {
            //North West == 0.0
            _rooms = new List<Room>();

            //Y = 0
            CreateRoom("Room (0, 0)", "You can walk North", new Point(0, 0), new []{1, 7}, new []{"Bart"});
            CreateRoom("Room (0, 1)", "You can walk South", new Point(0, 1), new[] {3}, new[] {String.Empty});
        }
    }
}