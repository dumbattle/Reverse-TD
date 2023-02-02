namespace Core {
    public class EndRound_ScenarioState : IFSM_State<ScenarioInstance> {
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

        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            if (mode == 0) {
                s.playerFunctions.GetShop().Refresh(s);

       
                // unlock new item
                for (int i = 0; i < 2; i++) {
                    var newItem = PlayerItemUtility.GetRandomItem(s.roundManager.current);
                    s.playerFunctions.AddItem(newItem);
                    //s.references.ui.endRoundUnlockBehaviour.AddEntry(newItem.GetIcon(), newItem.GetName());
                }

                // money reward
                var m = s.roundManager.GetCurrentRoundMoneyReward();
                s.playerFunctions.AddMoney(m);
                s.references.ui.endRoundUnlockBehaviour.AddEntry(IconResourceCache.moneyReward, m.ToString());

                // next state
                s.references.ui.endRoundUnlockBehaviour.StartUnlockAnimation();
                mode++;
                return null;
            }
            else if (mode == 1) {
                // wait for animation
                if (!s.references.ui.endRoundUnlockBehaviour.AnimationDone()) {
                    return null;
                }

                mode++;
            }
            else if (mode == 2) {
                // get close input
                if (InputManager.Continue.requested) {
                    s.references.ui.endRoundUnlockBehaviour.StartCloseAnimation();
                    mode++;
                }
            }
            else if (mode == 3) {
                // wait for animation
                if (!s.references.ui.endRoundUnlockBehaviour.AnimationDone()) {
                    return null;
                }

                mode++;
            }
            else if (mode == 4) {
                s.parameters.towerController.OnRoundEnd(s);

                s.roundManager.NextRound();
                s.references.roundText.text = $"Round {s.roundManager.current}";

                // go to pre round
                return PreRoundIdle_ScenarioState.Get(s);
            }


            return null;
        }
    }
}
