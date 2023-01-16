﻿using LPE;
using UnityEngine;
using LPE.Steering;

namespace Core {
    public class CreepInstance : ISteerAgent {
        static ObjectPool<CreepInstance> _pool = new ObjectPool<CreepInstance>(() => new CreepInstance());
        CreepInstance() { }

        public static CreepInstance Get(ScenarioInstance s, CreepDefinition def, Vector2Int pos) {
            var result = _pool.Get();
            result.definition = def;
            result.position = pos;
            result.health = new Health((int)def.hp);
            result.direction = new Vector2(0, 0);

            result.tileDist = 0;
            result.offset = Random.insideUnitCircle * (.5f - def.radius);
            result.tileA = pos + result.offset;
            result.destTile = GetDestinationTile(s, pos);
            result.tileB = result.destTile + result.offset;

            result.slowLevel = 0;
            result.slowTimer = 0;

            result.hpRegen = 0;
            return result;
        }

        public Vector2 position { get; set; }
        public Vector2 direction { get; set; }
        public float radius => definition.radius;

        public CreepDefinition definition;
        public Health health;

        //--------------------------------------------------------------------------------------
        // State
        //--------------------------------------------------------------------------------------

        float hpRegen;

        //.............................................................................
        // Pathfinding
        //.............................................................................

        float tileDist;
        Vector2 offset;
        Vector2 tileA;
        Vector2 tileB;
        Vector2Int destTile;

        //--------------------------------------------------------------------------------------
        // Status
        //--------------------------------------------------------------------------------------

        float slowLevel;
        float slowTimer;

        //.............................................................................
        // Apply
        //.............................................................................

        public void ApplySlow(float strength, float time) {
            slowLevel = Mathf.Sqrt(slowLevel * slowLevel + strength * strength);
            slowTimer = Mathf.Sqrt(slowTimer * slowTimer + time * time);
        }
        
        //**********************************************************************************************************
        // Control
        //**********************************************************************************************************

        public void Update(ScenarioInstance s) {
            // hp regen
            hpRegen += health.max * definition.hpRegenRate / 60f;
            var healAmnt = (int)hpRegen;
            health.AddHealth(healAmnt);
            hpRegen -= healAmnt;
            // update slow
            slowTimer -= 1f / 60f;
            if (slowTimer < 0) {
                slowLevel = 0;
            }

            // move 
            tileDist += GetCurrentSpeed() / 60f;

            // update tiles destinations
            while (tileDist > 1) {
                tileDist--;
                tileA = tileB;
                destTile = GetDestinationTile(s, destTile);
                tileB = destTile + offset;
            }

            // set position + direction
            position = Vector2.Lerp(tileA, tileB, tileDist);
            direction = tileB - tileA;
        }

        public void Return() {
            _pool.Return(this);
        }

        //**********************************************************************************************************
        // Helpers
        //**********************************************************************************************************

        float GetCurrentSpeed() {
            return definition.speed / (slowLevel + 1f);
        }


        public static Vector2Int GetDestinationTile(ScenarioInstance s, Vector2Int current) {
            var d = s.mapQuery.GetTile(current.x, current.y).distFromTarget;

            var top = current + new Vector2Int(0, 1);
            var right = current + new Vector2Int(1, 0);
            var bottom = current + new Vector2Int(0, -1);
            var left = current + new Vector2Int(-1, 0);


            var dt = s.mapQuery.IsInRange(top.x, top.y) ? s.mapQuery.GetTile(top.x, top.y).distFromTarget : 999999;
            var dr = s.mapQuery.IsInRange(right.x, right.y) ? s.mapQuery.GetTile(right.x, right.y).distFromTarget : 999999;
            var db = s.mapQuery.IsInRange(bottom.x, bottom.y) ? s.mapQuery.GetTile(bottom.x, bottom.y).distFromTarget : 999999;
            var dl = s.mapQuery.IsInRange(left.x, left.y) ? s.mapQuery.GetTile(left.x, left.y).distFromTarget : 999999;
            var result = current;
            float r = 1;

            if (dt < d) {
                result = top;
                d = dt;
                r = 1;
            }
            else if (d == dt) {
                r++;
                if (Random.value < 1f / r) {
                    result = top;
                    d = dt;
                }
            }
            
            if (dr < d) {
                result = right;
                d = dr;
                r = 1;
            }
            else if (d == dr) {
                r++;
                if (Random.value < 1f / r) {
                    result = right;
                    d = dr;
                }
            }

            if (db < d) {
                result = bottom;
                d = db;
                r = 1;
            }
            else if (d == db) {
                r++;
                if (Random.value < 1f / r) {
                    result = bottom;
                    d = db;
                }
            }

            if (dl < d) {
                result = left;
            }
            else if (d == dl) {
                r++;
                if (Random.value < 1f / r) {
                    result = left;
                }
            }

            return result;
        }
    }
}
