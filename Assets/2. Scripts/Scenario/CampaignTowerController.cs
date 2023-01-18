using UnityEngine;
using System.Collections.Generic;


namespace Core {
    public class CampaignTowerController : ITowerController {

        //--------------------------------------------------------------------------------------------------
        // Fixed
        //--------------------------------------------------------------------------------------------------

        ITower mainTower;
        HashSet<TowerDefinition> availableUpgrades;

        int initialMoney = 100;
        int moneyPerRoundFlat = 100;
        int moneyPerRoundScale = 150;

        //--------------------------------------------------------------------------------------------------
        // Dynamic
        //--------------------------------------------------------------------------------------------------

        Health health = new Health(100_000);
        int money = 0;
        int towerPurchases = 0;


        List<ITower> activeTowers = new List<ITower>();
        List<ITower> walls = new List<ITower>();

        //--------------------------------------------------------------------------------------------------
        // Cache
        //--------------------------------------------------------------------------------------------------

        List<UpgradeOption> upgradeOptionsCache = new List<UpgradeOption>();
        List<SpecializationUpgradeOptions> specializationUpgradeOptionsCache = new List<SpecializationUpgradeOptions>();


        //*********************************************************************************************************************************
        // Builds
        //*********************************************************************************************************************************

        /// <summary>
        /// Make sure to TowerDefinitionCatalogue to get the tower definitions
        /// </summary>
        public void SetAvailableUpgrades(HashSet<TowerDefinition> availableUpgrades) {
            this.availableUpgrades = availableUpgrades;

        }
        /// <summary>
        /// Make sure to TowerDefinitionCatalogue to get the tower definitions
        /// </summary>
        public void SetAvailableUpgrades(params TowerDefinition[] availableUpgrades) {
            this.availableUpgrades = this.availableUpgrades ?? new HashSet<TowerDefinition>();

            foreach (var u in availableUpgrades) {
                if (this.availableUpgrades.Contains(u)) {
                    continue;
                }
                this.availableUpgrades.Add(u);
            }

        }

        public void SetMoneyStats(int initial = 100, int perRoundFlat = 150, int perRoundScale = 10) {
            initialMoney = initial;
            moneyPerRoundFlat = perRoundFlat;
            moneyPerRoundScale = perRoundScale;
        }

        public void SetMaxHealth(int amnt) {
            health.Reset(amnt);
        }
        //*********************************************************************************************************************************
        // ITowerController
        //*********************************************************************************************************************************

        public void Init(ScenarioInstance s) {
            // main tower
            mainTower = s.towerFunctions.AddMainTower(TowerDefinitionCatalog.main_Basic, s.parameters.mainTowerBl);

            // walls
            foreach (var wallIndex in s.parameters.walls) {
                var w = s.towerFunctions.AddTower(s.parameters.wallTower, wallIndex);
                walls.Add(w);
            }

            // starting towers
            foreach (var (index, towerDef) in s.parameters.startingTowers) {
                var t = s.towerFunctions.AddTower(towerDef, index);
                activeTowers.Add(t);
            }

            //initial money
            money = initialMoney;
        }

        public void OnRoundEnd(ScenarioInstance s) {
            money += moneyPerRoundFlat + moneyPerRoundScale * s.roundManager.current;

            // upgrade existing
            UpgradeTowers(s);
        }

        public void OnCreepReachMainTower(ScenarioInstance s, CreepInstance c, ITower mainTower) {
            health.DealDamage(c.health.current);
            var scale = (float)health.current / health.max;
            scale = Mathf.Clamp01(scale);
            s.references.ui.healthBarPivot.transform.localScale = new Vector3(scale, 1, 1);
        }

        //*********************************************************************************************************************************
        // Helpers
        //*********************************************************************************************************************************

        void UpgradeTowers(ScenarioInstance s) {
            while (true) {
                var up = GetUpgrade();
                var spec = GetSpecialization();

                bool doUp = up.upgrade != null;
                bool doSpec = spec.spec.current != null;

                if (doUp && doSpec) {
                    if (Random.value < (up.totalWeight / (up.totalWeight + spec.totalWeight))) {
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
            // Helpers
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
                float totalWeight = 0;

                // get options
                specializationUpgradeOptionsCache.Clear();
                foreach (var t in activeTowers) {
                    t.GetSpecializationUpgradeOptions(s, specializationUpgradeOptionsCache);
                }

                // select option
                foreach (var opt in specializationUpgradeOptionsCache) {
                    // too expensive
                    if (opt.cost > money) {
                        continue;
                    }

                    // upgrade is allowed
                    if (availableUpgrades != null) {
                        if (!availableUpgrades.Contains(opt.upgradeResult)) {
                            continue;
                        }
                    }

                    // resovoiur sample
                    var w = opt.weight;
                    totalWeight += w;

                    if (Random.value < w / totalWeight) {
                        result = opt;
                    }
                }
                return (result, totalWeight);
            }
        }

    }
}
