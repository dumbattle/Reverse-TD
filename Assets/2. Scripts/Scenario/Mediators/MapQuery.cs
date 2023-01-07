using UnityEngine;


namespace Core {
    public class MapQuery {
        ScenarioMap map;

        public MapQuery(ScenarioMap m) {
            map = m;
        }


        public int width => map.width;
        public int height => map.height;


        public Vector2Int GetRandomSpawn() {
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
                corner = new Vector2Int(-1, -1);
            }
            else {
                corner = new Vector2Int(width, height);
                delta *= -1;
            }
            var result = corner + delta * (1 + Random.Range(0, maxRange));
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
    }
}
