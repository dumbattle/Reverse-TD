﻿namespace Core {
    public class PreRoundIdle_CreepMenu_ScenarioState : IFSM_State {
        //******************************************************************************
        // Singleton
        //******************************************************************************
        PreRoundIdle_CreepMenu_ScenarioState() { }
        static PreRoundIdle_CreepMenu_ScenarioState instance = new PreRoundIdle_CreepMenu_ScenarioState();

        public static PreRoundIdle_CreepMenu_ScenarioState Get(ScenarioInstance s) {
            s.parameters.ui.preRoundBehaviour.OpenCreepMenu(s);
            return instance;
        }

        //******************************************************************************
        // Implementation
        //******************************************************************************

        public IFSM_State Update(ScenarioInstance s) {
            IFSM_State nextState = null;
            if (InputManager.PreRoundUI.creepMenuOpen || InputManager.Cancel.requested) {
                s.parameters.ui.preRoundBehaviour.CloseAllMenus();
                nextState = PreRoundIdle_ScenarioState.Get(s);
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
}
