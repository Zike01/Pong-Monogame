using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PongGame
{
    public class Paddle
    {
        public Rectangle rect;
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsSecondPlayer { get; }
        private int paddleSpeed;
        private Keys _up;
        private Keys _down;
        

        public Paddle(int width, int height, Keys up, Keys down, bool isSecondPlayer)
        {
            Width = width;
            Height = height;
            IsSecondPlayer = isSecondPlayer;
            rect = new Rectangle(isSecondPlayer ? Globals.WIDTH - Width : 0, (Globals.HEIGHT - Height) / 2, Width, Height);

            _up = up;
            _down = down;
            paddleSpeed = 10;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(_up) && rect.Top > 0)
            {
                rect.Y -= paddleSpeed;
            }
            if (kstate.IsKeyDown(_down) && rect.Bottom < Globals.HEIGHT) 
            {
                rect.Y += paddleSpeed;
            }
        }

        public void Draw(GameTime gameTime)
        {
            Globals.spriteBatch.Draw(Globals.pixel, rect, Color.White);
        }
    }
}
