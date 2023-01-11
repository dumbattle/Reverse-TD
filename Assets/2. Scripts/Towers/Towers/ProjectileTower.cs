using UnityEngine;
using LPE;
using System.Collections.Generic;



namespace Core {
    public class ProjectileTower : TowerBehaviour {
        public ProjectileBehaviour projectileSrc;

        public Transform rotationPivot;
        [Header("Base stats")]
        public float atkDelay = 0.25f;
        public int damage = 15;
        public float range = 5;
        public float projectileSpeed = 1;
        public float projectileRadius = 1;

        [Header("Upgrades")]
        public int dmgStep = 5;
        public float rangeScale = 1.25f;
        public float atkDelayScale = 0.875f;
        
        float atkTimer = 0;

        ObjectPool<ProjectileBehaviour> projectilePool;
        List<ProjectileBehaviour> activeProjectiles = new List<ProjectileBehaviour>();




        TowerUpgradeDetails dmgUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.DAMAGE, 25, 50, 100, 175, 275);
        TowerUpgradeDetails spdUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.SPEED, 45, 100, 210, 375, 595);
        TowerUpgradeDetails rangeUpgrade = new TowerUpgradeDetails(TowerUpgradeIdUtility.RANGE, 30, 60, 120, 210, 340);
        
        
        private void Awake() {
            projectileSrc.gameObject.SetActive(false);
            projectilePool = new ObjectPool<ProjectileBehaviour>(() => Instantiate(projectileSrc));
        }

        public override void GameUpdate(ScenarioInstance s) {
            for (int i = activeProjectiles.Count - 1; i >=0; i--) {
                ProjectileBehaviour proj = activeProjectiles[i];
                proj.GameUpdate(s);
                if (!proj.Active()) {
                    activeProjectiles.RemoveAt(i);
                    proj.gameObject.SetActive(false);
                    projectilePool.Return(proj);
                }
            }
            // update timer
            if (atkTimer > 0) {
                atkTimer -= 1f / 60f;
            }

            //check timer
            if (atkTimer > 0) {
                return;
            }

            // scan for creeps
            float r = 1;
            for (int i = 0; i < rangeUpgrade.currentLevel; i++) {
                r *= rangeScale;
            }
            var target = s.creepFunctions.GetNearestCreep(position, r * range);
            if (target == null) {
                return;
            }

            var delay = atkDelay;
            for (int i = 0; i < spdUpgrade.currentLevel; i++) {
                delay *= atkDelayScale;
            }
            atkTimer += delay;

            var dir = target.position - position;
            rotationPivot.up = dir;

            var p = projectilePool.Get();
            p.Init(s, position, dir, projectileSpeed / r, range * 2, projectileRadius, damage + dmgStep * dmgUpgrade.currentLevel);
            activeProjectiles.Add(p);

        }

        public override void EndRound() {
            for (int i = activeProjectiles.Count - 1; i >= 0; i--) {
                ProjectileBehaviour proj = activeProjectiles[i];
                proj.gameObject.SetActive(false);
                projectilePool.Return(proj);
            }
            activeProjectiles.Clear();
        }

        public override void GetUpgradeOptions(List<UpgradeOption> results) {
            //results.Add(new UpgradeOption(dmgUpgrade, 1));
            results.Add(new UpgradeOption(spdUpgrade, 1));
            //results.Add(new UpgradeOption(rangeUpgrade, 1));
        }


        protected override int GetTotalUpgradeLevel() {
            return dmgUpgrade.currentLevel + spdUpgrade.currentLevel + rangeUpgrade.currentLevel;
        }
    }
}
