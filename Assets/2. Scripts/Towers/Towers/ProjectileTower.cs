using UnityEngine;
using LPE;
using System.Collections.Generic;



namespace Core {
    public abstract class ProjectileTower<T> : TowerBehaviour where T : ProjectileBehaviour {
        public T projectileSrc;

        public Transform rotationPivot;
 
        
        float atkTimer = 0;

        ObjectPool<T> projectilePool;
        List<T> activeProjectiles = new List<T>();
        public float projectileRadius;

        private void Awake() {
            projectileSrc.gameObject.SetActive(false);
            projectilePool = new ObjectPool<T>(() => Instantiate(projectileSrc));
        }

        public override void GameUpdate(ScenarioInstance s) {
            // update projectiles
            for (int i = activeProjectiles.Count - 1; i >= 0; i--) {
                T proj = activeProjectiles[i];
                proj.GameUpdate(s);
                if (!proj.Active()) {
                    activeProjectiles.RemoveAt(i);
                    proj.gameObject.SetActive(false);
                    projectilePool.Return(proj);
                }
            }

            //check active
            if (!active) {
                return;
            }

            // update timer
            if (atkTimer > 0) {
                atkTimer -= FrameUtility.DeltaTime(true);
            }

            //check timer
            if (atkTimer > 0) {
                return;
            }

            // scan for creeps
            var range = GetRange();
            var target = s.creepFunctions.GetFurthestCreep(position, range);
            if (target == null) {
                return;
            }

            // make sure is in range
            var dist2 = (target.position - position).sqrMagnitude;
            if (dist2 > (range + target.radius) * (range + target.radius)) {
                return;
            }

            // execute attack
            atkTimer += GetAtkDelay();

            var dir = target.position - position;
            rotationPivot.up = dir;

            var p = projectilePool.Get();
            p.Init(
                s,
                position,
                dir,
                GetProjectileSpeed(),
                range * 1.2f,
                projectileRadius,
                GetDamage());
            SetProjectile(p);
            activeProjectiles.Add(p);
        }

        public override void EndRound() {
            for (int i = activeProjectiles.Count - 1; i >= 0; i--) {
                T proj = activeProjectiles[i];
                proj.gameObject.SetActive(false);
                projectilePool.Return(proj);
            }
            activeProjectiles.Clear();
        }

        public override void GetSpecializationUpgradeOptions(ScenarioInstance s, List<SpecializationUpgradeOptions> results) { }

        //******************************************************************************************************
        // Abstract
        //******************************************************************************************************

        protected abstract TowerDamageInstance GetDamage();
        protected abstract float GetAtkDelay();

        protected abstract float GetRange();

        protected abstract float GetProjectileSpeed();
        
        protected virtual void SetProjectile(T proj) {}
    }
}
