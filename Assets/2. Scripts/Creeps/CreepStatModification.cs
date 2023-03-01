namespace Core {
    public class CreepStatModification {
        public float regenRate;
        public int deathSpawnLevel;
        public int carrierSpawnLevel;
        public ResourceCollection resourceReward = new ResourceCollection();

        StatEntry size = new StatEntry();
        StatEntry hp = new StatEntry();
        StatEntry spawnRate = new StatEntry();
        StatEntry count = new StatEntry();
        StatEntry spd = new StatEntry();
        StatEntry dmg = new StatEntry();
        StatEntry shrinkMinHp = new StatEntry();
        StatEntry speedMinHpScale = new StatEntry();

        public CreepStatModification() {
            Reset();
        }

        public void Apply(CreepDefinition def) {
            if (def == null) {
                return;
            }

            def.radius *= size.GetRatio();
            def.hp *= hp.GetRatio();
            def.resourceReward.Add(resourceReward);
            //def.spawnRate *= spawnRate.GetRatio();
            def.count *= count.GetRatio();
            def.speed *= spd.GetRatio();
            def.damageScale *= dmg.GetRatio();
            def.radius = UnityEngine.Mathf.Min(def.radius, 0.45f);
            def.hpRegenRate += regenRate;
            def.shrinkMinHp *= shrinkMinHp.GetRatio();
            def.speedMinHpScale *= speedMinHpScale.GetRatio();
        }

        public void Reset() {
            size.Reset();
            hp.Reset();
            resourceReward.Reset();
            spawnRate.Reset();
            count.Reset();
            spd.Reset();
            dmg.Reset();
            shrinkMinHp.Reset();
            speedMinHpScale.Reset();
            deathSpawnLevel = 0;
            carrierSpawnLevel = 0;
            regenRate = 0;
        }

        //***************************************************************************************************
        // Set Modifications
        //***************************************************************************************************

        //-----------------------------------------------------------------------
        // Basic Stats
        //-----------------------------------------------------------------------

        /// <summary>
        /// Negative to indicate decrease
        /// </summary>
        public void AddSizeScale(float scale) {
            size.AddScale(scale);
        }

        /// <summary>
        /// Negative to indicate decrease
        /// </summary>
        public void AddHpScale(float scale) {
            hp.AddScale(scale);
        }

        /// <summary>
        /// Negative to indicate decrease
        /// </summary>
        public void AddResourceReward(ResourceType t, float amnt) {
            resourceReward[t] += amnt;
        }
       
        /// <summary>
        /// Negative to indicate decrease
        /// </summary>
        public void AddSpawnRateScale(float scale) {
            spawnRate.AddScale(scale);
        }
       
        /// <summary>
        /// Negative to indicate decrease
        /// </summary>
        public void AddCountScale(float scale) {
            count.AddScale(scale);
        }

        /// <summary>
        /// Negative to indicate decrease
        /// </summary>
        public void AddSpdScale(float scale) {
            spd.AddScale(scale);
        }

        /// <summary>
        /// Negative to indicate decrease
        /// </summary>
        public void AddDamage(float scale) {
            dmg.AddScale(scale);
        }

        /// <summary>
        /// Always Positive
        /// </summary>
        public void AddShrinkMinHp(float scale) {
            shrinkMinHp.AddScale(-scale);
        }

        /// <summary>
        /// Always Positive
        /// </summary>
        public void AddSpeedMinHpScale(float scale) {
            speedMinHpScale.AddScale(-scale);
        }

        //***************************************************************************************************
        // Helpers
        //***************************************************************************************************

        class StatEntry {
            float numer = 1;
            float denom = 1;

            public void Reset() {
                numer = 1;
                denom = 1;
            }
            public void AddScale(float val) {
                if (val > 0) {
                    numer += val;
                }
                else {
                    denom -= val;
                }
            }

            public float GetRatio() {
                return numer / denom;
            }
        }
    }

}
