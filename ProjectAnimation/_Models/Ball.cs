using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectAnimation._Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAnimation._Models
{
    public class Ball
    {

        private static Texture2D _texture;
        private Vector2 _position;
        private readonly AnimationManager _animationManager = new();
        private string _currentAnimationKey = "Idle";
        private InputManager _inputManager = new();

        public Ball(Vector2 position)
        {
            _texture ??= Globals.Content.Load<Texture2D>("ballBounce");

            _animationManager.AddAnimation("Idle", new Animation(_texture, 11, 2, 0.1f, 0, 4, 1));
            _animationManager.AddAnimation("Jump", new Animation(_texture, 11, 2, 0.1f, 0, 10, 2));

            _inputManager.Space = Keys.Space;
            _position = position;
           
        }

        public void Update()
        {
            
            if (Keyboard.GetState().IsKeyDown(_inputManager.Space))
            {
                _currentAnimationKey = "Jump";
            }
            else
            {
                _currentAnimationKey = "Idle";
            }
            _animationManager.SetAnimation(_currentAnimationKey);
            _animationManager.Update(_currentAnimationKey);

        }

        public void Draw()
        {
            _animationManager.Draw(_position);
        }

    }
}
