namespace Core {
    public class EndRound_ScenarioState : IFSM_State {
        //******************************************************************************
        // Singleton
        //******************************************************************************

        EndRound_ScenarioState() { }
        static EndRound_ScenarioState instance = new EndRound_ScenarioState();

        public static EndRound_ScenarioState Get() {
            instance.mode = 0;
            return instance;
        }

        int mode = 0;

        //******************************************************************************
        // IFSM_State
        //******************************************************************************

        public IFSM_State Update(ScenarioInstance s) {
            if (mode == 0) {
                s.playerFunctions.GetShop().Refresh(s);

                s.towerController.money += 100 +150 * s.roundManager.current;
                s.towerFunctions.UpgradeTowers();
       
                // unlock new item
                for (int i = 0; i < 2; i++) {
                    var newItem = PlayerItemUtility.GetRandomItem(s.roundManager.current);
                    s.playerFunctions.AddItem(newItem);
                    s.parameters.ui.endRoundUnlockBehaviour.AddEntry(newItem.GetIcon(), newItem.GetName());
                }

                // money reward
                var m = s.roundManager.GetCurrentRoundMoneyReward();
                s.playerFunctions.AddMoney(m);
                s.parameters.ui.endRoundUnlockBehaviour.AddEntry(IconResourceCache.moneyReward, m.ToString());

                // next state
                s.parameters.ui.endRoundUnlockBehaviour.StartUnlockAnimation();
                mode++;
                return null;
            }
            else if (mode == 1) {
                // wait for animation
                if (!s.parameters.ui.endRoundUnlockBehaviour.AnimationDone()) {
                    return null;
                }

                mode++;
            }
            else if (mode == 2) {
                // get close input
                if (InputManager.Continue.requested) {
                    s.parameters.ui.endRoundUnlockBehaviour.StartCloseAnimation();
                    mode++;
                }
            }
            else if (mode == 3) {
                // wait for animation
                if (!s.parameters.ui.endRoundUnlockBehaviour.AnimationDone()) {
                    return null;
                }

                mode++;
            }
            else if (mode == 4) {
                // more towers
                for (int i = 0; i < s.mapQuery.width * s.mapQuery.height / 40f; i++) {
                    s.towerFunctions.AddTowerRandomPlacement(TowerDefinitionCatalog.wall1);

                }

                s.roundManager.NextRound();

                // go to pre round
                return PreRoundIdle_ScenarioState.Get(s);
            }


            return null;
        }
    }
}
