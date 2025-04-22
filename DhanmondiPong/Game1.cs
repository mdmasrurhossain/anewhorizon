using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DhanmondiPong
{
    public partial class Game1 : Game
    {
        private Random _random = new Random();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Point _gameBounds = new Point(1280, 720);
        private Texture2D _backgroundTexture;

        public Paddle PaddleLeft;
        public Paddle PaddleRight;
        public float PaddleSpeed = 400f;
        public Vector2 PaddleLeftStartPosition;
        public Vector2 PaddleRightStartPosition;
        public int PaddleWidth = 30;
        public int PaddleHeight = 150;
  

        private Rectangle _ball;
        private Vector2 _ballPosition;
        private Vector2 _ballVelocity;
        private float _ballSpeed = 300f;
        private float _bounceSpeed = 20f;
        public Texture2D BallTexture;

        private SpriteFont _scoreFont;
        private SpriteFont _winnerFont;
        private int _scoreLeft = 0;
        private int _scoreRight = 0;
        private int _winningScore = 10;

        private AudioSource _soundFX;
        private int _jungleCounter = 0;

        public KeyboardState KeyboardState;

        public enum GameState { Playing, GameOver }
        private GameState _gameState = GameState.Playing;


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

            PaddleLeftStartPosition = new Vector2(8, (_gameBounds.Y / 2) - (PaddleHeight / 2));
            PaddleRightStartPosition = new Vector2(_gameBounds.X - PaddleWidth - 8, (_gameBounds.Y / 2) - (PaddleHeight / 2));

            PaddleLeft = new Paddle(PaddleLeftStartPosition, PaddleSpeed, PaddleWidth, PaddleHeight);
            PaddleRight = new Paddle(PaddleRightStartPosition, PaddleSpeed, PaddleWidth, PaddleHeight);

            _soundFX = new AudioSource();

            ResetBall();
            _ballVelocity = new Vector2(1, 0.1f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            BallTexture = Content.Load<Texture2D>("simple_ball");
            PaddleLeft.Texture = Content.Load<Texture2D>("simple_paddle");
            PaddleRight.Texture = Content.Load<Texture2D>("simple_paddle");
            _backgroundTexture = Content.Load<Texture2D>("simple_background");

            _scoreFont = Content.Load<SpriteFont>("Score");
            _winnerFont = Content.Load<SpriteFont>("Winner");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (_gameState == GameState.GameOver) return;
            if (_scoreLeft >= _winningScore || _scoreRight >= _winningScore) _gameState = GameState.GameOver;

            KeyboardState = Keyboard.GetState();

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float updatedPaddleSpeed = PaddleSpeed * elapsed;

            _ball.X = (int)_ballPosition.X;
            _ball.Y = (int)_ballPosition.Y;
            _ballPosition.X += _ballVelocity.X * _ballSpeed * elapsed;
            _ballPosition.Y += _ballVelocity.Y * _ballSpeed * elapsed;

            BoundaryCollision();
            GetInput(KeyboardState, updatedPaddleSpeed);

            if (PaddleLeft.Bounds.Intersects(_ball))
            {
                _ballVelocity.X *= -1;
                _ballVelocity.Y *= 1.1f;
                _ballPosition.X = PaddleLeft.Position.X + PaddleLeft.Width + 10;
                _ballSpeed += _bounceSpeed;
                _soundFX.PlayWave(220.0f, 50, WaveType.Sin, 0.3f);
            }

            if (PaddleRight.Bounds.Intersects(_ball))
            {
                _ballVelocity.X *= -1;
                _ballVelocity.Y *= 1.1f;
                _ballPosition.X = PaddleRight.Position.X - PaddleRight.Width - 10;
                _ballSpeed += _bounceSpeed;
                _soundFX.PlayWave(220.0f, 50, WaveType.Sin, 0.3f);
            }

            #region Play Reset Jingle
            //use jingle counter as a timeline to play notes
            _jungleCounter++;

            int speed = 7;
            if (_jungleCounter == speed * 1) { _soundFX.PlayWave(440.0f, 100, WaveType.Sin, 0.2f); }
            else if (_jungleCounter == speed * 2) { _soundFX.PlayWave(523.25f, 100, WaveType.Sin, 0.2f); }
            else if (_jungleCounter == speed * 3) { _soundFX.PlayWave(659.25f, 100, WaveType.Sin, 0.2f); }
            else if (_jungleCounter == speed * 4) { _soundFX.PlayWave(783.99f, 100, WaveType.Sin, 0.2f); }
            //only play this jingle once
            else if (_jungleCounter > speed * 4) { _jungleCounter = int.MaxValue - 1; }
            #endregion Play Reset Jingle

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(_scoreFont, $"{_scoreLeft}", new Vector2(((_gameBounds.X / 2) - 40), 0), Color.Black);
            _spriteBatch.DrawString(_scoreFont, $"{_scoreRight}", new Vector2(((_gameBounds.X / 2) + 24), 0), Color.Black);

            _spriteBatch.Draw(BallTexture, _ball, Color.White);

            PaddleLeft.Draw(_spriteBatch);
            PaddleRight.Draw(_spriteBatch);


            if(_gameState == GameState.GameOver)
            {
                string winner = _scoreLeft >= _winningScore ? "Left Player Wins!" : "Right Player Wins!";
                _spriteBatch.DrawString(_winnerFont, $"{winner}", new Vector2(((_gameBounds.X / 2) - 200), _gameBounds.Y / 2), Color.Black);
     
            }
                

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        //boundary collision
        private void BoundaryCollision()
        {
            if (_ballPosition.X < 0 - 15)
            {
                _ballPosition.X = 1 - 15;
                _scoreRight++;
                _soundFX.PlayWave(440.4f, 50, WaveType.Square, 0.3f);
                ResetBall();
            }

            else if (_ballPosition.X > _gameBounds.X - 15)
            {
                _ballPosition.X = _gameBounds.X - 1 - 15;
                _scoreLeft++;
                _soundFX.PlayWave(440.4f, 50, WaveType.Square, 0.3f);
                ResetBall();
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
            if (keyboardState.IsKeyDown(Keys.W) && PaddleLeft.Bounds.Y > 0)
            {
                PaddleLeft.Bounds.Y -= (int)paddleSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.S) && PaddleLeft.Bounds.Y < _gameBounds.Y - PaddleLeft.Height)
            {
                PaddleLeft.Bounds.Y += (int)paddleSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Up) && PaddleRight.Bounds.Y > 0)
            {
                PaddleRight.Bounds.Y -= (int)paddleSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Down) && PaddleRight.Bounds.Y < _gameBounds.Y - PaddleRight.Height)
            {
                PaddleRight.Bounds.Y += (int)paddleSpeed;
            }

        }

        private void ResetBall()
        {
            _ballSpeed = 350f;
            _ballPosition = new Vector2(_gameBounds.X / 2, _random.Next(10, _gameBounds.Y));
            _ball = new Rectangle((int)_ballPosition.X, (int)_ballPosition.Y, 20, 20);
            
            _jungleCounter = 0;
        }

    }
}
