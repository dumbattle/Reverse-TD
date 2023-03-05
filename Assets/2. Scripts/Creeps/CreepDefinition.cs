using UnityEngine;


namespace Core {
    public abstract class DamageType {
        public static DamageType normal { get; private set; } = new _NormalDamage();
        public static DamageType explosive { get; private set; } = new _ExplosiveDamage();
        public static DamageType energy { get; private set; } = new _EnergyDamage();


        public float ApplyArmor(float rawDamage, CreepArmor armor) {
            int armorRating = GetArmorRating(armor);

            float scale = rawDamage / (rawDamage + armorRating);
            float result = rawDamage * scale;

            if (result < 1) {
                result = 1 - result;
                result = Mathf.Sqrt(result);
                result = 1 - result;
            }
            return result;
        }


        protected abstract int GetArmorRating(CreepArmor armor);

        class _NormalDamage : DamageType {
            protected override int GetArmorRating(CreepArmor armor) {
                return armor.normalRating;
            }
        }
        class _ExplosiveDamage : DamageType {
            protected override int GetArmorRating(CreepArmor armor) {
                return armor.explosiveRating;
            }
        }
        class _EnergyDamage : DamageType {
            protected override int GetArmorRating(CreepArmor armor) {
                return armor.energyRating;
            }
        }
    }


    public class CreepArmor {
        public int normalRating;
        public int explosiveRating;
        public int energyRating;

        /// <summary>
        /// resets all armor ratings to 0
        /// </summary>
        public void ResetValues() {
            normalRating = 0;
            explosiveRating = 0;
            energyRating = 0;
        }

        public void CopyFrom(CreepArmor src) {
            normalRating = src.normalRating;
            explosiveRating = src.explosiveRating;
            energyRating = src.energyRating;
        }
    }

    public class CreepDefinition {
        public string name;
        public Sprite sprite;
        public Color glowColor = Random.ColorHSV(0, 1, .5f, 1, 1, 1);

        //----------------------------------
        // Common
        //----------------------------------

        public float hp;
        public float speed;
        public float count;
        public float spawnRate;
        public float damageScale = 1;
        public float incomeScale = 1;

        public float radius;
        public ResourceCollection resourceReward = new ResourceCollection();

        public float spawnInterval => 1 / spawnRate;
        public CreepArmor armor = new CreepArmor();

        //----------------------------------
        // Special
        //----------------------------------

        public float hpRegenRate;
        public float shrinkMinHp = 1;
        public float speedMinHpScale = 1;

        public CreepDefinition deathSplitDefinition;
        public CreepDefinition carrierDefinition;

        //***********************************************************************
        // Helpers
        //***********************************************************************
        
        public CreepDefinition CreateCopy() {
            var result = new CreepDefinition();
            result.CopyFrom(this);
            return result;
        }

        public void CopyFrom(CreepDefinition d) {
            name = d.name;
            speed = d.speed;
            radius = d.radius;
            sprite = d.sprite;
            hp = d.hp;
            resourceReward[ResourceType.green] = d.resourceReward[ResourceType.green];
            resourceReward[ResourceType.red] = d.resourceReward[ResourceType.red];
            resourceReward[ResourceType.blue] = d.resourceReward[ResourceType.blue];
            resourceReward[ResourceType.yellow] = d.resourceReward[ResourceType.yellow];
            resourceReward[ResourceType.diamond] = d.resourceReward[ResourceType.diamond];
            count = d.count;
            spawnRate = d.spawnRate;
            glowColor = d.glowColor;
            hpRegenRate = d.hpRegenRate;
            shrinkMinHp = d.shrinkMinHp;
            speedMinHpScale = d.speedMinHpScale;
        }


    }
}
