using System.Collections.Generic;

namespace Spelletje
{
    public class Actions
    {
        public Dictionary<string, int> _actions { get; set; }
        public Dictionary<string, int> _chatActions { get; set; }

        public Actions()
        {
            FillActions();
            FillChatActions();
        }

        private void FillActions()
        {
            _actions = new Dictionary<string, int>();

            _actions.Add("Menu", 0);

            //Directions
            _actions.Add("North", 1);
            _actions.Add("East", 2);
            _actions.Add("South", 3);
            _actions.Add("West", 4);

            _actions.Add("N", 1);
            _actions.Add("E", 2);
            _actions.Add("S", 3);
            _actions.Add("W", 4);


            //Basic Actions
            _actions.Add("Open", 5);
            _actions.Add("Close", 6);
            _actions.Add("Talk to", 7);
            _actions.Add("Examine", 8);
            _actions.Add("Look", 9);
        }

        private void FillChatActions()
        {
            _chatActions = new Dictionary<string, int>();

            _chatActions.Add("Menu", 0);
            _chatActions.Add("1", 1);
            _chatActions.Add("2", 2);
            _chatActions.Add("3", 3);
            _chatActions.Add("4", 4);
        }
    }
}