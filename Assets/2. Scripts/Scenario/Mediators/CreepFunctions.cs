using LPE.Triangulation;
using LPE.Shape2D;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Core {
    public class CreepFunctions {
        ScenarioInstance s;
        CreepManager creepManager;


        CircleShape cachedCircleShape = new CircleShape(1);
        List<CreepInstance> creepResults = new List<CreepInstance>();

        public CreepFunctions(ScenarioInstance s, CreepManager creepManager) {
            this.s = s;
            this.creepManager = creepManager ?? throw new ArgumentNullException(nameof(creepManager));
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

                // check collision with main towers
                foreach (var mt in s.parameters.towerController.GetAllMainTowers()) {
                    // collision check
                    cachedCircleShape.SetScale(c.radius);
                    cachedCircleShape.SetPosition(c.position);

                    var mainShape = mt.GetShape();
                    bool collision =  mainShape.CheckCollision(cachedCircleShape);

                    if (collision) {
                        // money
                        float hpScale = (float)c.health.current / c.health.max;
                        s.playerFunctions.AddMoney(Mathf.CeilToInt(c.definition.moneyReward * hpScale));

                        // notify tower controller
                        s.parameters.towerController.OnCreepReachMainTower(s, c, mt);

                        // creep done
                        DestroyCreep(c);
                        
                        break;
                    }
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
