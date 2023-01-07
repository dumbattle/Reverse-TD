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
                // unlock new items
                var newCreep = CreepSelectionUtility.GetRandomNewCreep();
                s.playerFunctions.GetCreepArmy().AddNewSquad(newCreep);

                var m = s.roundManager.GetCurrentRoundMoneyReward();
                s.playerFunctions.AddMoney(m);
                s.parameters.ui.endRoundUnlockBehaviour.AddEntry(newCreep.sprite, newCreep.name);
                s.parameters.ui.endRoundUnlockBehaviour.AddEntry(IconResourceCache.moneyReward, m.ToString());
                mode++;
                s.parameters.ui.endRoundUnlockBehaviour.StartUnlockAnimation();
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
                s.roundManager.NextRound();
                // go to pre round
                return PreRoundIdle_ScenarioState.Get(s);
            }


            return null;
        }
    }
}
