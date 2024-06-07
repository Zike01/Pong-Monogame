using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace PongGame
{
    public static class Globals
    {
        public static SpriteBatch spriteBatch;
        public static Texture2D pixel;
        public static SoundEffect effect;
        public static int WIDTH = 800, HEIGHT = 600;
        public static int player1Score = 0, player2Score = 0;
    }
}
