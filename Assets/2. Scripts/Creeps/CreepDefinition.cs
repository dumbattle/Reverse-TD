using UnityEngine;


namespace Core {
    public class CreepDefinition {
        public string name;
        public Sprite sprite;
        public Color glowColor = Random.ColorHSV(0, 1, .5f, 1, 1, 1);

        //----------------------------------
        // Common
        //----------------------------------

        public float hp;
        public float speed;
        public float radius;
        public ResourceCollection resourceReward = new ResourceCollection();

        public float count;
        public float spawnRate;
        public float spawnInterval => 1 / spawnRate;

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
