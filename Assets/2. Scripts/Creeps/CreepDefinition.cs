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
        public float moneyReward;

        public float count;
        public float spawnRate;
        public float spacing => 1 / spawnRate;

        //----------------------------------
        // Special
        //----------------------------------

        public float hpRegenRate;
        public float shrinkMinHp = 1;
        public float speedMinHpScale = 1;

        public CreepDefinition deathSplitDefinition;

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
            moneyReward = d.moneyReward;
            count = d.count;
            spawnRate = d.spawnRate;
            glowColor = d.glowColor;
            hpRegenRate = d.hpRegenRate;
            shrinkMinHp = d.shrinkMinHp;
            speedMinHpScale = d.speedMinHpScale;
        }


    }
}
