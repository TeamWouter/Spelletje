using System;

namespace Spelletje
{
    public class Game
    {
        private bool _running = false;
        private GameMenu _menu;

        public Game()
        {
            _menu = new GameMenu();
            RunGame();
        }

        public void RunGame()
        {
            _menu.OpenMenu(_running);
            string SavePath = _menu.Done();
            if (SavePath != string.Empty)
                _running = true;


            while (_running)
            {
                //Console.Clear();


                Console.Read();
            }
        }
    }
}