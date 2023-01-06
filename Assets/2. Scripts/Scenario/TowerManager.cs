using System.Collections.Generic;
using UnityEngine;
using LPE.SpacePartition;


namespace Core {
    public class TowerManager {
        public ITower[,] towers;
        public List<ITower> allTowers = new List<ITower>();

        public ITower target;
        public Vector2 targetLocation;
        public Grid2D<ITower> grid;

        public TowerManager(int w, int h) {
            towers = new ITower[w, h];
            grid = new Grid2D<ITower>(new Vector2(0, 0), new Vector2(w, h), new Vector2Int(w, h));
        }
    }
}
