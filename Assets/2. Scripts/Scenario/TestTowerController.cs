using UnityEngine;
using System.Collections.Generic;


namespace Core {
    public class TestTowerController : ITowerController {
        Health health = new Health(100_000);
        int money = 0;
        int towerPurchases = 0;

        TowerPlacementManager placementManager;

        List<ITower> activeTowers = new List<ITower>();
        List<ITower> walls = new List<ITower>();

        List<IMainTower> mainTowers = new List<IMainTower>();
        IMainTower mainTower;

        List<UpgradeOption> upgradeOptionsCache = new List<UpgradeOption>();
        List<SpecializationUpgradeOptions> specializationUpgradeOptionsCache = new List<SpecializationUpgradeOptions>();

        public void Init(ScenarioInstance s) {
            placementManager = new TowerPlacementManager(s);
            // main tower
            mainTower = (IMainTower)s.towerFunctions.AddMainTower(TowerDefinitionCatalog.main_Basic, s.parameters.mainTowerBl);
            placementManager.StartNewGroup(mainTower, true);
            mainTowers.Add(mainTower);
            // starting support towers
            var t1 = s.towerFunctions.AddTower(TowerDefinitionCatalog.gun_1, s.parameters.mainTowerBl + new Vector2Int(-2, -2));
            var t2 = s.towerFunctions.AddTower(TowerDefinitionCatalog.gun_1, s.parameters.mainTowerBl + new Vector2Int(3, 3));
            var t3 = s.towerFunctions.AddTower(TowerDefinitionCatalog.gun_1, s.parameters.mainTowerBl + new Vector2Int(-2, 3));
            var t4 = s.towerFunctions.AddTower(TowerDefinitionCatalog.gun_1, s.parameters.mainTowerBl + new Vector2Int(3, -2));

            placementManager.StartNewGroup(t1, false);
            placementManager.StartNewGroup(t2, false);
            placementManager.StartNewGroup(t3, false);
            placementManager.StartNewGroup(t4, false);
            activeTowers.Add(t1);
            activeTowers.Add(t2);
            activeTowers.Add(t3);
            activeTowers.Add(t4);
            // starting groups
            int area = s.mapQuery.width * s.mapQuery.height;
            int targetCount = area / 50;

            for (int i = 0; i < targetCount; i++) {
                var tile = placementManager.GetRandomGroupSpawnLocation();
                if (tile == new Vector2Int(-1, -1)) {
                    continue;
                }

                var t = s.towerFunctions.AddTower(s.parameters.wallTower, tile);
                walls.Add(t);
                placementManager.StartNewGroup(t, false);
            }

            for (int i = 0; i < targetCount; i++) {
                for (int j = 0; j < 5; j++) {
                    var t = placementManager.SpawnTowerRandom(s.parameters.wallTower, s.towerFunctions);
                    walls.Add(t);
                }
            }

        }

        public void OnRoundEnd(ScenarioInstance s) {
            money += 100 + 150 * s.roundManager.current;

            //// more walls
            //for (int i = 0; i < s.mapQuery.width * s.mapQuery.height / 40f; i++) {
            //    var t = placementManager.SpawnTowerRandom(TowerDefinitionCatalog.wall1, s.towerFunctions);
            //    walls.Add(t);
            //}

            // upgrade existing
            UpgradeTowers2(s);
        }

        public void OnCreepReachMainTower(ScenarioInstance s, CreepInstance c, IMainTower mainTower) {
            health.DealDamage(c.health.current);
            s.references.ui.healthBarPivot.transform.localScale = new Vector3((float)health.current / health.max, 1, 1);
        }

        public List<IMainTower> GetAllMainTowers() {
            return mainTowers;
        }

        void UpgradeTowers(ScenarioInstance s) {
            for (int i = 0; i < activeTowers.Count; i++) {
                ITower t = activeTowers[i];

                // select random available upgrade
                TowerUpgradeDetails up = null;
                float r = 0;

                foreach (var u in t.GetGeneralUpgrades()) {
                    if (!u.UpgradeAvailable()) {
                        continue;
                    }
                    r++;
                    if (UnityEngine.Random.value < 1f / r) {
                        up = u;
                    }
                }

                // apply upgrade
                if (up != null) {
                    up.IncrmentLevel();
                    t.Refresh();
                }

                // get random specialization
                specializationUpgradeOptionsCache.Clear();
                t.GetSpecializationUpgradeOptions(s, specializationUpgradeOptionsCache);

                TowerDefinition upgradeTo = null;
                r = 0;
                foreach (var spec in specializationUpgradeOptionsCache) {
                    r++;
                    if (UnityEngine.Random.value < 1f / r) {
                        upgradeTo = spec.upgradeResult;
                    }
                }

                // upgrade to specialization
                if (upgradeTo != null) {
                    var newTower = s.towerFunctions.ReplaceTower(t, upgradeTo);
                    activeTowers[i] = newTower;
                    newTower.Refresh();
                }
            }
        }

        public void UpgradeTowers2(ScenarioInstance s) {
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
                    money -= up.upgrade.CurrentUpgradeCost();
                    up.upgrade.IncrmentLevel();
                }
                else if (doSpec) {
                    var newTower = s.towerFunctions.ReplaceTower(spec.spec.current, spec.spec.upgradeResult);
                    activeTowers.Remove(spec.spec.current);
                    activeTowers.Add(newTower);
                    money -= spec.spec.cost;
                }
                else {
                    break;
                }

            }

            foreach (var t in activeTowers) {
                t.Refresh();
            }

            (TowerUpgradeDetails upgrade, float totalWeight) GetUpgrade() {
                // get options
                upgradeOptionsCache.Clear();

                foreach (var t in activeTowers) {
                    t.GetGeneralUpgradeOptions(upgradeOptionsCache);
                }

                // select options
                TowerUpgradeDetails upgrade = null;
                float r = 0;
                foreach (var opt in upgradeOptionsCache) {
                    if (!opt.upgrade.UpgradeAvailable()) {
                        continue;
                    }
                    if (opt.upgrade.CurrentUpgradeCost() > money) {
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
                specializationUpgradeOptionsCache.Clear();
                foreach (var t in activeTowers) {
                    t.GetSpecializationUpgradeOptions(s, specializationUpgradeOptionsCache);
                }

                // select option
                foreach (var opt in specializationUpgradeOptionsCache) {
                    if (opt.cost > money) {
                        continue;
                    }

                    var w = opt.weight;
                    r += w;

                    if (UnityEngine.Random.value < w / r) {
                        result = opt;
                    }
                }
                return (result, r);
            }
        }
    }
}
