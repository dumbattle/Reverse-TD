namespace Core {
    public class PreRoundIdle_ScenarioState : IFSM_State {
        //******************************************************************************
        // Singleton
        //******************************************************************************
        PreRoundIdle_ScenarioState() { }
        static PreRoundIdle_ScenarioState instance = new PreRoundIdle_ScenarioState();

        public static PreRoundIdle_ScenarioState Get(ScenarioInstance s) {
            s.parameters.ui.BeginPreRound();
            return instance;
        }
        public IFSM_State Update(ScenarioInstance s) {

            s.HandleMoveZoomInput();
            if (InputManager.Start.requested) {
                return SpawnCreeps_ScenarioState.Get(s);
            }
            return null;
        }

    }
}
