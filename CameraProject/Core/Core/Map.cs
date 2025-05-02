using System;

namespace CameraProject
{
    public class Map
    {

        private Dictionary<Vector2, int> _tree, _grass, _ground, _underground;
        private Texture2D _farmAtlas, _grassAtlas, _treeAtlas, _waterAtlas;

        public Point TileSize { get; } = new(48, 48);
        public Point MapSize { get; } = new(1440, 1440);

        public Map()
        {
            _farmAtlas = Globals.Content.Load<Texture2D>("./Sprites/Tiles/farm");
            _grassAtlas = Globals.Content.Load<Texture2D>("./Sprites/Tiles/grass");
            _treeAtlas = Globals.Content.Load<Texture2D>("./Sprites/Tiles/tree");
            _waterAtlas = Globals.Content.Load<Texture2D>("./Sprites/Tiles/water");


            _tree = LoadMap("../../../Content/Maps/FarmMaps/practiceLevel_tree.csv");
            _grass = LoadMap("../../../Content/Maps/FarmMaps/practiceLevel_grass.csv");
            _ground = LoadMap("../../../Content/Maps/FarmMaps/practiceLevel_ground.csv");
            _underground = LoadMap("../../../Content/Maps/FarmMaps/practiceLevel_underground.csv");


        }

        private Dictionary<Vector2, int> LoadMap(string filepath)
        {
            Dictionary<Vector2, int> map = new();
            StreamReader reader = new(filepath);

            int y = 0;
            int maxWidth = 0;
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');

                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int id))
                    {
                        if (id > -1)
                        {
                            map[new Vector2(x, y)] = id;
                        }
                    }
                }
                maxWidth = Math.Max(maxWidth, items.Length); // track widest row
                y++;

            }
            return map;

        }

        private void DrawMap(Dictionary<Vector2, int> map, Texture2D atlas)
        {
            int pixelSize = 16;
            int tilesPerRow = atlas.Width / pixelSize;
            int updatedPixelSize = 3 * pixelSize;
            foreach (KeyValuePair<Vector2, int> tile in map)
            {
                int tileID = tile.Value;
                int tileX = tileID % tilesPerRow;
                int tileY = tileID / tilesPerRow;
                Globals.SpriteBatch.Draw(atlas, new Rectangle((int)tile.Key.X * updatedPixelSize, (int)tile.Key.Y * updatedPixelSize, updatedPixelSize, updatedPixelSize), new Rectangle(tileX * pixelSize, tileY * pixelSize, pixelSize, pixelSize), Color.White);
            }
        }


        public void Draw()
        {
            DrawMap(_underground, _waterAtlas);
            DrawMap(_ground, _farmAtlas);
            DrawMap(_grass, _grassAtlas);
            DrawMap(_tree, _treeAtlas);
        }
    }
}
