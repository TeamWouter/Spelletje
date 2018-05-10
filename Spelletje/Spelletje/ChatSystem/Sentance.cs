using System.Collections.Generic;

namespace Spelletje
{
    public class Sentance
    {
        public string _index;
        public string[] _keys;
        public string _info;
        public Dictionary<string, string> _choices;

        public Sentance(string index, string[] keys, string info, Dictionary<string, string> choices)
        {
            this._index = index;
            this._keys = keys;
            this._info = info;
            this._choices = choices;
        }
    }
}