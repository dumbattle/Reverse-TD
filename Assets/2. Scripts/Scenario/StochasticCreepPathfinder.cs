using LPE;
using UnityEngine;
using System.Collections.Generic;


namespace Core {
    public class StochasticCreepPathfinder : ICreepPathfinder {
        public List<Vector2Int> GetPath(ScenarioInstance s) {
            var result = new List<Vector2Int>();
            result.Add(s.mapQuery.GetRandomCreepSpawn());
            var currentTile = result[0];
            var safety = new LoopSafety(1000);
            safety.SetException("Loopo safety reached");
            while (true) {
                safety.Inc();

                var nextTile = GetDestinationTile(s, currentTile);
                result.Add(nextTile);
                currentTile = nextTile;

                var t = s.mapQuery.GetTile(currentTile.x, currentTile.y);
                if (t.distFromTarget <= 0) {
                    break;
                }
            }
            return result;
        }

        Vector2Int GetDestinationTile(ScenarioInstance s, Vector2Int current) {
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

        public void DrawBehaviours(ScenarioInstance s) { }
    }
}
