using UnityEngine;
using System;


namespace Core {
    public class TowerFunctions {
        TowerManager towerManager;
        ScenarioInstance s;

        public TowerFunctions(ScenarioInstance s, TowerManager towerManager) {
            this.towerManager = towerManager ?? throw new ArgumentNullException(nameof(towerManager));
            this.s = s;
        }

        public int numTowers => towerManager.allTowers.Count;
      
        public IMainTower AddMainTower(TowerDefinition tower, Vector2Int loc) {
            var t = (IMainTower)tower.GetNewInstance(s, loc);

            AddTower(t);

            return t;

        }

        public ITower AddTower(TowerDefinition tower, Vector2Int loc) {
            var t = tower.GetNewInstance(s, loc);
            AddTower(t);
            return t;
        }

        void AddTower(ITower t) {
            Vector2Int bl = t.GetBottomLeft();
            Vector2Int tr = t.GetTopRight();

            // add to grid
            for (int x = bl.x; x <= tr.x; x++) {
                for (int y = bl.y; y <= tr.y; y++) {
                    towerManager.towers[x, y] = t;
                }
            }

            // add to list
            towerManager.allTowers.Add(t);
            
            // recalculate 
            s.mapQuery.CalculateTileDistances(s.parameters.towerController);
        }

        public void UpdateAllTowers() {
            foreach (var t in towerManager.allTowers) {
                t.GameUpdate(s);
            }
        }

        public void EndRound() {
            foreach (var t in towerManager.allTowers) {
                t.EndRound();
            }
        }

        public ITower GetTower (Vector2Int loc) {
            return towerManager.towers[loc.x, loc.y];
        }

        //***********************************************************************************************************************************
        // Helpers
        //***********************************************************************************************************************************

        public ITower ReplaceTower(ITower t, TowerDefinition upgrade) {
            t.OnBeforeUpgrade(s);
            var newTower = upgrade.GetNewInstance(s, t.GetBottomLeft());
            
            Vector2Int bl = t.GetBottomLeft();
            Vector2Int tr = t.GetTopRight();

            // replace in grid 
            for (int x = bl.x; x <= tr.x; x++) {
                for (int y = bl.y; y <= tr.y; y++) {
                    towerManager.towers[x, y] = newTower;
                }
            }

            // replace in list
            towerManager.allTowers.Remove(t);
            towerManager.allTowers.Add(newTower);

            //// replace in placement
            //placementManager.ReplaceTower(t, newTower);

            // transfer upgrades
            foreach (var up in t.GetGeneralUpgrades()) {
                newTower.TransferGeneralUpgrade(up);
            }

            // destroy old
            t.Destroy();

            return newTower;
        }
    }
}
