namespace Core {
    public class PreRoundIdle_CreepMenu_ScenarioState : IFSM_State<ScenarioInstance> {
        //******************************************************************************
        // Singleton
        //******************************************************************************
        PreRoundIdle_CreepMenu_ScenarioState() { }
        static PreRoundIdle_CreepMenu_ScenarioState instance = new PreRoundIdle_CreepMenu_ScenarioState();

        public static PreRoundIdle_CreepMenu_ScenarioState Get(ScenarioInstance s) {
            s.references.ui.preRoundBehaviour.OpenCreepMenu(s);
            return instance;
        }

        //******************************************************************************
        // Implementation
        //******************************************************************************

        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            IFSM_State<ScenarioInstance> nextState = null;
            if (InputManager.PreRoundUI.creepMenuOpen || InputManager.Cancel.requested) {
                s.references.ui.preRoundBehaviour.CloseAllMenus();
                nextState = PreRoundIdle_ScenarioState.Get(s);
            }

            if (InputManager.PreRoundUI.shopMenuOpen) {
                nextState= PreRoundIdle_ShopMenu_ScenarioState.Get(s);
            }

            if (nextState != null) {
                s.references.ui.preRoundBehaviour.creepMenu.Close();
            }

            return nextState;
        }
    }
}
