namespace CharacterAnimation
{
    public class GameManager
    {
        public Player Player;

        public void Initialize()
        {
            Player = new Player();
        }

        public void Update()
        {
            InputManager.Update();
            Player.Update();
        }

        public void Draw()
        {
            Player.Draw();
        }

    }
}
