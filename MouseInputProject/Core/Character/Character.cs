using Microsoft.Xna.Framework.Graphics;

namespace MouseInputProject
{
    public abstract class Character
    {
        public AnimationManager AnimationManger;


        public static Texture2D Texture;
        public bool IsFacingLeft { get; set; } = false;

        public Vector2 Position { get; set; }
        //public abstract Rectangle Rectangle { get; }
        public Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }
        public abstract void Update();
        public abstract void Draw();


    }
}
