using UnityEngine;
using System.Collections.Generic;



namespace Core {
    public abstract class CircleAOETower : TowerBehaviour {
        public SpriteRenderer atkRenderer;

        float animTimer = 0;
        float atkTimer = 0;
        List<CreepInstance> creepsInRange = new List<CreepInstance>();

        private void Awake() {
            var col = atkRenderer.color;
            col.a = 0;
            atkRenderer.color = col;
        }

        public override void GameUpdate(ScenarioInstance s) {
            float radius = GetRange();

            animTimer -= 1f / 15f;
            var scale = (radius + GetExtraRange()) * 2 * Mathf.Clamp01(1 - animTimer);
            atkRenderer.transform.localScale = new Vector3(scale, scale, 1);
            var col = atkRenderer.color;
            col.a = animTimer;
            atkRenderer.color = col;

            // update timer
            if (atkTimer > 0) {
                atkTimer -= FrameUtility.DeltaTime(true);
            }

            //check timer
            if (atkTimer > 0) {
                return;
            }

            // scan for in attack range
            var range = GetRange();
            creepsInRange.Clear();
            s.creepFunctions.QueryCreeps(position, range, creepsInRange);
            if (creepsInRange.Count == 0) {
                return;
            }

            // scan for creeps in damage range
            creepsInRange.Clear();
            s.creepFunctions.QueryCreeps(position, range + GetExtraRange(), creepsInRange);

            // damage creeps
            foreach (var c in creepsInRange) {
                DamageCreep(s, c);
            }
            atkTimer += GetAtkDelay();
            animTimer = 1;
        }

        public override void GetGeneralUpgradeOptions(List<UpgradeOption> results) {
        }

        public override void GetSpecializationUpgradeOptions(ScenarioInstance s, List<SpecializationUpgradeOptions> results) {
        }

        protected override int GetTotalUpgradeLevel() {
            return 0;
        }

        //******************************************************************************************************
        // virtual
        //******************************************************************************************************
        protected virtual void DamageCreep(ScenarioInstance s, CreepInstance c) {
            s.creepFunctions.DamageCreep(c, GetDamage());
        }
        //******************************************************************************************************
        // Abstract
        //******************************************************************************************************

        protected abstract float GetAtkDelay();
        protected abstract float GetRange();
        protected abstract float GetExtraRange();
        protected abstract int GetDamage();

    }
}
