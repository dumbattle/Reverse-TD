using LPE;
using UnityEngine;
using LPE.Steering;

namespace Core {
    public class CreepInstance : ISteerAgent {
        static ObjectPool<CreepInstance> _pool = new ObjectPool<CreepInstance>(() => new CreepInstance());
        CreepInstance() { }

        public static CreepInstance Get(CreepDefinition def, Vector2 pos) {
            var result = _pool.Get();
            result.definition = def;
            result.position = pos;
            result.health = new Health(def.hp);
            result.direction = new Vector2(0, 0);
            return result;
        }

        public Vector2 position { get; set; }
        public Vector2 direction { get; set; }
        public float radius => definition.radius;

        public CreepDefinition definition;
        public Health health;

        public void Update(ScenarioInstance s) {
            var d = s.creepPathfinder.GetDirection(this);
            d = s.creepPathfinder.SteerCreep(this, d);

            d = d * (1f / 60f);
            direction = d;
            position = s.creepPathfinder.GetUnitMoveDestination(position, d, radius, this);
        }

        public void Return() {
            _pool.Return(this);
        }
    }
}
