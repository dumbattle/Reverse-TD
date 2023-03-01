namespace Core {
    public class PreRoundIdle_CreepMenuState : IFSM_State<ScenarioInstance> {
        //******************************************************************************
        // Singleton
        //******************************************************************************
        PreRoundIdle_CreepMenuState() { }
        static PreRoundIdle_CreepMenuState instance = new PreRoundIdle_CreepMenuState();

        public static PreRoundIdle_CreepMenuState Get(ScenarioInstance s) {
            instance.menuState = s.references.ui.creepMenu.GetGameState(s);
            return instance;
        }


        IFSM_State<ScenarioInstance> menuState;


        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            if (menuState == null) {
                return PreRoundIdle_ScenarioState.Get(s);
            }
           
            menuState = menuState.Update(s);
            return this;
        }
    }
}
