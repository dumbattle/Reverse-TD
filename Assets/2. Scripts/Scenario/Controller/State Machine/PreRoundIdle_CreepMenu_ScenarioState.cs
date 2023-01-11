namespace Core {
    public class PreRoundIdle_CreepMenu_ScenarioState : IFSM_State {
        //******************************************************************************
        // Singleton
        //******************************************************************************
        PreRoundIdle_CreepMenu_ScenarioState() { }
        static PreRoundIdle_CreepMenu_ScenarioState instance = new PreRoundIdle_CreepMenu_ScenarioState();

        public static PreRoundIdle_CreepMenu_ScenarioState Get(ScenarioInstance s) {
            s.parameters.ui.preRoundBehaviour.creepMenu.Open(s);
            return instance;
        }


        public IFSM_State Update(ScenarioInstance s) {
            if (InputManager.PreRoundUI.creepMenuOpen || InputManager.Cancel.requested) {
                s.parameters.ui.preRoundBehaviour.creepMenu.Close();
                return PreRoundIdle_ScenarioState.Get(s);
            }

            return null;
        }
    }
}
