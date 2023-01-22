using LPE;
using UnityEngine;
using LPE.Steering;
using System.Collections.Generic;


namespace Core {
    public class CreepInstance {
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

        public Health health;
        public float radius => GetCurrentRadius();
        CreepDefinition definition;

        //--------------------------------------------------------------------------------------
        // Accessors
        //--------------------------------------------------------------------------------------

     

        public Sprite GetSprite() {
            return definition.sprite;
        }
        
        public Color GetGlowColor() {
            return definition.glowColor;
        }

        public float GetMaxMoneyReward() {
            return definition.moneyReward;
        }

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
            float deltaTime = FrameUtility.DeltaTime(true);

            // hp regen
            hpRegen += health.max * definition.hpRegenRate * deltaTime;
            var healAmnt = (int)hpRegen;
            health.AddHealth(healAmnt);
            hpRegen -= healAmnt;
            
            // update slow status
            slowTimer -= deltaTime;
            if (slowTimer < 0) {
                slowLevel = 0;
            }

            // move 
            distTraveled += GetCurrentSpeed() * deltaTime;
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
        // Query
        //**********************************************************************************************************

        public float GetCurrentSpeed() {
            var hpScale = Mathf.Lerp(definition.speedMinHpScale, 1, health.Ratio());
            return definition.speed / (slowLevel + 1f) * hpScale;
        }
        
        public float GetCurrentRadius() {
            var scale = Mathf.Lerp(definition.shrinkMinHp, 1, health.Ratio());
            return definition.radius * scale;

        }
    }
}
