using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DhanmondiPong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Point _gameBounds = new Point(1280, 720);

        private Rectangle _paddleLeft;
        private Rectangle _paddleRight;
        private Vector2 _paddleLeftPosition;
        private Vector2 _paddleRightPosition; 
        private float _paddleSpeed = 400f;

        private Rectangle _ball;
        private Vector2 _ballPosition;
        private Vector2 _ballVelocity;
        private float _ballSpeed = 300f;
        private float _bounceSpeed = 20f;

        public Texture2D BallTexture;
        public Texture2D PaddleTexture;

        public KeyboardState KeyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = _gameBounds.X;
            _graphics.PreferredBackBufferHeight = _gameBounds.Y;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _paddleLeftPosition = new Vector2(8, 270);
            _paddleLeft = new Rectangle((int)_paddleLeftPosition.X, (int)_paddleLeftPosition.Y, 30, 150);

            _paddleRightPosition = new Vector2(1242, 270);
            _paddleRight = new Rectangle((int)_paddleRightPosition.X, (int)_paddleRightPosition.Y, 30, 150);


            _ballPosition = new Vector2(_gameBounds.X / 2, 200);
            _ball = new Rectangle((int)_ballPosition.X, (int)_ballPosition.Y, 20, 20);
            _ballVelocity = new Vector2(1, 0.1f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            BallTexture = Content.Load<Texture2D>("simple_ball");
            PaddleTexture = Content.Load<Texture2D>("simple_paddle");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float updatedPaddleSpeed = _paddleSpeed * elapsed;

            KeyboardState = Keyboard.GetState();

            _paddleLeft.Y = (int)_paddleLeftPosition.Y;
            _paddleRight.Y = (int)_paddleRightPosition.Y;

            _ball.X = (int)_ballPosition.X;
            _ball.Y = (int)_ballPosition.Y;
            _ballPosition.X += _ballVelocity.X * _ballSpeed * elapsed;
            _ballPosition.Y += _ballVelocity.Y * _ballSpeed * elapsed;

            BoundaryCollision();
            GetInput(KeyboardState, updatedPaddleSpeed);

            if (_paddleLeft.Intersects(_ball))
            {
                _ballVelocity.X *= -1;
                _ballVelocity.Y *= 1.1f;
                _ballPosition.X = _paddleLeft.X + _paddleLeft.Width + 10;
                _ballSpeed += _bounceSpeed;
            }

            if (_paddleRight.Intersects(_ball))
            {
                _ballVelocity.X *= -1;
                _ballVelocity.Y *= 1.1f;
                _ballPosition.X = _paddleRight.X - _paddleRight.Width - 10;
                _ballSpeed += _bounceSpeed;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(BallTexture, _ball, Color.White);
            _spriteBatch.Draw(PaddleTexture, _paddleLeft, Color.White);
            _spriteBatch.Draw(PaddleTexture, _paddleRight, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        //screen boundary collision
        private void BoundaryCollision()
        {
            if (_ballPosition.X < 0 - 15)
            {
                _ballPosition.X = 1 - 15;
                _ballVelocity.X *= -1;
                //_ballSpeed += _bounceSpeed;
            }

            else if (_ballPosition.X > _gameBounds.X - 15)
            {
                _ballPosition.X = _gameBounds.X - 1 - 15;
                _ballVelocity.X *= -1;
                //_ballSpeed += _bounceSpeed;
            }

            if (_ballPosition.Y < 0)
            {
                _ballPosition.Y = 1;
                _ballVelocity.Y *= -1;
                _ballSpeed += _bounceSpeed;
            }

            else if (_ballPosition.Y > _gameBounds.Y - 20)
            {
                _ballPosition.Y = _gameBounds.Y - 1 - 20;
                _ballVelocity.Y *= -1;
                _ballSpeed += _bounceSpeed;
            }
        }

        //keyboard input
        private void GetInput(KeyboardState keyboardState, float paddleSpeed)
        {
            if (keyboardState.IsKeyDown(Keys.W) && _paddleLeftPosition.Y > 0)
            {
                _paddleLeftPosition.Y -= paddleSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.S) && _paddleLeftPosition.Y < _gameBounds.Y - _paddleLeft.Height)
            {
                _paddleLeftPosition.Y += paddleSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Up) && _paddleRightPosition.Y > 0)
            {
                _paddleRightPosition.Y -= paddleSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Down) && _paddleRightPosition.Y < _gameBounds.Y - _paddleRight.Height)
            {
                _paddleRightPosition.Y += paddleSpeed;
            }

        }
    }
}
