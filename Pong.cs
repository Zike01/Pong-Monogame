using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame
{
    public class Pong : Game
    {
        private GraphicsDeviceManager _graphics;

        private Ball ball;
        private Paddle player1;
        private Paddle player2;
        private int paddleWidth;
        private int paddleHeight;
        private SpriteFont font;

        private bool isPaused = false;
        private bool isGameOver = false;
        private KeyboardState previousState;

        private string message;

        public Pong()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = Globals.WIDTH;
            _graphics.PreferredBackBufferHeight = Globals.HEIGHT;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.pixel = new Texture2D(GraphicsDevice, 1, 1);
            Globals.pixel.SetData<Color>(new Color[] { Color.White });

            paddleWidth = 20;
            paddleHeight = 120;

            ball = new Ball();
            player1 = new Paddle(paddleWidth, paddleHeight, Keys.W, Keys.S, isSecondPlayer: false);
            player2 = new Paddle(paddleWidth, paddleHeight, Keys.Up, Keys.Down, isSecondPlayer: true);
            font = Content.Load<SpriteFont>("Score");
            Globals.effect = Content.Load<SoundEffect>("effect");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();

            KeyboardState currState = Keyboard.GetState();

            // pause game
            if (currState.IsKeyDown(Keys.Escape) && previousState.IsKeyUp(Keys.Escape) && !isGameOver)
            {
                isPaused = !isPaused;
            }

            if (!isPaused && !isGameOver)
            {
                player1.Update(gameTime);
                player2.Update(gameTime);
                ball.Update(gameTime, player1, player2);
            }

            // end game
            if (Globals.player1Score == 5 || Globals.player2Score == 5)
            {
                ball.ResetBall();
                isGameOver = true;
                if (Globals.player1Score > Globals.player2Score)
                {
                    message = "PLAYER 1 WINS!";
                } else
                {
                    message = "PLAYER 2 WINS!";
                }
            }

            // start new game and reset scores
            if (isGameOver)
            {
                if (currState.IsKeyDown(Keys.N))
                {
                    isGameOver = false;
                    Globals.player1Score = 0;
                    Globals.player2Score = 0;
                }
            }

            base.Update(gameTime);
            previousState = currState;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            

            Globals.spriteBatch.Begin();
            Globals.spriteBatch.DrawString(font, Globals.player1Score.ToString(), new Vector2(100, 50), Color.White);
            Globals.spriteBatch.DrawString(font, Globals.player2Score.ToString(), new Vector2(Globals.WIDTH - 120, 50), Color.White);
            ball.Draw(gameTime);
            player1.Draw(gameTime);
            player2.Draw(gameTime);

            if (isPaused)
            {
                // draw semi-transparent overlay
                Globals.spriteBatch.Draw(Globals.pixel, new Rectangle(0, 0, Globals.WIDTH, Globals.HEIGHT), Color.Black * 0.5f);
                Globals.spriteBatch.DrawString(font, "PAUSED", new Vector2(Globals.WIDTH / 2 - font.MeasureString("PAUSED").Length() / 2, Globals.HEIGHT / 2 - 20), Color.White);

            }
            if (isGameOver)
            {
                Globals.spriteBatch.Draw(Globals.pixel, new Rectangle(0, 0, Globals.WIDTH, Globals.HEIGHT), Color.Black * 0.5f);
                Globals.spriteBatch.DrawString(font, message, new Vector2(Globals.WIDTH / 2 - font.MeasureString(message).Length() / 2, Globals.HEIGHT / 2 - 20), Color.White);
                Globals.spriteBatch.DrawString(font, "press Q to quit. press N to start a new game", new Vector2(100, Globals.HEIGHT - 50), Color.White);
            }
            
            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
