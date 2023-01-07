namespace Core {
    public class PlayRound_ScenarioState : IFSM_State {
        //******************************************************************************
        // Singleton
        //******************************************************************************

        PlayRound_ScenarioState() { }
        static PlayRound_ScenarioState instance = new PlayRound_ScenarioState();

        public static PlayRound_ScenarioState Get() {
            return instance;
        }

        //******************************************************************************
        // IFSM_State
        //******************************************************************************

        public IFSM_State Update(ScenarioInstance s) {
            s.GamplayUpdate();

            if (s.creepFunctions.CreepCount() <= 0) {
                return EndRound_ScenarioState.Get();
            }
            return null;
        }
    }
}
