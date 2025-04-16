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

        private Rectangle _ball;
        private Vector2 _ballPosition;
        private Vector2 _ballVelocity;
        private float _ballSpeed = 15f;

        public Texture2D ballTexture;

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

            _ballPosition = new Vector2(_gameBounds.X / 2, 200);
            _ball = new Rectangle((int)_ballPosition.X, (int)_ballPosition.Y, 35, 35);
            _ballVelocity = new Vector2(1, 0.1f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("simple_ball");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _ball.X = (int)_ballPosition.X;
            _ball.Y = (int)_ballPosition.Y;
            _ballPosition.X += _ballVelocity.X * _ballSpeed;
            _ballPosition.Y += _ballVelocity.Y * _ballSpeed;

            if(_ballPosition.X < 0 - 15)
            {
                _ballPosition.X = 1 - 15;
                _ballVelocity.X *= -1;
            }

            else if (_ballPosition.X > _gameBounds.X - 15)
            {
                _ballPosition.X = _gameBounds.X - 1 - 15;
                _ballVelocity.X *= -1;
            }

            if(_ballPosition.Y < 0 - 15)
            {
                _ballPosition.Y = 1 - 15;
                _ballVelocity.Y *= -1;
            }

            else if(_ballPosition.Y > _gameBounds.Y - 20)
            {
                _ballPosition.Y = _gameBounds.Y - 1 - 20;
                _ballVelocity.Y *= -1;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(ballTexture, _ball, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
