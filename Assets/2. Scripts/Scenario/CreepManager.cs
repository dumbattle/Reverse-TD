using UnityEngine;
using LPE.SpacePartition;
using System.Collections.Generic;

namespace Core {
    public class CreepManager {
        public Grid2D<CreepInstance> grid;
        List<CreepInstance> allCreeps = new List<CreepInstance>();

        public CreepManager(int w, int h) {
            grid = new Grid2D<CreepInstance>(new Vector2(-2, -2), new Vector2(w + 2, h + 2), new Vector2Int(w + 4, h + 4));
        }

        public void AddCreep(CreepInstance c) {
            grid.Add(c, LPE.Math.Geometry.CircleAABB(c.position, c.radius));
            allCreeps.Add(c);
        }
        
        public void RemoveCreep(CreepInstance c) {
            grid.Remove(c);
            allCreeps.Remove(c);
        }

        public void UpdateAllCreeps(ScenarioInstance s) {
            foreach (var c in allCreeps) {
                c.Update(s);
                grid.UpdateItem(c, LPE.Math.Geometry.CircleAABB(c.position, c.radius));
            }
        }
        public int CreepCount() {
            return allCreeps.Count;
        }
    }
}
