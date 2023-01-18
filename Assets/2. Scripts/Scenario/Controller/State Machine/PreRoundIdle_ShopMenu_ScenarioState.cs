namespace Core {
    public class PreRoundIdle_ShopMenu_ScenarioState : IFSM_State<ScenarioInstance> {
        //******************************************************************************
        // Singleton
        //******************************************************************************

        PreRoundIdle_ShopMenu_ScenarioState() { }
        static PreRoundIdle_ShopMenu_ScenarioState instance = new PreRoundIdle_ShopMenu_ScenarioState();

        public static PreRoundIdle_ShopMenu_ScenarioState Get(ScenarioInstance s) {
            s.references.ui.preRoundBehaviour.OpenShopMenu(s);
            return instance;
        }

        //******************************************************************************
        // Implementation
        //******************************************************************************

        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            IFSM_State<ScenarioInstance> nextState = null;
            if (InputManager.PreRoundUI.shopMenuOpen || InputManager.Cancel.requested) {
                s.references.ui.preRoundBehaviour.CloseAllMenus();
                nextState = PreRoundIdle_ScenarioState.Get(s);
            }

            if (InputManager.PreRoundUI.creepMenuOpen) {
                nextState = PreRoundIdle_CreepMenu_ScenarioState.Get(s);
            }
            if (nextState != null) {
                s.references.ui.preRoundBehaviour.shopMenu.Close();
            }
            return nextState;
        }
    }
}
