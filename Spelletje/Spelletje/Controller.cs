using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Spelletje
{
    public class Controller
    {
        private Actions _actions;

        public Controller()
        {
            _actions = new Actions();
        }

        private int CheckActionInput(string input)
        {
            foreach (var key in _actions._actions.Keys)
            {
                if (input.ToLower().Contains(key.ToLower()))
                {
                    return _actions._actions[key];
                }
            }

            return -1;
        }

        private int CheckChatInput(string input)
        {
            foreach (var key in _actions._chatActions.Keys)
            {
                if (input.ToLower().Contains(key.ToLower()))
                {
                    return _actions._chatActions[key];
                }
            }

            return -1;
        }

        private int CheckMenuInput(string input, bool isRunning)
        {
            if (isRunning)
            {
                foreach (var key in _actions._menuActions.Keys)
                {
                    if (input.ToLower().Contains(key.ToLower()))
                    {
                        return _actions._menuActions[key];
                    }
                }
            }
            else
            {
                foreach (var key in _actions._menuActions.Keys)
                {
                    if (input.ToLower().Contains(key.ToLower()) && input.ToLower() != "save game" && input.ToLower() != "close menu")
                    {
                        return _actions._menuActions[key];
                    }
                }
            }

            return -1;

        }

        public bool DoInput(string input, Map.Map map, ChatSystem chatSystem)
        {
            int actionIndex = CheckActionInput(input);

            if (actionIndex >= 0)
            {
                if (map.CurrentRoom._commands.Contains(actionIndex))
                {
                    Point newPosition = map.CurrentRoom._location;

                    switch (actionIndex)
                    {
                        //North
                        case 1:
                            newPosition.Y = map.CurrentRoom._location.Y + 1;
                            map.CurrentRoom = map.GetRoomByIndex(newPosition);
                            break;

                        //East
                        case 2:
                            newPosition.X = map.CurrentRoom._location.X + 1;
                            map.CurrentRoom = map.GetRoomByIndex(newPosition);
                            break;

                        //South
                        case 3:
                            newPosition.Y = map.CurrentRoom._location.Y - 1;
                            map.CurrentRoom = map.GetRoomByIndex(newPosition);
                            break;

                        //West
                        case 4:
                            newPosition.X = map.CurrentRoom._location.X - 1;
                            map.CurrentRoom = map.GetRoomByIndex(newPosition);
                            break;

                        //Talk To
                        case 7:
                            NPC currentNpc = chatSystem.GetNpcByName(input.Split(' ').Last());

                            if (currentNpc != null)
                            {
                                chatSystem._isChatting = true;
                                chatSystem._currentNpc = currentNpc;
                            }
                            else
                            {
                                return false;
                            }
                            
                            break;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ChatInput(string input, ChatSystem chatSystem)
        {
            int actionIndex = CheckChatInput(input);

            if (actionIndex >= 0 && actionIndex <= 3)
            {
                foreach (KeyValuePair<string, string> kvp in chatSystem._currentNpc.CurrrentSentance._choices)
                {
                    if (kvp.Value.Split(':').First().Equals(input))
                    {
                        chatSystem._currentNpc.History = kvp.Key;
                        return true;
                    }
                }
            }
            else if (actionIndex == 4)
            {
                chatSystem._isChatting = false;
                return true;
            }
            return false;
        }

        public bool MenuInput(string input, GameMenu gameMenu, bool isRunning)
        {
            int actionIndex = CheckMenuInput(input, isRunning);

            if (actionIndex >= 0)
            {
                switch (actionIndex)
                {
                    //New Game
                    case 0:
                        return gameMenu.NewGame();

                    //Load Game
                    case 1:
                        return gameMenu.LoadFile();
                    //Save Game
                    case 2:
 
                    //Close Menu
                    case 3:
                        break;

                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }

}