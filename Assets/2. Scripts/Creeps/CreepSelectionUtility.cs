using UnityEngine;


namespace Core {
    public static class CreepSelectionUtility {

        public static CreepDefinition GetRandomNewCreep() {
            return GetDefinition(false);
        }
        public static CreepDefinition GetInitialCreep() {
            return GetDefinition(false);
        }
        //**************************************************************************************
        // Helpers
        //**************************************************************************************

        static Sprite[] sprites = {
                CreepResourceCache.circleSpriteBlue,
                CreepResourceCache.circleSpriteGreen,
                CreepResourceCache.circleSpriteRed,
                CreepResourceCache.circleSpriteYellow,

                CreepResourceCache.squareSpriteGreen,
                CreepResourceCache.squareSpriteBlue,
                CreepResourceCache.squareSpriteRed,
                CreepResourceCache.squareSpriteYellow,

                CreepResourceCache.triangleSpriteGreen,
                CreepResourceCache.triangleSpriteBlue,
                CreepResourceCache.triangleSpriteRed,
                CreepResourceCache.triangleSpriteYellow,
            };
        static CreepDefinition GetDefinition(bool randomize) {
            var result = new CreepDefinition();

            result.hp = 100;
            result.radius = .1f + .15f;
            result.radius = Mathf.Min(result.radius, 0.45f);
            result.speed = 2;
            result.damageScale = 1;

            result.count = 20;
            result.spawnRate = 5f;
            result.sprite = sprites[Random.Range(0, sprites.Length)];
            result.glowColor = Random.ColorHSV(0, 1, .5f, 1, 1, 1);
            return result;
        }
      
    }
}
