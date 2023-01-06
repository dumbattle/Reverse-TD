using LPE.Triangulation;
using LPE.Shape2D;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Core {
    public class CreepFunctions {
        ScenarioInstance s;
        CreepManager creepManager;


        Dictionary<CreepInstance, CreepBehaviour> creep2behaviour = new Dictionary<CreepInstance, CreepBehaviour>();

        CircleShape cachedCircleShape = new CircleShape(1);
        List<CreepInstance> creepResults = new List<CreepInstance>();

        public CreepFunctions(ScenarioInstance s, CreepManager creepManager) {
            this.s = s;
            this.creepManager = creepManager ?? throw new ArgumentNullException(nameof(creepManager));
        }

        public void AddCreep(CreepInstance c) {
            creepManager.AddCreep(c);
            var behaviour = GameObject.Instantiate(s.parameters.creepSrc);
            behaviour.AssignCreep(s, c);
            creep2behaviour.Add(c, behaviour);
        }
        
        public void UpdateAllCreeps(ScenarioInstance s) {
            creepManager.UpdateAllCreeps(s);
        }

        public CreepInstance GetNearestCreep(Vector2 location, float maxRange) {
            creepResults.Clear();

            cachedCircleShape.SetPosition(location);
            cachedCircleShape.SetScale(maxRange);
            creepManager.grid.QueryItems(cachedCircleShape.AABB(), creepResults);

            CreepInstance closet = null;
            float dist = Mathf.Infinity;

            foreach (var c in creepResults) {
                var d = (c.position - location).sqrMagnitude;
                if (d < dist) {
                    closet = c;
                    dist = d;
                }
            }
            return closet;
        }
 
        public void DamageCreep(CreepInstance c, int amnt) {
            c.health.DealDamage(amnt);

            if (c.health.current <= 0) {
                creepManager.RemoveCreep(c);
                GameObject.Destroy(creep2behaviour[c].gameObject);
                creep2behaviour.Remove(c);
                c.Return();
            }
        }

        public int CreepCount() {
            return creepManager.CreepCount();
        }
    }
}
