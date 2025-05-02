namespace CameraProject
{
    public class GameManager
    {
        public Camera Camera;
        public Player Player;
        public Map Map;

        public void Initialize()
        {
            Map = new Map();

            Player = new Player();
            Camera = new Camera();
        }

        public void Update()
        {
            InputManager.Update();
            Player.Update();
            Camera.CalculateTranslation(Player, Map);
        }

        public void Draw()
        {
            Map.Draw();
            Player.Draw();
        }

    }
}
