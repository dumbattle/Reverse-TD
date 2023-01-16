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
            s.playerFunctions.AddMoney(500);
            // initial towers
            var center = new Vector2Int((s.mapQuery.width - 1) / 2, (s.mapQuery.height - 1) / 2);

            s.towerFunctions.AddMainTower(TowerDefinitionCatalog.main_Basic, TowerDefinitionCatalog.gun_1, center);
            s.towerFunctions.AddStartingGroups(TowerDefinitionCatalog.wall1);
            BuildMap(s);

            s.playerFunctions.GetCreepArmy().Init(s.playerFunctions.GetGlobalCreeepUpgrades());

            // TODO refactor out
            s.parameters.ui.endRoundUnlockBehaviour.continueButton.SetClickListener(InputManager.Set.Continue);
            s.parameters.ui.startButton.SetDownListener(InputManager.Set.ButtonDown);
            s.parameters.ui.startButton.SetClickListener(InputManager.Set.Start);
            s.parameters.ui.preRoundBehaviour.creepButton.SetClickListener(InputManager.Set.PreRoundUI.CreepMenuOpen);
            s.parameters.ui.preRoundBehaviour.shopButton.SetClickListener(InputManager.Set.PreRoundUI.ShopMenuOpen);


            s.playerFunctions.GetShop().Refresh(s);
            return PreRoundIdle_ScenarioState.Get(s);
        }

        //******************************************************************************
        // IFSM_State
        //******************************************************************************

        private static void BuildMap(ScenarioInstance s) {
            for (int x = 0; x < s.mapQuery.width; x++) {
                for (int y = 0; y < s.mapQuery.height; y++) {
                    var t = GameObject.Instantiate(s.parameters.tileSrc);
                    t.gameObject.SetActive(true);
                    t.transform.position = s.mapQuery.TileToWorld(x, y);
                    var tile = s.mapQuery.GetTile(x, y);
                    t.SetTile(tile);
                    tile.behaviour = t;
                }
            }

        }
    }
}
