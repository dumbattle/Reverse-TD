using LPE;
using UnityEngine;
using LPE.Steering;
using System.Collections.Generic;


namespace Core {
    public struct TowerDamageInstance {
        public DamageType damageType;
        public float value;

        public TowerDamageInstance(DamageType damageType, float value) {
            this.damageType = damageType;
            this.value = value;
        }
    }

    public class CreepInstance {
        static ObjectPool<CreepInstance> _pool = new ObjectPool<CreepInstance>(() => new CreepInstance());
        CreepInstance() { }

        public static CreepInstance Get(ScenarioInstance s, CreepDefinition def, CreepSquad squad) {
            var result = _pool.Get();
            result.squad = squad;
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
            result.carrierSpawnTimer = ((def.carrierDefinition?.spawnInterval) ?? 0) * (Random.value / 2);
            return result;
        }
        public static CreepInstance GetChild(ScenarioInstance s, CreepDefinition def, CreepInstance parent, CreepSquad squad) {
            var result = _pool.Get();
            result.squad = squad;
            result.definition = def;
            result.health = new Health((int)def.hp);
            result.direction = new Vector2(0, 0);

            result.distTraveled = parent.distTraveled - Random.value;
            result.distTraveled = Mathf.Max(result.distTraveled, 0);
            result.offset = Random.insideUnitCircle * (.5f - def.radius);
            result.path = parent.path;

            result.slowLevel = 0;
            result.slowTimer = 0;

            result.hpRegen = 0;
            result.SetPosition();
            return result;
        }

        public Vector2 position { get; set; }
        public Vector2 direction { get; set; }

        public Health health;
        public float radius => GetCurrentRadius();
        CreepDefinition definition;
        
        public CreepSquad squad { get; private set; }

        //--------------------------------------------------------------------------------------
        // Accessors
        //--------------------------------------------------------------------------------------

        public CreepDefinition GetDeathSplitDefinition() {
            return definition.deathSplitDefinition;
        }

        public Sprite GetSprite() {
            return definition.sprite;
        }
        
        public Color GetGlowColor() {
            return definition.glowColor;
        }

        public ResourceCollection GetMaxMoneyReward() {
            return definition.resourceReward;
        }

        public float IncomeMultiplier() {
            return definition.incomeScale;
        }

        public float NumberOfCreepsInSquad() {
            return definition.count;
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
       
        //.............................................................................
        // Pathfinding
        //.............................................................................

        float carrierSpawnTimer = 0;

        //--------------------------------------------------------------------------------------
        // Status
        //--------------------------------------------------------------------------------------

        float slowLevel;
        float slowTimer;


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
            SetPosition();

            // carrier spawn
            if (definition.carrierDefinition != null) {
                carrierSpawnTimer -= deltaTime;
                if (carrierSpawnTimer < 0) {
                    carrierSpawnTimer += definition.carrierDefinition.spawnInterval;

                    var child = GetChild(s, definition.carrierDefinition, this, squad.GetCarrierSquad());
                    s.creepFunctions.AddCreep(child);
                }
            }
        }

        public void Return() {
            _pool.Return(this);
        }

        //.............................................................................
        // Modify
        //.............................................................................

        public void ApplySlow(float strength, float time) {
            slowLevel = Mathf.Sqrt(slowLevel * slowLevel + strength * strength);
            slowTimer = Mathf.Sqrt(slowTimer * slowTimer + time * time);
        }

        /// <summary>
        /// Returns actual damage dealt.  Does not check for death or other effects besides logging
        /// </summary>
        public float ApplyDamage(TowerDamageInstance dmg) {
            var armor = definition.armor;

            var result = dmg.damageType.ApplyArmor(dmg.value, armor);
            health.DealDamage(result);
            return result;
        }
        //**********************************************************************************************************
        // Query
        //**********************************************************************************************************

        public int GetTowerDamage() {
            return Mathf.CeilToInt(health.current * definition.damageScale);
        }
        public float GetCurrentSpeed() {
            var hpScale = Mathf.Lerp(definition.speedMinHpScale, 1, health.Ratio());
            return definition.speed / (slowLevel + 1f) * hpScale;
        }
        
        public float GetCurrentRadius() {
            var scale = Mathf.Lerp(definition.shrinkMinHp, 1, health.Ratio());
            return definition.radius * scale;

        }

        public float EstimatedDistanceFromTarget() {
            float total = path.Count - 1;
            return total - distTraveled;
        }

        void SetPosition() {

            var tileA = path[(int)distTraveled];
            var tileB = path[(int)distTraveled + 1];

            // set position + direction
            position = Vector2.Lerp(tileA, tileB, distTraveled % 1) + offset;
            direction = tileB - tileA;
        }
    }
}
