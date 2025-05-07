
namespace MouseInputProject
{
    public class GameManager
    {
        public Camera Camera;
        public Player Player;
        public Map Map;
        public Button InteractiveButton;

        public void Initialize()
        {
            Map = new Map();
            InteractiveButton = new Button(Globals.Content.Load<Texture2D>("Sprites/Button/Cave_Floor_Decoration"), Globals.Content.Load<SpriteFont>("Fonts/ButtonFont"))
            {
                Position = new Vector2(500, 300),
                Text = "Click Me!",
                PenColor = Color.White
                
            };

            InteractiveButton.Click += RandomButton_Click;

            Player = new Player();
            Camera = new Camera();
        }

        private void RandomButton_Click(object sender, EventArgs e)
        {
            var random = new Random();
            Map.BackgroundColor = new Color(random.Next(256), random.Next(256), random.Next(256));
        }

        public void Update()
        {
            InputManager.Update();
            InteractiveButton.Update(Camera.Transform, Player);
            Player.Update();
            Camera.CalculateTranslation(Player, Map);
        }

        public void Draw()
        {
            Map.Draw();
            InteractiveButton.Draw();
            Player.Draw();
        }

    }
}
