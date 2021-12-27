using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace library
{
    public class GameLoop
    {
        private GameCore _game;
        public bool isRunning { get; private set; }
        public InputManager inp;
        public int money;

        public void Load(GameCore game)
        {
            _game = game;
        }

        public async void Start()
        {
            if (_game == null)
                throw new ArgumentException("No game, pal, go home");
            _game.Load();
            isRunning = true;
            DateTime _previousTime = DateTime.Now;
            inp = new InputManager();

            while (isRunning)
            {   
                money = _game.money;

                TimeSpan GameTime = DateTime.Now - _previousTime;
                _previousTime = _previousTime + GameTime;
                _game.Update(GameTime, inp);
                
                await Task.Delay(8); //60 fps
            }
        }

        public void Stop()
        {
            isRunning = false;
            _game?.Unload();
        }
        public void Draw(Graphics graphics)
        {
            _game.Draw(graphics);
            
        }


    }
}
