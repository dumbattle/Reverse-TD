using LPE.Triangulation;
using LPE.Shape2D;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Core {
    public class CreepFunctions {
        ScenarioInstance s;
        CreepManager creepManager;
        TowerController towerController;


        CircleShape cachedCircleShape = new CircleShape(1);
        List<CreepInstance> creepResults = new List<CreepInstance>();

        public CreepFunctions(ScenarioInstance s, CreepManager creepManager, TowerController towerController) {
            this.s = s;
            this.creepManager = creepManager ?? throw new ArgumentNullException(nameof(creepManager));
            this.towerController = towerController;
        }

        public void AddCreep(CreepInstance c) {
            creepManager.AddCreep(c);
        }

        public void UpdateAllCreeps(ScenarioInstance s) {
            // iterate in reverse to make removal easier
            for (int i = creepManager.allCreeps.Count - 1; i >= 0; i--) {
                CreepInstance c = creepManager.allCreeps[i];
                c.Update(s);
                creepManager.grid.UpdateItem(c, LPE.Math.Geometry.CircleAABB(c.position, c.radius));
                
                
                if (s.towerFunctions.IsCollidingWithMainTower(c.position, c.radius)) {
                    s.playerFunctions.AddMoney(((int)(c.definition.moneyReward) * c.health.current / c.health.max));

                    towerController.health.DealDamage(c.health.current);
                    s.parameters.ui.healthBarPivot.transform.localScale = new Vector3((float)towerController.health.current / towerController.health.max, 1, 1);
                    DestroyCreep(c);
                }
            }
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

        /// <summary>
        /// 'results' is returned
        /// If 'results' is null, a new List is created and returned
        /// </summary>
        public List<CreepInstance> QueryCreeps(Vector2 location, float maxRange, List<CreepInstance> results) {
            if (results == null) {
                results = new List<CreepInstance>();
            }
            creepResults.Clear();
            cachedCircleShape.SetPosition(location);
            cachedCircleShape.SetScale(maxRange);
            creepManager.grid.QueryItems(cachedCircleShape.AABB(), creepResults);


            foreach (var c in creepResults) {
                var d = (c.position - location).sqrMagnitude;
                var maxDist = maxRange + c.radius;
                if (d < maxDist * maxDist) {
                    results.Add(c);
                }
            }
            return results;
        }

        public void DamageCreep(CreepInstance c, int amnt) {
            c.health.DealDamage(amnt);

            if (c.health.current <= 0) {
                DestroyCreep(c);
            }
        }

        public void DestroyCreep(CreepInstance c) {
            creepManager.RemoveCreep(c);
            c.Return();
        }


        public int CreepCount() {
            return creepManager.CreepCount();
        }
    }
}
