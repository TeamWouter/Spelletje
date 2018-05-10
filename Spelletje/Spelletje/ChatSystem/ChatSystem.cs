using System;
using System.Collections.Generic;
using System.IO;

namespace Spelletje
{
    public class ChatSystem
    {
        private List<NPC> _npcs { get; set; }

        public bool _isChatting { get; set; }
        public string _message { get; set; }

        public NPC _currentNpc { get; set; }

        public ChatSystem()
        {
            _isChatting = false;
            CreateAllNpcs();
        }


        private void CreateAllNpcs()
        {
            _npcs =  new List<NPC>();

            NPC npc = new NPC("Bart");
            _npcs.Add(npc);
        }

 
        public NPC GetNpcByName(string name)
        {
            foreach (NPC npc in _npcs)
            {
                if (npc.Name.ToLower().Equals(name.ToLower()))
                {
                    return npc;
                }
            }

            return null;
        }

        public void UpdateMessage()
        {
            _currentNpc.CurrrentSentance = _currentNpc.GetSentanceByIndex(_currentNpc.History);

            string choices = String.Empty;
            foreach (string choice in _currentNpc.CurrrentSentance._choices.Values)
            {
                choices += $"{choice}\n";
            }

            _message = $"{_currentNpc.Name}:\n{_currentNpc.CurrrentSentance._info}\n\n{choices}";
        }

    }
}