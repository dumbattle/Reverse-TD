using UnityEngine;


namespace Core {
    public class Init_ScenarioState : IFSM_State<ScenarioInstance> {
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

        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            s.references.ui.fadeOverlay.color = new Color(0, 0, 0, 1);
            s.playerFunctions.AddMoney(500);
            // initial towers
            s.parameters.towerController.Init(s);

            BuildMap(s);

            s.playerFunctions.GetCreepArmy().Init(s.playerFunctions.GetGlobalCreeepUpgrades(), s.parameters.creepModifiers);

            // TODO refactor out
            s.references.ui.endRoundUnlockBehaviour.continueButton.SetClickListener(InputManager.Set.Continue);
            s.references.ui.startButton.SetDownListener(InputManager.Set.ButtonDown);
            s.references.ui.startButton.SetClickListener(InputManager.Set.Start);
            s.references.ui.preRoundBehaviour.creepButton.SetClickListener(InputManager.Set.PreRoundUI.CreepMenuOpen);
            s.references.ui.preRoundBehaviour.shopButton.SetClickListener(InputManager.Set.PreRoundUI.ShopMenuOpen);


            s.playerFunctions.GetShop().Refresh(s);

            s.parameters.creepPathfinder.DrawBehaviours(s);
            s.references.roundText.text = "Round 1";
            return FadeIn_ScenarioState.Get(24);
        }

        //******************************************************************************
        // IFSM_State
        //******************************************************************************

        private static void BuildMap(ScenarioInstance s) {
            for (int x = 0; x < s.mapQuery.width; x++) {
                for (int y = 0; y < s.mapQuery.height; y++) {
                    var t = GameObject.Instantiate(s.references.tileSrc);
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
