namespace CameraProject
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void CalculateTranslation(Player actor, Map map)
        {
            var dx = (Globals.ScreenWidth / 2) - actor.Position.X;
            dx = MathHelper.Clamp(dx, -map.MapSize.X + Globals.ScreenWidth + (map.TileSize.X / 2), map.TileSize.X / 2 - map.TileSize.X);
            var dy = (Globals.ScreenHeight / 2) - actor.Position.Y;
            dy = MathHelper.Clamp(dy, -map.MapSize.Y + Globals.ScreenHeight + (map.TileSize.Y / 2), map.TileSize.Y / 2 - map.TileSize.Y);
            Transform = Matrix.CreateTranslation(
                dx,
                dy,
                0);
        }
    }
}
