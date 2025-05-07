namespace MouseInputProject
{
    public class Button
    {
        private MouseState _currentMouseState;

        private MouseState _previousMouseState;

        private SpriteFont _font;

        private bool _isHovering;
        private Texture2D _texture;


        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Color PenColor { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width * 3, _texture.Height * 3);
            }
        }

        public string Text { get; set; }

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;

            PenColor = Color.Black;
        }

        public void Update(Matrix cameraTransform, Player player)
        {

            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            // Convert mouse screen position to world coordinates
            var inverse = Matrix.Invert(cameraTransform);
            var mousePosition = new Vector2(_currentMouseState.X, _currentMouseState.Y);
            var worldMousePosition = Vector2.Transform(mousePosition, inverse);

            var mouseRectangle = new Rectangle(
                (int)worldMousePosition.X,
                (int)worldMousePosition.Y,
                1,
                1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;
                if (_currentMouseState.LeftButton == ButtonState.Pressed &&
                    _previousMouseState.LeftButton == ButtonState.Released)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }

            // 👇 Debug print current position
            //System.Diagnostics.Debug.WriteLine($"Button Pos: {Position}");
        }

        public void Draw()
        {

            System.Diagnostics.Debug.WriteLine($"Button Pos: {Position}");
            var color = _isHovering ? Color.Gray : Color.White;

            Globals.SpriteBatch.Draw(_texture, Rectangle, color);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + Rectangle.Width / 2) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + Rectangle.Height / 2) - (_font.MeasureString(Text).Y / 2);

                Globals.SpriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
            }

            // Debug outline (optional)
            //Texture2D pixel = new Texture2D(Globals.SpriteBatch.GraphicsDevice, 1, 1);
            //pixel.SetData(new[] { Color.Red });
            //Globals.SpriteBatch.Draw(pixel, Rectangle, Color.Red * 0.3f);
        }

    }
}
