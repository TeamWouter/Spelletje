using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Spelletje
{
    public class ChatSystem
    {
        private Dictionary<string, int> Actions { get; set; }
        private bool CommandFailed { get; set; }
        private List<NPC> Npcs { get; set; }
        private string Text { get; set; }
        private NPC CurrentNpc { get; set; }
        public int ChatState { get; set; }

        public string UpdateText(string name)
        {
            foreach (NPC npc in Npcs)
            {
                if (npc.Name.Equals(name))
                {
                    CurrentNpc = npc;
                }
            }

            CurrentNpc.CurrentSentance = CurrentNpc.GetSentanceByIndex(CurrentNpc.History);
            string choices = String.Empty;
            foreach (string choice in CurrentNpc.CurrentSentance._choices.Values)
            {
                choices += $"{choice}\n";
            }

            Text = $"{CurrentNpc.Name}:\n{CurrentNpc.CurrentSentance._info}\n\n{choices}\n\n";

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
            if (checkInt != -1 && checkInt != 4)
            {
                CommandFailed = false;
                foreach (KeyValuePair<string, string> kvp in CurrentNpc.CurrentSentance._choices)
                {
                    if (kvp.Value.Split(':').First().Equals($"{checkInt}"))
                    {
                        CurrentNpc.History = kvp.Key;
                    }
                }
            }
            else if (checkInt != -1 && checkInt == 4)
            {
                CommandFailed = false;
                ChatState = 4;
            }
            else
            {
                CommandFailed = true;
            }
        }

        private int CheckInput(string input)
        {
            FillActions();
            foreach (var key in Actions.Keys)
            {
                if (input.ToLower().Equals(key.ToLower()))
                {
                    return Actions[key];
                }
            }

            return -1;
        }

        private void FillActions()
        {
            Actions = new Dictionary<string, int>();
            foreach (KeyValuePair<string, string> kvp in CurrentNpc.CurrentSentance._choices)
            {
                Actions.Add(kvp.Value.Split(':').First(), Int32.Parse(kvp.Value.Split(':').First()));
            }
        }

        public void CreateAllNpcs(Map.Map map)
        {
            Npcs = new List<NPC>();
            foreach (Room room in map.GetAllRooms())
            {
                foreach (string npcName in room.Npcs)
                {
                    Npcs.Add(new NPC(npcName));
                }
            }
        }

    }
}