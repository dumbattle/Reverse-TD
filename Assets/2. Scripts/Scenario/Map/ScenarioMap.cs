using UnityEngine;

namespace Core {
    public class ScenarioMap {
        Tile[,] tiles;
        
        public int width => tiles.GetLength(0);
        public int height => tiles.GetLength(1);

        public ScenarioMap(int width, int height) {
            tiles = new Tile[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    tiles[x, y] = new Tile();
                }
            }
        }
    }
}
