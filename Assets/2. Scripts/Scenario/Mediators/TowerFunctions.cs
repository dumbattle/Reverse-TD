using LPE.Triangulation;
using UnityEngine;
using System;
using LPE.Shape2D;
using System.Collections.Generic;


namespace Core {
    public class TowerFunctions {
        TowerManager towerManager;
        TowerPlacementManager placementManager;
        ScenarioInstance s;


        CircleShape cachedCircle = new CircleShape(1);
        List<UpgradeOption> generalUpgradeCache = new List<UpgradeOption>();
        List<SpecializationUpgradeOptions> specializationUpgradeCache = new List<SpecializationUpgradeOptions>();

        public TowerFunctions(ScenarioInstance s, TowerManager towerManager, TowerPlacementManager placementManager) {
            this.towerManager = towerManager ?? throw new ArgumentNullException(nameof(towerManager));
            this.s = s;
            this.placementManager = placementManager;
        }

        public int numTowers => towerManager.allTowers.Count;

        public void AddTowerRandomPlacement(TowerDefinition towerDef) {
            var t = placementManager.SpawnTowerRandom(towerDef);
            if (t == null) {
                return;
            }
            AddTower(t);
            s.mapQuery.CalculateTileDistances();
        }
      
        public void AddStartingGroups(TowerDefinition wall) {
            int area = s.mapQuery.width * s.mapQuery.height;
            int targetCount = area / 50;

            for (int i = 0; i < targetCount; i++) {
                var tile = placementManager.GetRandomGroupSpawnLocation();
                if (tile == new Vector2Int(-1, -1)) {
                    continue;
                }

                var t = wall.GetNewInstance(s, tile);
                AddTower(t);
                placementManager.StartNewGroup(t, false);
            }

            for (int i = 0; i < targetCount; i++) {
                for (int j = 0; j < 5; j++) {
                    var t = placementManager.SpawnTowerRandom(wall);
                    AddTower(t);
                }
            }
       
            s.mapQuery.CalculateTileDistances();
        }

        public void AddMainTower(TowerDefinition main, TowerDefinition startingSupport) {
            var bl = s.parameters.mainTowerBl;
            var mainTower = main.GetNewInstance(s, bl);
          
            var size = main.size;
            var size2 = startingSupport.size;

            var t1 = startingSupport.GetNewInstance(s, bl + new Vector2Int(-1 - size2, -1 - size2));
            var t2 = startingSupport.GetNewInstance(s, bl + new Vector2Int(1 + size, 1 + size));
            var t3 = startingSupport.GetNewInstance(s, bl + new Vector2Int(-1 - size2, 1 + size));
            var t4 = startingSupport.GetNewInstance(s, bl + new Vector2Int(1 + size, -1 - size2));
            AddMainTower(mainTower);
            AddTower(t1);
            AddTower(t2);
            AddTower(t3);
            AddTower(t4);
            placementManager.StartNewGroup(mainTower, true);
            placementManager.StartNewGroup(t1, false);
            placementManager.StartNewGroup(t2, false);
            placementManager.StartNewGroup(t3, false);
            placementManager.StartNewGroup(t4, false);
            s.mapQuery.CalculateTileDistances();
        }

        void AddMainTower(ITower t) {
            AddTower(t);
            towerManager.target = t;

            Vector2Int bl = t.GetBottomLeft();
            Vector2Int tr = t.GetTopRight();
            towerManager.targetLocation = (Vector2)(bl + tr) / 2f;

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

        public bool IsCollidingWithMainTower(Vector2 pos, float radius) {
            cachedCircle.radius = radius;
            cachedCircle.SetPosition(pos);

            var mainShape = towerManager.target.GetShape();
            return mainShape.CheckCollision(cachedCircle);
        }
    
        public void UpgradeTowers() {
            while (true) {
                var up = GetUpgrade();
                var spec = GetSpecialization();
                
                bool doUp = up.upgrade != null;
                bool doSpec = spec.spec.current != null;

                if (doUp && doSpec) {
                    if (UnityEngine.Random.value < (up.totalWeight / (up.totalWeight + spec.totalWeight))) {
                        doSpec = false;
                    }
                    else {
                        doUp = false;
                    }
                }

                if (doUp) {
                    s.towerController.money -= up.upgrade.CurrentUpgradeCost();
                    up.upgrade.IncrmentLevel();
                }
                else if (doSpec) {
                    ReplaceTower(spec.spec.current, spec.spec.upgradeResult);
                    s.towerController.money -= spec.spec.cost;
                }
                else {
                    break;
                }

            }

            foreach (var t in towerManager.allTowers) {
                t.Refresh();
            }

            (TowerUpgradeDetails upgrade, float totalWeight) GetUpgrade() {
                // get options
                generalUpgradeCache.Clear();

                foreach (var t in towerManager.allTowers) {
                    if (t == towerManager.target) {
                        continue;
                    }
                    t.GetGeneralUpgradeOptions(generalUpgradeCache);
                }

                // select options
                TowerUpgradeDetails upgrade = null;
                float r = 0;
                foreach (var opt in generalUpgradeCache) {
                    if (!opt.upgrade.UpgradeAvailable()) {
                        continue;
                    }
                    if (opt.upgrade.CurrentUpgradeCost() > s.towerController.money) {
                        continue;
                    }
                    var w = 1;
                    r += w;
                    if (UnityEngine.Random.value < w / r) {
                        upgrade = opt.upgrade;
                    }
                }
                return (upgrade, r);
            }
        
            (SpecializationUpgradeOptions spec, float totalWeight) GetSpecialization() {
                SpecializationUpgradeOptions result = new SpecializationUpgradeOptions();
                float r = 0;

                // get options
                specializationUpgradeCache.Clear();
                foreach (var t in towerManager.allTowers) {
                    if (t == towerManager.target) {
                        continue;
                    }
                    t.GetSpecializationUpgradeOptions(s, specializationUpgradeCache);
                }

                // select option
                foreach (var opt in specializationUpgradeCache) {
                    if (opt.cost > s.towerController.money) {
                        continue;
                    }
                    
                    var w =  opt.weight;
                    r += w;

                    if (UnityEngine.Random.value < w / r) {
                        result = opt;
                    }
                }
                return (result, r);
            }
        }
        
        //***********************************************************************************************************************************
        // Helpers
        //***********************************************************************************************************************************

        void ReplaceTower(ITower t, TowerDefinition upgrade) {
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

            // replace in placement
            placementManager.ReplaceTower(t, newTower);

            // transfer upgrades
            foreach (var up in t.GetGeneralUpgrades()) {
                newTower.TransferGeneralUpgrade(up);
            }

            // destroy old
            t.Destroy();
        }
    }
}
