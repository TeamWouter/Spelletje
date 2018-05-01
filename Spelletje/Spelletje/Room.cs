using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace Spelletje
{
    public class Room
    {
        public string _roomName { get; set; }
        public string _roomInfo { get; set; }
        public Point _location { get; set; }
        public List<int> _commands { get; set; }
        public List<NPC> _npcs { get; set; }

        public Room(string name, string info, Point location)
        {
            _roomName = name;
            _roomInfo = info;
            _location = location;
            _commands = new List<int>();
        }
    }
}