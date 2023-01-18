namespace Core {
    public class PlayRound_ScenarioState : IFSM_State<ScenarioInstance> {
        //******************************************************************************
        // Singleton
        //******************************************************************************

        PlayRound_ScenarioState() { }
        static PlayRound_ScenarioState instance = new PlayRound_ScenarioState();

        public static PlayRound_ScenarioState Get() {
            instance.bufferFrames = 40;
            return instance;
        }

        //******************************************************************************
        // State
        //******************************************************************************

        int bufferFrames = 0;

        //******************************************************************************
        // IFSM_State
        //******************************************************************************

        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            s.GamplayUpdate();

            if (s.creepFunctions.CreepCount() <= 0) {
                bufferFrames--;
                if (bufferFrames <= 0) {
                    s.towerFunctions.EndRound();
                    return EndRound_ScenarioState.Get();
                }
            }
            return null;
        }
    }
}
