using LPE;
using UnityEngine;
using LPE.Steering;
using System.Collections.Generic;


namespace Core {
    public class CreepInstance : ISteerAgent {
        static ObjectPool<CreepInstance> _pool = new ObjectPool<CreepInstance>(() => new CreepInstance());
        CreepInstance() { }

        public static CreepInstance Get(ScenarioInstance s, CreepDefinition def) {
            var result = _pool.Get();
            result.definition = def;
            result.health = new Health((int)def.hp);
            result.direction = new Vector2(0, 0);

            result.distTraveled = 0;
            result.offset = Random.insideUnitCircle * (.5f - def.radius);
            result.path = s.parameters.creepPathfinder.GetPath(s);
            result.position = result.path[0] + result.offset;

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
        float distTraveled;
        Vector2 offset;
        List<Vector2Int> path;

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
            distTraveled += GetCurrentSpeed() / 60f;
            var tileA = path[(int)distTraveled];
            var tileB = path[(int)distTraveled + 1];


            // set position + direction
            position = Vector2.Lerp(tileA, tileB, distTraveled % 1) + offset;
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



    }
}
