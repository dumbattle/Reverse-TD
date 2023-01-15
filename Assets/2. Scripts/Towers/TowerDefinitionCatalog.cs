using LPE;
using UnityEngine;


namespace Core {
    public static class TowerDefinitionCatalog {
        public static TowerDefinition main_Basic = new TowerDefinition("Prefabs/Main Tower");
        public static TowerDefinition gun_1 = new TowerDefinition("Prefabs/Gun Tower 1");
        public static TowerDefinition gun_2 = new TowerDefinition("Prefabs/Gun Tower 2");
        public static TowerDefinition bomb_1 = new TowerDefinition("Prefabs/Bomb Tower 1");
        public static TowerDefinition circleAOE_1 = new TowerDefinition("Prefabs/Circle AOE 1");
        public static TowerDefinition circleAOE_slow_1 = new TowerDefinition("Prefabs/Circle AOE Slow 1");
        public static TowerDefinition wall1 = new TowerDefinition("Prefabs/Wall 1");
    }

    public class TowerDefinition {
        LazyLoadResource<TowerBehaviour> src;
        public TowerDefinition(string resourcePath) {
            src = new LazyLoadResource<TowerBehaviour>(resourcePath);
        }

        public TowerBehaviour GetNewInstance(ScenarioInstance s, Vector2Int bl) {
            var result = GameObject.Instantiate(src.obj);
            result.Init(s, bl);
            return result;
        }

        public int size => src.obj.Size;
    }
}
