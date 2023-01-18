using UnityEngine;
using System.Collections.Generic;


namespace Core {
    public class SetCreepPathfinder : ICreepPathfinder {
        List<List<Vector2Int>> paths = new List<List<Vector2Int>>();
        bool randomizeOrder;
        int currentIndex = -1;

        public SetCreepPathfinder(bool randomizeOrder, params List<Vector2Int>[] paths) {
            this.randomizeOrder = randomizeOrder;
            this.paths.AddRange(paths);
        }

        public List<Vector2Int> GetPath(ScenarioInstance s) {
            if (randomizeOrder) {
                return paths[Random.Range(0, paths.Count)];
            }

            currentIndex++;
            currentIndex %= paths.Count;
            return paths[currentIndex];

        }

        public void DrawBehaviours(ScenarioInstance s) {
            foreach (var p in paths) {
                // start
                var start = p[0];
                var t = s.mapQuery.GetTile(start.x, start.y);

                var spawnObj = TileSpriteCache.Magenta.GetSpawn();
                spawnObj.transform.position = t.behaviour.transform.position;

                // path
                for (int i = 1; i < p.Count - 1; i++) {
                    var current = p[i];
                    var prev = p[i - 1];
                    var next = p[i + 1];

                    bool up = current.y + 1 == next.y || current.y + 1 == prev.y;
                    bool down = current.y - 1 == next.y || current.y - 1 == prev.y;
                    bool right = current.x + 1 == next.x || current.x + 1 == prev.x;
                    bool left = current.x - 1 == next.x || current.x - 1 == prev.x;
                    s.mapQuery.GetTile(current.x, current.y).behaviour.SetSprite(TileSpriteCache.Magenta.GetPath(up, right, down, left));
                }
            }
        }
    }
}
