using UnityEngine;

namespace Core {

    public class ProjectileTower : TowerBehaviour {
        [SerializeField] Transform rotationPivot;
        float atkDelay = 0.25f;
        int damage = 15;
        float range = 5;

        float atkTimer = 0;

        public override void GameUpdate(ScenarioInstance s) {
            // update timer
            if (atkTimer > 0) {
                atkTimer -= 1f / 60f;
            }

            //check timer
            if (atkTimer > 0) {
                return;
            }

            // scan for creeps
            var target = s.creepFunctions.GetNearestCreep(position, range);
            if (target == null) {
                return;
            }
            atkTimer += atkDelay;
            s.creepFunctions.DamageCreep(target, damage);

            var dir = target.position - position;
            rotationPivot.up = dir;
        }

    }
}
