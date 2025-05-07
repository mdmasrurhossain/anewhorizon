namespace MouseInputProject
{
    public class Animation
    {
        private Texture2D _texture;
        public List<Rectangle> _sourceRectagles = [];
        private int _currentFrame;
        private int _frames;
        private float _timePerFrame;
        private float _frameTimeLeft;
        private bool _isActive = true;

        public Animation(Texture2D texture, int framesX, int framesY, int activeFrames, float frameTime,  int row = 0)
        {
            _texture = texture;
            _frames = activeFrames;
            _timePerFrame = frameTime;
            _frameTimeLeft = _timePerFrame;

            var frameWidth = _texture.Width / framesX;
            var frameHeight = _texture.Height / framesY;
            
            for(int i = 0; i < _frames; i++)
            {
                _currentFrame = 0;
                _sourceRectagles.Add(new (i * frameWidth, row * frameHeight, frameWidth, frameHeight));
            }

        }

        public void Stop()
        {
            _isActive = false;
        }

        public void Start()
        {
            _isActive = true;
        }

        public void Reset()
        {
            _currentFrame = 0;
            _frameTimeLeft = _timePerFrame;
        }

        public void Update()
        {
            if (!_isActive) return;
            _frameTimeLeft -= Globals.TotalSeconds;

            if (_frameTimeLeft <= 0)
            {
               _frameTimeLeft += _timePerFrame;
                _currentFrame = (_currentFrame + 1) % _frames;


            }
        }

        public void Draw(Vector2 position, SpriteEffects flip = SpriteEffects.None)
        {
            Globals.SpriteBatch.Draw(_texture, position, _sourceRectagles[_currentFrame], Color.White, 0f, Vector2.Zero, 3f, flip, 1);
        }


    }

    
}
