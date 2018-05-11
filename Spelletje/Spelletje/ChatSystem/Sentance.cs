using System.Collections.Generic;

namespace Spelletje
{
    public class Sentance
    {
        public string _index;
        public Dictionary<string, string> _action;
        public string _info;
        public Dictionary<string, string> _choices;

        public Sentance(string index, Dictionary<string, string> action, string info, Dictionary<string, string> choices)
        {
            this._index = index;
            this._action = action;
            this._info = info;
            this._choices = choices;
        }
    }
}