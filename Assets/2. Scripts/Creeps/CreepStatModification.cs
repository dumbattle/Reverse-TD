namespace Core {
    public class CreepStatModification {
        public float regenRate;

        Entry size = new Entry();
        Entry hp = new Entry();
        Entry money = new Entry();
        Entry spawnRate = new Entry();
        Entry count = new Entry();
        Entry spd = new Entry();
        Entry shrinkMinHp = new Entry();
        Entry speedMinHpScale = new Entry();
        
        
        public CreepStatModification() {
            Reset();
        }

        public void Apply(CreepDefinition def) {
            def.radius *= size.GetRatio();
            def.hp *= hp.GetRatio();
            def.moneyReward *= money.GetRatio();
            def.spawnRate *= spawnRate.GetRatio();
            def.count *= count.GetRatio();
            def.speed *= spd.GetRatio();
            def.radius = UnityEngine.Mathf.Min(def.radius, 0.45f);
            def.hpRegenRate += regenRate;
            def.shrinkMinHp *= shrinkMinHp.GetRatio();
            def.speedMinHpScale *= speedMinHpScale.GetRatio();
        }

        public void Reset() {
            size.Reset();
            hp.Reset();
            money.Reset();
            spawnRate.Reset();
            count.Reset();
            spd.Reset();
            shrinkMinHp.Reset();
            speedMinHpScale.Reset();
        }

        //***************************************************************************************************
        // Set Modifications
        //***************************************************************************************************

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
        public void AddMoneyScale(float scale) {
            money.AddScale(scale);
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

        class Entry {
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
