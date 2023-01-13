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

        //******************************************************************************
        // Implementation
        //******************************************************************************

        public IFSM_State Update(ScenarioInstance s) {
            IFSM_State nextState = null;
            if (InputManager.PreRoundUI.creepMenuOpen || InputManager.Cancel.requested) {
                nextState= PreRoundIdle_ScenarioState.Get(s);
            }

            if (InputManager.PreRoundUI.shopMenuOpen) {
                nextState= PreRoundIdle_ShopMenu_ScenarioState.Get(s);
            }

            if (nextState != null) {
                s.parameters.ui.preRoundBehaviour.creepMenu.Close();
            }

            return nextState;
        }
    }

    public class PreRoundIdle_ShopMenu_ScenarioState : IFSM_State {
        //******************************************************************************
        // Singleton
        //******************************************************************************

        PreRoundIdle_ShopMenu_ScenarioState() { }
        static PreRoundIdle_ShopMenu_ScenarioState instance = new PreRoundIdle_ShopMenu_ScenarioState();

        public static PreRoundIdle_ShopMenu_ScenarioState Get(ScenarioInstance s) {
            s.parameters.ui.preRoundBehaviour.shopMenu.Open(s);
            return instance;
        }

        //******************************************************************************
        // Implementation
        //******************************************************************************

        public IFSM_State Update(ScenarioInstance s) {
            IFSM_State nextState = null;
            if (InputManager.PreRoundUI.shopMenuOpen || InputManager.Cancel.requested) {
                nextState = PreRoundIdle_ScenarioState.Get(s);
            }

            if (InputManager.PreRoundUI.creepMenuOpen) {
                nextState = PreRoundIdle_CreepMenu_ScenarioState.Get(s);
            }
            if (nextState != null) {
                s.parameters.ui.preRoundBehaviour.shopMenu.Close();
            }
            return nextState;
        }
    }
}
