namespace Core {
    public class PreRoundIdle_ShopMenu_ScenarioState : IFSM_State {
        //******************************************************************************
        // Singleton
        //******************************************************************************

        PreRoundIdle_ShopMenu_ScenarioState() { }
        static PreRoundIdle_ShopMenu_ScenarioState instance = new PreRoundIdle_ShopMenu_ScenarioState();

        public static PreRoundIdle_ShopMenu_ScenarioState Get(ScenarioInstance s) {
            s.parameters.ui.preRoundBehaviour.OpenShopMenu(s);
            return instance;
        }

        //******************************************************************************
        // Implementation
        //******************************************************************************

        public IFSM_State Update(ScenarioInstance s) {
            IFSM_State nextState = null;
            if (InputManager.PreRoundUI.shopMenuOpen || InputManager.Cancel.requested) {
                s.parameters.ui.preRoundBehaviour.CloseAllMenus();
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
