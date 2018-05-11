using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace Spelletje
{
    public class Room
    {
        private string _roomName { get; set; }
        private string _roomStaticText { get; set; }
        public Point Location { get; set; }
        public List<int> Commands { get; set; }
        public List<string> Npcs { get; set; }

        public Room(string name, string staticText, Point location)
        {
            _roomName = name;
            _roomStaticText = staticText;
            Location = location;
            Commands = new List<int>();
            Npcs =  new List<string>();
        }

        public override string ToString()
        {
            string roomInfo = $"{_roomName}:\n{_roomStaticText}\n";
            foreach (string npc in Npcs)
            {
                roomInfo += $@"{npc} is also in this room.";
            }
            roomInfo += $"\n\n";

            return roomInfo;
        }
    }
}