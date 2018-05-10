using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace Spelletje
{
    public class Room
    {
        private string _roomName { get; set; }
        private string _roomStaticText { get; set; }
        public Point _location { get; set; }
        public List<int> _commands { get; set; }
        public List<string> _npcs { get; set; }

        public Room(string name, string staticText, Point location)
        {
            _roomName = name;
            _roomStaticText = staticText;
            _location = location;
            _commands = new List<int>();
            _npcs =  new List<string>();
        }

        public override string ToString()
        {
            string roomInfo = $"{_roomName}:\n{_roomStaticText}\n";
            foreach (string npc in _npcs)
            {
                roomInfo += $@"{npc} is also in this room.";
            }

            return roomInfo;
        }
    }
}