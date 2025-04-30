namespace CharacterAnimation
{
    public abstract class Character
    {
        public AnimationManager AnimationManger;

        public static Texture2D Texture;
        public bool IsFacingLeft { get; set; } = false;

        public abstract void Update();
        public abstract void Draw();

    }
}
