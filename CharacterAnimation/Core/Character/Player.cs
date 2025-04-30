using System;

namespace CharacterAnimation
{
    public class Player : Character
    {
        public AnimationManager AnimationManager = new();
        private readonly float _speed = 200f;
        public Vector2 Position = new (300, 300);
        private string _lastDirection = "Down";

        private SpriteEffects _flip = SpriteEffects.None;

        public Player()
        {
            Texture = Globals.Content.Load<Texture2D>("Sprites/Player/Player_Idle_Run_Death_Anim");
            AnimationManager.AddAnimation("IdleDown", new Animation(Texture, 8, 13, 6, 0.1f, 0));
            AnimationManager.AddAnimation("IdleRight", new Animation(Texture, 8, 13, 6, 0.1f, 1));
            AnimationManager.AddAnimation("IdleLeft", new Animation(Texture, 8, 13, 6, 0.1f, 1));
            AnimationManager.AddAnimation("IdleUp", new Animation(Texture, 8, 13, 6, 0.1f, 2));

            AnimationManager.AddAnimation("RunDown", new Animation(Texture, 8, 13, 6, 0.1f, 3));
            AnimationManager.AddAnimation("RunRight", new Animation(Texture, 8, 13, 6, 0.1f, 4));
            AnimationManager.AddAnimation("RunLeft", new Animation(Texture, 8, 13, 6, 0.1f, 4));
            AnimationManager.AddAnimation("RunUp", new Animation(Texture, 8, 13, 6, 0.1f, 5));

        }


        public override void Update()
        {
            if(InputManager.Moving)
            {
                Position += Vector2.Normalize(InputManager.Direction) * _speed * Globals.TotalSeconds;
            }
            SetAnimations();

        }
        public void SetAnimations()
        {
            if (!InputManager.Moving)
            {
                AnimationManager.Update($"Idle{_lastDirection}");
                return;
            }

            var dir = Vector2.Normalize(InputManager.Direction);
            string directionKey = "Down";
            _flip = SpriteEffects.None;

            if (Math.Abs(dir.X) > Math.Abs(dir.Y))
            {
                directionKey = "Right";
                if (dir.X < 0) _flip = SpriteEffects.FlipHorizontally;
            }
            else
            {
                directionKey = dir.Y > 0 ? "Down" : "Up";
            }

            _lastDirection = directionKey;
            AnimationManager.Update($"Run{directionKey}");
        }

        public override void Draw()
        {
            AnimationManager.Draw(Position, _flip);
        }


    }
}
