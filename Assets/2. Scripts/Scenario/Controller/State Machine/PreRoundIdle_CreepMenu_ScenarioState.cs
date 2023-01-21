namespace Core {
    public class PreRoundIdle_CreepMenu_ScenarioState : IFSM_State<ScenarioInstance> {

        //******************************************************************************
        // Singleton
        //******************************************************************************

        PreRoundIdle_CreepMenu_ScenarioState() { }
        static PreRoundIdle_CreepMenu_ScenarioState instance = new PreRoundIdle_CreepMenu_ScenarioState();

        public static PreRoundIdle_CreepMenu_ScenarioState Get(ScenarioInstance s) {
            s.references.ui.preRoundBehaviour.OpenCreepMenu(s);
            instance.subState = SubState_Idle.Get(0);
            return instance;
        }

        //******************************************************************************
        // State

        //******************************************************************************
        IFSM_State<ScenarioInstance> subState;

        //******************************************************************************
        // Implementation
        //******************************************************************************

        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            subState = subState.Update(s) ?? subState;


            IFSM_State<ScenarioInstance> nextState = null;

            // close
            if (InputManager.PreRoundUI.creepMenuOpen || InputManager.Cancel.requested) {
                s.references.ui.preRoundBehaviour.CloseAllMenus();
                nextState = PreRoundIdle_ScenarioState.Get(s);
            }
            // switch to shop
            if (InputManager.PreRoundUI.shopMenuOpen) {
                nextState = PreRoundIdle_ShopMenu_ScenarioState.Get(s);
            }

            if (nextState != null) {
                s.references.ui.preRoundBehaviour.creepMenu.Close();
            }

            return nextState;
        }

        public class SubState_Idle : IFSM_State<ScenarioInstance> {
            //******************************************************************************
            // Singleton
            //******************************************************************************
            SubState_Idle() { }
            static SubState_Idle instance = new SubState_Idle();

            public static SubState_Idle Get(int currentSelectCreep) {
                instance.currentSelectCreep = currentSelectCreep;
                return instance;
            }

            //******************************************************************************
            // State
            //******************************************************************************

            int currentSelectCreep;

            //******************************************************************************
            // Implementation
            //******************************************************************************

            public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
                PreRound_CreepMenu_Behaviour creepMenu = s.references.ui.preRoundBehaviour.creepMenu;

                // select creep
                var creepSelected = creepMenu.creepSelected;
                if (creepSelected >= 0) {
                    currentSelectCreep = creepSelected;
                    creepMenu.SetCreepDetails(creepSelected);
                    creepMenu.DehighlightAllAttachmentSlots();
                }

                // select attachment slot
                var atchSelected = creepMenu.attachmentSelected;
                if (atchSelected >= 0) {
                    var atch = s.playerFunctions.GetCreepArmy().GetSquad(currentSelectCreep).GetAttachment(atchSelected);
                    if (atch == null) {
                        creepMenu.DehighlightAllAttachmentSlots();
                        return SubState_SelectAtchForCreep.Get(s, currentSelectCreep);
                    }
                    else {
                        creepMenu.HighlightAttachmentSlot(atchSelected);
                    }
                }


                return null;
            }
        }

        public class SubState_SelectAtchForCreep : IFSM_State<ScenarioInstance> {

            //******************************************************************************
            // Singleton
            //******************************************************************************

            SubState_SelectAtchForCreep() { }
            static SubState_SelectAtchForCreep instance = new SubState_SelectAtchForCreep();

            public static SubState_SelectAtchForCreep Get(ScenarioInstance s, int currentSelectCreep) {
                int numAttachInventory = s.playerFunctions.NumAttachableInInventory(s.playerFunctions.GetCreepArmy().GetSquad(currentSelectCreep));
                if (numAttachInventory <=0) {
                    // invalid - don't enter
                    return null;
                }

                s.references.ui.preRoundBehaviour.creepMenu.OpenItemSelect();
                instance.currentSelectCreep = currentSelectCreep;
                return instance;
            }

            //******************************************************************************
            // State
            //******************************************************************************

            int currentSelectCreep;

            //******************************************************************************
            // Implementation
            //******************************************************************************

            public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
                PreRound_CreepMenu_Behaviour creepMenu = s.references.ui.preRoundBehaviour.creepMenu;

                if (creepMenu.itemSelected != null) {
                    s.playerFunctions.RemoveItem(creepMenu.itemSelected);
                    s.playerFunctions.GetCreepArmy().GetSquad(currentSelectCreep).AddModifier(creepMenu.itemSelected);
                    creepMenu.SetCreepDetails(currentSelectCreep);
                    creepMenu.OpenItemSelect();
                    int numAttachInventory = s.playerFunctions.NumAttachableInInventory(s.playerFunctions.GetCreepArmy().GetSquad(currentSelectCreep));

                    if (numAttachInventory <= 0) {
                        creepMenu.CloseItemSelect();
                        return SubState_Idle.Get(currentSelectCreep);
                    }
                }
                return null;
            }
        }
    }
}