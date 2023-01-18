using UnityEngine;
using LPE;
using LPE.SpacePartition;
using System.Collections.Generic;

namespace Core {
    public class CreepManager {
        public Grid2D<CreepInstance> grid;
        public List<CreepInstance> allCreeps = new List<CreepInstance>();
        ObjectPool<CreepBehaviour> behaviourPool;
        ScenarioInstance s;
        Dictionary<CreepInstance, CreepBehaviour> creep2behaviour = new Dictionary<CreepInstance, CreepBehaviour>();

        public CreepManager(ScenarioInstance s,int w, int h) {
            this.s =s;
            grid = new Grid2D<CreepInstance>(new Vector2(-2, -2), new Vector2(w + 2, h + 2), new Vector2Int(w + 4, h + 4));
            behaviourPool = new ObjectPool<CreepBehaviour>(() => GameObject.Instantiate(s.references.creepSrc), 100);
        }

        public void AddCreep(CreepInstance c) {
            grid.Add(c, LPE.Math.Geometry.CircleAABB(c.position, c.radius));
            allCreeps.Add(c);

            var behaviour = behaviourPool.Get();
            behaviour.AssignCreep(s, c, behaviourPool);
            creep2behaviour.Add(c, behaviour);

        }
        
        public void RemoveCreep(CreepInstance c) {
            grid.Remove(c);
            allCreeps.Remove(c);
            creep2behaviour[c].PlayDeathAnim();
            creep2behaviour.Remove(c);
        }

        public int CreepCount() {
            return allCreeps.Count;
        }
    }
}
