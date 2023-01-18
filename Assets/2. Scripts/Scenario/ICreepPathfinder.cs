using UnityEngine;
using System.Collections.Generic;


namespace Core {
    public interface ICreepPathfinder {
        List<Vector2Int> GetPath(ScenarioInstance s);
    }
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
    }
}
