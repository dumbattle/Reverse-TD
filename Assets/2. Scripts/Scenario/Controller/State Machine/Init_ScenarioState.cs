using UnityEngine;


namespace Core {
    public class Init_ScenarioState : IFSM_State {
        //******************************************************************************
        // Singleton
        //******************************************************************************

        Init_ScenarioState() { }
        static Init_ScenarioState instance = new Init_ScenarioState();

        public static Init_ScenarioState Get() {
            return instance;
        }

        //******************************************************************************
        // IFSM_State
        //******************************************************************************

        public IFSM_State Update(ScenarioInstance s) {
            BuildMap(s);
            s.playerFunctions.AddMoney(1000);
            // initial towers
            var center = new Vector2Int((s.mapQuery.width - 1) / 2, (s.mapQuery.height - 1) / 2);
            s.towerFunctions.AddMainTower(TowerPrefabCache.MainBasic(s, center));
            s.towerFunctions.AddTower(TowerPrefabCache.Cannon1(s, center + new Vector2Int(-1, -1)));

            s.towerFunctions.AddTower(TowerPrefabCache.Cannon1(s, center + new Vector2Int(2, 2)));


            s.towerFunctions.AddTower(TowerPrefabCache.Cannon1(s, center + new Vector2Int(-1, 2)));
            s.towerFunctions.AddTower(TowerPrefabCache.Cannon1(s, center + new Vector2Int(2, -1)));
            s.playerFunctions.GetCreepArmy().Init();

            // TODO refactor out
            s.parameters.ui.endRoundUnlockBehaviour.continueButton.SetClickListener(InputManager.Set.Continue);
            s.parameters.ui.startButton.SetDownListener(InputManager.Set.ButtonDown);
            s.parameters.ui.startButton.SetClickListener(InputManager.Set.Start);
            return PreRoundIdle_ScenarioState.Get(s);
        }

        //******************************************************************************
        // IFSM_State
        //******************************************************************************

        private static void BuildMap(ScenarioInstance s) {
            for (int x = 0; x < s.mapQuery.width; x++) {
                for (int y = 0; y < s.mapQuery.height; y++) {
                    var t = GameObject.Instantiate(s.parameters.tileSrc);
                    t.SetActive(true);
                    t.transform.position = s.mapQuery.TileToWorld(x, y);
                }
            }
        }
    }
}
