using UnityEngine;


namespace Core {
    public static class CreepSelectionUtility {
        static BaseCreepDefinition[] _allCreeps = {
            new Circle_Green(),
            new Circle_Blue(),
            new Circle_Yellow(),
            new Circle_Red(),
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

                result.count *= 2;
                result.spacing /= 2;

                result.sprite = CreepResourceCache.circleSpriteBlue;
                result.moneyReward = 100 / result.count;
                return result;
            }
        }
        class Circle_Yellow : Circle_Green {
            public override CreepDefinition GetDefinition() {
                var result = base.GetDefinition();
                result.name = "Fastly Swarmy";

                result.radius *= .9f;
                result.speed *= 1.75f;
                result.hp /= 2;

                result.sprite = CreepResourceCache.circleSpriteYellow;
                result.moneyReward = 100 / result.count;
                return result;
            }
        }
        class Circle_Red : Circle_Green {
            public override CreepDefinition GetDefinition() {
                var result = base.GetDefinition();
                result.name = "Fatly Swarmy";

                result.radius *= 1.5f;
                result.radius = Mathf.Min(result.radius, 0.45f);

                result.hp *= 2;
                
                result.speed /= 1.5f;

                result.count /= 1.5f;
                result.spacing = 5f / result.count;
                result.sprite = CreepResourceCache.circleSpriteRed;
                result.moneyReward = 100 / result.count;
                return result;
            }
        }
    }
}
