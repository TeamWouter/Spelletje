using System;
using System.Collections.Generic;
using System.Linq;

namespace Spelletje
{
    public class Prologue
    {
        private string Text { get; set; }
        public int PrologueIndex { get; set; }
        private bool CommandFailed { get; set; }
        private Dictionary<string, int> Actions { get; set; }

        //NPC
        private readonly List<Sentance> _sentances;
        public string History { get; set; }
        public Sentance CurrentSentance { get; set; }

        public Prologue()
        {
            _sentances = new List<Sentance>();
            LoadPrologue();

            History = "1";
            CurrentSentance = GetSentanceByIndex(History);
        }


        public string UpdateText()
        {
            CurrentSentance = GetSentanceByIndex(History);
            string choices = String.Empty;
            foreach (string choice in CurrentSentance._choices.Values)
            {
                choices += $"{choice}\n";
            }

            Text = $"{CurrentSentance._info.Replace("\\n", "\n")}\n\n{choices}\n\n";

            if (CommandFailed)
            {
                Text += $"DOES NOT COMPUTE, please try again.";
            }
            else
            {
                Text += $"Type here:";
            }
            return Text;
        }

        public void WaitInput()
        {
            string input = Console.ReadLine();

            int checkInt = CheckInput(input);
            if (checkInt != -1 && checkInt != -2)
            {
                CommandFailed = false;
                foreach (KeyValuePair<string, string> kvp in CurrentSentance._choices)
                {
                    if (kvp.Value.Split(':').First().Equals($"{checkInt}"))
                    {
                        History = kvp.Key;
                    }
                }
            }
            else if (checkInt != -1 && checkInt == -2)
            {
                CommandFailed = false;
                PrologueIndex = -2;
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
            foreach (KeyValuePair<string, string> kvp in CurrentSentance._choices)
            {
                Actions.Add(kvp.Value.Split(':').First(), Int32.Parse(kvp.Value.Split(':').First()));
            }
        }


        private void LoadPrologue()
        {
            //Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))}\DankSouls\NPC\{Name}.txt"
            string file = Properties.Resources.ResourceManager.GetString("Prologue");
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

                    //Sentance
                    string[] bits = check.Split('|');

                    if (bits.Length > 1)
                    {
                        if (bits[0] != String.Empty && bits[1] != String.Empty && bits[2] != String.Empty &&
                            bits[3] != String.Empty)
                        {
                            Dictionary<string, string> actions = new Dictionary<string, string>();
                            if (bits[1] != "!")
                            {
                                foreach (string bit in bits[1].Split(','))
                                {
                                    string[] action = bit.Split('>');
                                    actions.Add(action[1], action[0]);
                                }
                            }
                            else
                            {
                                actions.Add("!", "!");
                            }


                            Dictionary<string, string> choices = new Dictionary<string, string>();
                            foreach (string bit in bits[3].Split('<'))
                            {
                                string[] choice = bit.Split('>');
                                choices.Add(choice[1], choice[0]);
                            }



                            Sentance sentance = new Sentance(bits[0], actions, bits[2], choices);
                            _sentances.Add(sentance);
                        }
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