using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAnimation._Models
{
    public class Animation
    {
        private readonly Texture2D _texture;
        private readonly List<Rectangle> _sourceRectangles = [];
        private int _frames;
        private int _currentFrame;
        private float _frameTime;
        private float _frameTimeLeft;
        private bool _active = true;
        private readonly List<int> _validFrameIndices = new();

        public Animation(Texture2D texture, int framesX, int framesY, float frameTime, int startFrame = 0, int endFrame = -1, int row = 1)
        {
            _texture = texture; 
            _frameTime = frameTime;
            _frameTimeLeft = _frameTime;
            _frames = framesX;
            var frameWidth = _texture.Width / framesX;
            var frameHeight = _texture.Height / framesY;


            for (int i = 0; i < _frames; i++)
            {
                _sourceRectangles.Add(new(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight));
            }

            if(endFrame == -1)
            {
                endFrame = _frames - 1;
            }

            for(int i = startFrame; i <= endFrame; i++)
            {
                _validFrameIndices.Add(i);
            }

        }

        public void Stop()
        {
            _active = false;
        }

        public void Start()
        {
            _active = true;
        }

        public void Reset()
        {
            _currentFrame = 0;
            _frameTimeLeft = _frameTime;
        }

        public void Update()
        {
            if (!_active) return;

            _frameTimeLeft -= Globals.TotalSeconds;

            if (_frameTimeLeft <= 0)
            {
                _frameTimeLeft += _frameTime;
                _currentFrame = (_currentFrame + 1) % _validFrameIndices.Count;

            }
        }

        public void Draw(Vector2 position)
        {
            Globals.SpriteBatch.Draw(_texture, position, _sourceRectangles[_currentFrame], Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 1);
        }

    }
}
