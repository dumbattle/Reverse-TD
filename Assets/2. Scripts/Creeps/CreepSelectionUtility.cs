using UnityEngine;


namespace Core {
    public static class CreepSelectionUtility {
        static BaseCreepDefinition[] _allCreeps = {
            new Circle_Green(),
            new Circle_Red(),
            new Circle_Yellow(),
            new Circle_Blue(),

            new Square_Green(),
            new Square_Yellow(),    
            new Square_Red(),
            new Square_Blue(),

        };
        public static CreepDefinition GetRandomNewCreep() {
            var def = _allCreeps[Random.Range(0, _allCreeps.Length)];
            return def.GetDefinition();
        }
        public static CreepDefinition GetInitialCreep() {
            return _allCreeps[0].GetDefinition();
        }
        //**************************************************************************************
        // Helpers
        //**************************************************************************************

        // Color Codes:
        //  - Green : well balanced 
        //  - Blue : Swarm
        //  - Red : Tanky
        //  - Yellow : Fast


        abstract class BaseCreepDefinition {
            public abstract CreepDefinition GetDefinition();
        }

        class Circle_Green : BaseCreepDefinition {
            public override CreepDefinition GetDefinition() {
                var result = new CreepDefinition();
                result.name = "Swarmly";
                result.radius = Random.Range(0.275f, 0.325f);
                result.speed = Random.Range(1.8f, 2.2f);
                result.hp = Random.Range(85, 115);

                result.count = Random.Range(15, 20);
                result.spacing = 5f / result.count;
                result.sprite = CreepResourceCache.circleSpriteGreen;
                result.moneyReward = 100 / result.count;
                result.glowColor = new Color(.65f, .94f, .14f);
                return result;
            }
        }
        class Circle_Blue : Circle_Green {
            public override CreepDefinition GetDefinition() {
                var result = base.GetDefinition();
                result.name = "Verly Swarmy";

                result.radius /= 1.15f;
                result.speed *= 1.15f;
                result.hp /= 2;

                result.count *= 1.8f;
                result.spacing /= 1.8f;

                result.sprite = CreepResourceCache.circleSpriteBlue;
                result.moneyReward = 100 / result.count;
                result.glowColor = new Color(.23f, .55f, .93f);
                return result; 
            }
        }
        class Circle_Yellow : Circle_Green {
            public override CreepDefinition GetDefinition() {
                var result = base.GetDefinition();
                result.name = "Fastly Swarmy";

                result.radius *= .9f;
                result.speed *= 1.2f;
                result.hp = result.hp / 1.5f;
                result.count /= 1.2f;
                result.spacing *= 1.8f;
                result.sprite = CreepResourceCache.circleSpriteYellow;
                result.moneyReward = 100 / result.count;
                result.glowColor = new Color(.96f, .64f, .16f);
                return result;
            }
        }
        class Circle_Red : Circle_Green {
            public override CreepDefinition GetDefinition() {
                var result = base.GetDefinition();
                result.name = "Fatly Swarmy";

                result.radius *= 1.25f;
                result.radius = Mathf.Min(result.radius, 0.45f);

                result.hp = result.hp * 1.25f;

                result.speed /= 1.25f;

                result.count /= 1.35f;
                result.spacing *= 1.35f;

                result.sprite = CreepResourceCache.circleSpriteRed;
                result.moneyReward = 100 / result.count;
                result.glowColor = Color.red;
                return result;
            }
        }


        class Square_Green : BaseCreepDefinition {
            public override CreepDefinition GetDefinition() {
                var result = new CreepDefinition();
                result.name = "Tankit";
                result.radius = Random.Range(0.325f, 0.375f);
                result.speed = Random.Range(.9f, 1.1f);
                result.hp = Random.Range(190, 230);

                result.count = Random.Range(7, 10);
                result.spacing = 7f / result.count;
                result.sprite = CreepResourceCache.squareSpriteGreen;
                result.moneyReward = 100 / result.count;
                result.glowColor = Color.green;
                return result;
            }
        }
        class Square_Blue : Square_Green {
            public override CreepDefinition GetDefinition() {
                var result = base.GetDefinition();
                result.name = "Diet Tankit";

                result.radius /= 1.35f;
                result.speed /= 1.45f;
                result.hp /= 1.7f;

                result.count *= 1.55f;
                result.spacing /= 1.55f;

                result.sprite = CreepResourceCache.squareSpriteBlue;
                result.moneyReward = 100 / result.count;
                result.glowColor = Color.blue;
                return result;
            }
        }
        class Square_Yellow : Square_Green {
            public override CreepDefinition GetDefinition() {
                var result = base.GetDefinition();
                result.name = "Quickit Tankit";

                result.radius *= 1.05f;
               
                result.speed *= 1.25f;
                result.hp /= 1.35f;
               
                result.count /= 1.35f;
                result.spacing /= 1.25f;
               
                result.sprite = CreepResourceCache.squareSpriteYellow;
                result.moneyReward = 100 / result.count;
                result.glowColor = new Color(.84f, 1, 0);
                return result;
            }
        }
        class Square_Red : Square_Green {
            public override CreepDefinition GetDefinition() {
                var result = base.GetDefinition();
                result.name = "Biggit Tankit";

                result.radius *= 1.15f;
                
                result.hp = result.hp * 1.25f;
                result.speed /= 1.45f;

                result.count /= 1.35f;
                result.spacing *= 1.35f;

                result.sprite = CreepResourceCache.squareSpriteRed;
                result.moneyReward = 100 / result.count;
                result.glowColor = new Color(1, .09f, .60f);
                return result;
            }
        }



    }
}
