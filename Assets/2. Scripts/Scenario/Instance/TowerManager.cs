using System.Collections.Generic;
using UnityEngine;
using LPE.SpacePartition;


namespace Core {
    public class TowerManager {
        public ITower[,] towers;
        public List<ITower> allTowers = new List<ITower>();

        public TowerManager(int w, int h) {
            towers = new ITower[w, h];
        }
    }
}
