using UnityEngine;
using System.Collections.Generic;


namespace Core {
    public class ScenarioMap {
        public Tile[,] tiles;
        
        public int width => tiles.GetLength(0);
        public int height => tiles.GetLength(1);

        public List<List<Vector2Int>> groupedPaths = new List<List<Vector2Int>>();

        public ScenarioMap(int width, int height) {
            tiles = new Tile[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    tiles[x, y] = new Tile();
                    tiles[x, y].distFromTarget = width * height;
                }
            }
        }
    }
}
