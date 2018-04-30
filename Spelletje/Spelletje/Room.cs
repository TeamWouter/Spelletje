using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace Spelletje
{
    public class Room
    {
        public string _roomInfo { get; set; }
        public Point _location { get; set; }
        public List<int> _commands { get; set; }

        public Room(string roomInfo, Point location)
        {
            this._roomInfo = roomInfo;
            this._location = location;
            _commands = new List<int>();
        }
    }
}