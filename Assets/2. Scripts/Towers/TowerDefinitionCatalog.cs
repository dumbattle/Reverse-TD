using LPE;
using UnityEngine;


namespace Core {
    public static class TowerDefinitionCatalog {
        public static TowerDefinition main_Basic = new TowerDefinition("Prefabs/Main Tower");
        public static TowerDefinition cannon_1 = new TowerDefinition("Prefabs/Cannon Tower 1");
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
