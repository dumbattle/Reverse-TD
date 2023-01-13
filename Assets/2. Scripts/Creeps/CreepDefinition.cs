using UnityEngine;


namespace Core {
    public class CreepDefinition {
        public string name;
        public Sprite sprite;

        public float hp;
        public float speed;
        public float radius;
        public float moneyReward;

        // group data
        public float count;
        public float spacing;

        public CreepDefinition Copy() {
            var result = new CreepDefinition();
            result.name = name;
            result.speed = speed;
            result.radius = radius;
            result.sprite = sprite;
            result.hp = hp;
            result.moneyReward = moneyReward;
            result.count = count;
            result.spacing = spacing;

            return result;
        }
    }
}
