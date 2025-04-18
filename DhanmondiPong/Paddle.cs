using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DhanmondiPong
{
    public partial class Game1
    {
        public class Paddle
        {
            public Rectangle Bounds;
            public Vector2 Position;
            public float Speed { get; }
            public int Height { get; }
            public int Width { get; }
            public Texture2D Texture;

            public Paddle(Vector2 position, float speed, int width, int height)
            {
                Position = position;
                Speed = speed;
                Height = height;
                Width = width;
                Bounds = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            }


            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(Texture, Bounds, Color.White);
            }

        }
    }
}
