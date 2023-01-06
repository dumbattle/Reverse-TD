using LPE;
using UnityEngine;


namespace Core {
    public class TowerPrefabCache {
        static LazyLoadResource<TowerBehaviour> _mainBasic = new LazyLoadResource<TowerBehaviour>("Prefabs/Main Tower");
        static LazyLoadResource<TowerBehaviour> _cannon1 = new LazyLoadResource<TowerBehaviour>("Prefabs/Cannon Tower 1");


        public static TowerBehaviour MainBasic(ScenarioInstance s, Vector2Int bl) => Get(_mainBasic, s, bl);
        public static TowerBehaviour Cannon1(ScenarioInstance s, Vector2Int bl) => Get(_cannon1, s, bl);

        // TODO - refactor out
        static TowerBehaviour Get(TowerBehaviour src, ScenarioInstance s, Vector2Int bl) {
            var result = GameObject.Instantiate(src);
            result.Init(s, bl);
            return result;
        }

    }
}
