using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PongGame
{
    public class Ball
    {
        public Rectangle rect;
        private int moveSpeed = 200;
        private int right = 1;
        private int top = 1;



        public Ball() 
        {
            rect = new Rectangle(Globals.WIDTH/2 - 10, Globals.HEIGHT/2 - 10, 20, 20);
        }

        public void Update(GameTime gameTime, Paddle player1, Paddle player2)
        {
            int deltaSpeed = (int)(moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            rect.X += right * deltaSpeed;
            rect.Y += top * deltaSpeed;

            if (rect.Top <= 0 || rect.Bottom >= Globals.HEIGHT)
            {
                top *= -1;
            }
            if (rect.Right >= Globals.WIDTH)
            {
                Globals.player1Score++;
                ResetBall();
            }
            if (rect.Left <= 0)
            {
                Globals.player2Score++;
                ResetBall();
            }

                // detect paddle collisions
                if (rect.Left < player1.rect.Right)
            {
                if (DetectCollision(player1))
                {
                    rect.X = player1.rect.Right;
                    right *= -1;
                    moveSpeed += 25;
                    Globals.effect.Play();
                }
            }

            if (rect.Right > player2.rect.Left)
            {
                if (DetectCollision(player2))
                {
                    rect.X = player2.rect.Left - rect.Width;
                    right *= -1;
                    moveSpeed += 25;
                    Globals.effect.Play();
                }
            }

        }

        public void Draw(GameTime gameTime)
        {
            Globals.spriteBatch.Draw(Globals.pixel, rect, Color.White);
        }

        public void ResetBall()
        {
            rect.X = Globals.WIDTH / 2 - 10;
            rect.Y = Globals.HEIGHT / 2 - 10;
            moveSpeed = 200;
        }
        private bool DetectCollision(Paddle paddle) => (rect.Top > paddle.rect.Top && rect.Bottom < paddle.rect.Bottom);
        
    }
}
