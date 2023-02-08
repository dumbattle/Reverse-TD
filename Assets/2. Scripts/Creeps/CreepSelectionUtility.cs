using UnityEngine;


namespace Core {
    public static class CreepSelectionUtility {
        static BaseCreepDefinition[] _allCreeps = {
            
            new Circle_Green(),
            new Circle_Blue(),
            new Circle_Red(),
            new Circle_Yellow(),

            new Square_Yellow(),    
            new Square_Blue(),
            new Square_Green(),
            new Square_Red(),


        };
       static  RandomCreepDefinition randDef = new RandomCreepDefinition();

        public static CreepDefinition GetRandomNewCreep() {
            return randDef.GetDefinition();
            //var def = _allCreeps[Random.Range(0, _allCreeps.Length)];
            //return def.GetDefinition();
        }
        public static CreepDefinition GetInitialCreep() {
            return randDef.GetDefinition();
            //return _allCreeps[0].GetDefinition();
        }
        //**************************************************************************************
        // Helpers
        //**************************************************************************************

        abstract class BaseCreepDefinition {
            public abstract CreepDefinition GetDefinition();
        }

        class RandomCreepDefinition : BaseCreepDefinition {
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
            public CreepDefinition GetDefinition(bool randomize) {
                var result = new CreepDefinition();
                RandomWeights weights = GetWeights();

                if (!randomize) {
                    weights.hp = 1;
                    weights.speed = 1;
                    weights.count = 1;
                    weights.spawnRate = 1;
                }

                result.name = GetName();
                result.hp = weights.hp * 100;
                result.radius = .1f + .15f * weights.hp / weights.count;
                result.radius = Mathf.Min(result.radius, 0.45f);
                result.speed = weights.speed * 2;

                result.count = 20 * weights.count;
                result.spawnRate = weights.spawnRate * result.count / 5f;
                result.sprite = GetSprite();
                result.glowColor = Random.ColorHSV(0, 1, .5f, 1, 1, 1);
                return result;
            }
            public override CreepDefinition GetDefinition() {
                return GetDefinition(false);
            }
            public virtual string GetName() {
                return "Happy";
            }
            public virtual RandomWeights GetWeights() {
                var result = new RandomWeights();
                result.Randomize();
                return result;
            }
            public virtual Sprite GetSprite() {
                return sprites[Random.Range(0, sprites.Length)];
            }
        }
        class Circle_Green : RandomCreepDefinition {
            public override RandomWeights GetWeights() {
                var result = new RandomWeights();
                result.hp = 1;
                result.speed = 1;
                result.count = 1;
                result.spawnRate = 1;
                return result;
            }
            public override Sprite GetSprite() {
                return CreepResourceCache.circleSpriteGreen;
            }
            public override string GetName() {
                return "Swarmly";
            }
        }
        class Circle_Blue : RandomCreepDefinition {
            public override RandomWeights GetWeights() {
                var result = new RandomWeights();
                result.hp = 0.6f;
                result.speed = 1f;
                result.count = 1.5f;
                result.spawnRate = 1.15f;
                return result;
            }
            public override Sprite GetSprite() {
                return CreepResourceCache.circleSpriteBlue;
            }
            public override string GetName() {
                return "Verly Swarmly";
            }
        }
        class Circle_Yellow : RandomCreepDefinition {
            public override RandomWeights GetWeights() {
                var result = new RandomWeights();
                result.hp = .7f;
                result.speed = 1.5f;
                result.count = .8f;
                result.spawnRate = 1;
                return result;
            }
            public override Sprite GetSprite() {
                return CreepResourceCache.circleSpriteYellow;
            }
            public override string GetName() {
                return "Speedly Swarmly";
            }
        }
        class Circle_Red : RandomCreepDefinition {
            public override RandomWeights GetWeights() {
                var result = new RandomWeights();
                result.hp = 1.3f;
                result.speed = .75f;
                result.count = .7f;
                result.spawnRate = 1.3f;
                return result;
            }
            public override Sprite GetSprite() {
                return CreepResourceCache.circleSpriteRed;
            }
            public override string GetName() {
                return "Fatly Swarmly";
            }
        }


        class Square_Green : RandomCreepDefinition {
            public override RandomWeights GetWeights() {
                var result = new RandomWeights();
                result.hp = 2;
                result.speed = .65f;
                result.count = .6f;
                result.spawnRate = .8f;
                return result;
            }
            public override Sprite GetSprite() {
                return CreepResourceCache.squareSpriteGreen;
            }
            public override string GetName() {
                return "Tankit";
            }
        }
        class Square_Blue : RandomCreepDefinition {

            public override RandomWeights GetWeights() {
                var result = new RandomWeights();
                result.hp = 1.3f;
                result.speed = .6f;
                result.count = .9f;
                result.spawnRate = 1.2f;
                return result;  
            }
            public override Sprite GetSprite() {
                return CreepResourceCache.squareSpriteBlue;
            }
            public override string GetName() {
                return "Diet Tankit";
            }
        }
        class Square_Yellow : RandomCreepDefinition {

            public override RandomWeights GetWeights() {
                var result = new RandomWeights();
                result.hp = 1.7f;
                result.speed = .9f;
                result.count = .6f;
                result.spawnRate = .7f;
                return result;
            }
            public override Sprite GetSprite() {
                return CreepResourceCache.squareSpriteYellow;
            }
            public override string GetName() {
                return "Quickit Tankit";
            }
        }
        class Square_Red : RandomCreepDefinition {
            public override RandomWeights GetWeights() {
                var result = new RandomWeights();
                result.hp = 3;
                result.speed = .5f;
                result.count = .5f;
                result.spawnRate = .6f;
                return result;
            }
            public override Sprite GetSprite() {
                return CreepResourceCache.squareSpriteRed;
            }
            public override string GetName() {
                return "Biggit Tankit";
            }
        }

        struct RandomWeights {
            public float hp;
            public float speed;
            public float count;
            public float spawnRate;

            public void Randomize() {
                hp = 100;
                speed = 100;
                count = 100;
                spawnRate = 100;

                int total = 400;

                for (int i = 0; i < 10; i++) {
                    var ind = Random.Range(0, 4);
                    SetStat(ind, GetStat(ind) + i * i);
                    total += i * i;
                }
                hp = hp / total * 4;
                speed = speed / total * 4;
                count = count / total * 4;
                spawnRate = spawnRate / total * 4;
            }

            public float GetStat(int index) {
                switch (index) {
                    case 0:
                        return hp;
                    case 1:
                        return speed;
                    case 2:
                        return count;
                    case 3:
                        return spawnRate;
                }
                return -1;
            }

            public void SetStat(int index, float val) {

                switch (index) {
                    case 0:
                        hp = val;
                        break;
                    case 1:
                        speed = val;
                        break;
                    case 2:
                        count = val;
                        break;
                    case 3:
                        spawnRate = val;
                        break;
                }
            }
        }
    }
}
