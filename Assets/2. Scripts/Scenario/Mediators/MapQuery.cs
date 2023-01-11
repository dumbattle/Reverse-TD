using UnityEngine;
using System.Collections.Generic;


namespace Core {
    public class MapQuery {
        ScenarioMap map;
        TowerManager towerManager;

        Queue<Vector2Int> queue = new Queue<Vector2Int>();

        public MapQuery(ScenarioMap m, TowerManager towerManager) {
            map = m;
            this.towerManager = towerManager;
        }


        public int width => map.width;
        public int height => map.height;

        public Tile GetTile(int x, int y) {
            return map.tiles[x, y];
        }

        public bool ValidTowerRange(int x, int y) {
            return
                x >= 1 &&
                y >= 1 &&
                x < width - 1 &&
                y < height - 1;
        }
        public bool IsInRange(int x, int y) {
            return
                x >= 0 &&
                y >= 0 &&
                x < width &&
                y < height;
        }
        public Vector2Int GetRandomCreepSpawn() {
            Vector2Int corner; ;
            Vector2Int delta;
            int maxRange;

            if (Random.value < 1f * width / (width + height)) {
                delta = new Vector2Int(0, 1);
                maxRange = height;
            }
            else {
                delta = new Vector2Int(1, 0);
                maxRange = width;
            }

            if (Random.value < 0.5f) {
                corner = new Vector2Int(0, 0);
            }
            else {
                corner = new Vector2Int(width - 1, height - 1);
                delta *= -1;
            }
            var result = corner + delta * (Random.Range(0, maxRange));
            return result;
        }


        public Vector2 TileToWorld(float x, float y) {
            return new Vector2(x - width / 2f + 0.5f, y - height / 2f + 0.5f);
        }
        public Vector2 TileToWorld(Vector2 v) {
            return TileToWorld(v.x, v.y);
        }

        public Vector2Int WorldToTileIndex(Vector2 world) {
            return new Vector2Int(
                Mathf.FloorToInt(world.x + width / 2f),
                Mathf.FloorToInt(world.y + height / 2f)
                );
        }

        public Vector2 WorldToTile(Vector2 world) {
            return new Vector2(
                world.x + width / 2f - 0.5f,
                world.y + height / 2f - 0.5f
                );
        }

        public void CalculateTileDistances() {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    map.tiles[x, y] = new Tile();
                    map.tiles[x, y].distFromTarget = width * height;
                }
            }
            queue.Clear();
            // seed
            var bl = towerManager.target.GetBottomLeft();
            var s = towerManager.target.Size;

            for (int x = 0; x < s; x++) {
                for (int y = 0; y < s; y++) {
                    var index = bl + new Vector2Int(x, y);

                    var tile = map.tiles[index.x, index.y];
                    tile.distFromTarget = 0;
                    queue.Enqueue(index);
                }
            }

            // main loop
            while (queue.Count > 0) {
                var index = queue.Dequeue();
                var top = index + new Vector2Int(0, 1);
                var right = index + new Vector2Int(1, 0);
                var bottom = index + new Vector2Int(0, -1);
                var left = index + new Vector2Int(-1, 0);
                int dist = map.tiles[index.x, index.y].distFromTarget + 1;
                ProcessTile(top, dist);
                ProcessTile(right, dist);
                ProcessTile(bottom, dist);
                ProcessTile(left, dist);
                // helper function 
                void ProcessTile(Vector2Int tileIndex, int dist) {
                    // in range
                    if (!IsInRange(tileIndex.x, tileIndex.y)) {
                        return;
                    }
                    // occupied by tower
                    if (towerManager.towers[tileIndex.x, tileIndex.y] != null) {
                        return;
                    }
                    // no need to update
                    var tile = map.tiles[tileIndex.x, tileIndex.y];

                    if (tile.distFromTarget <= dist) {
                        return;
                    }
                    // update
                    tile.distFromTarget = dist;
                    queue.Enqueue(tileIndex);
                }

            }
        }
    }
}
