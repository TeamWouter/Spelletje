using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Spelletje
{
    public class NPC
    {
        /* ID|{Keys}|INFO|{Choices>ID}
        * 1|{Keys}|Hello Stranger, what is your name?|{Hello, my name is Bart.>1.1|Bye>1.2}
        */

        public string Name { get; set; }
        public string History { get; set; }
        private readonly List<Sentance> _sentances;
        public Sentance CurrrentSentance { get; set; }

        public NPC(string name)
        {
            _sentances = new List<Sentance>();
            Name = name;
            LoadChat();
            
            History = "1";
            CurrrentSentance = GetSentanceByIndex(History);
        }

        private void LoadChat()
        {
            //Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))}\DankSouls\NPC\{Name}.txt"
            string file = Properties.Resources.ResourceManager.GetString(Name);
            string[] chat = file.Split('#');

            foreach (string str in chat)
            {
                if (str != String.Empty)
                {
                    string check = str;

                    if (str.StartsWith("\r\n"))
                    {
                        check = str.Substring(2);
                    }

                    string[] bits = check.Split('|');

                    if (bits[0] != String.Empty && bits[1] != String.Empty && bits[2] != String.Empty &&
                        bits[3] != String.Empty)
                    {
                        Dictionary<string, string> choices = new Dictionary<string, string>();

                        foreach (string bit in bits[3].Split('<'))
                        {
                            string[] choice = bit.Split('>');
                            choices.Add(choice[1], choice[0]);
                        }

                        Sentance sentance = new Sentance(bits[0], bits[1].Split(','), bits[2], choices);
                        _sentances.Add(sentance);
                    }                    
                }
            }
        }

        public Sentance GetSentanceByIndex(string index)
        {
            foreach (Sentance sentance in _sentances)
            {
                if (sentance._index.Equals(index))
                    return sentance;
            }
            return null;
        }


    }
}