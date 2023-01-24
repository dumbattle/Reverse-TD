namespace Core {
    public class PreRoundIdle_CreepMenu_ScenarioState : IFSM_State<ScenarioInstance> {

        //******************************************************************************
        // Singleton
        //******************************************************************************

        PreRoundIdle_CreepMenu_ScenarioState() { }
        static PreRoundIdle_CreepMenu_ScenarioState instance = new PreRoundIdle_CreepMenu_ScenarioState();

        public static PreRoundIdle_CreepMenu_ScenarioState Get(ScenarioInstance s) {
            var initialSquad = s.references.ui.preRoundBehaviour.OpenCreepMenu(s);
            instance.subState = SubState_Idle.Get(initialSquad);
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

            public static SubState_Idle Get(CreepSquad currentSelectCreep) {
                instance.currentSelectCreep = currentSelectCreep;
                return instance;
            }

            //******************************************************************************
            // State
            //******************************************************************************

            CreepSquad currentSelectCreep;
            int selectedAttachment;

            //******************************************************************************
            // Implementation
            //******************************************************************************

            public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
                PreRound_CreepMenu_Behaviour creepMenu = s.references.ui.preRoundBehaviour.creepMenu;

                // select creep
                var creepSelected = creepMenu.creepSelected;
                if (creepSelected != null) {
                    currentSelectCreep = creepSelected;
                    creepMenu.SetCreepDetails(creepSelected);
                    creepMenu.DehighlightAllAttachmentSlots();
                }

                // select attachment slot
                var atchSelected = creepMenu.attachmentSelected;
                if (atchSelected >= 0) {
                    selectedAttachment = atchSelected;

                    var atch = currentSelectCreep.GetAttachment(atchSelected);
                    if (atch == null) {
                        creepMenu.DehighlightAllAttachmentSlots();
                        return SubState_SelectAtchForCreep.Get(s, currentSelectCreep);
                    }
                    else {
                        creepMenu.HighlightAttachmentSlot(atchSelected);
                    }
                }

                // remove attachment
                if (creepMenu.removeAttachmentButton.Clicked) {
                    // apply modification
                    currentSelectCreep.RemoveItem(selectedAttachment);

                    // deselect
                    creepMenu.DehighlightAllAttachmentSlots();
                    selectedAttachment = -1;

                    // update display
                    creepMenu.SetCreepDetails(currentSelectCreep);
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

            public static SubState_SelectAtchForCreep Get(ScenarioInstance s, CreepSquad currentSelectCreep) {
                int numAttachInventory = s.playerFunctions.NumAttachableInInventory(currentSelectCreep);
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

            CreepSquad currentSelectCreep;
            CreepAttatchment selectedAttachment;

            //******************************************************************************
            // Implementation
            //******************************************************************************

            public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
                PreRound_CreepMenu_Behaviour creepMenu = s.references.ui.preRoundBehaviour.creepMenu;

                // select item
                if (creepMenu.itemSelected != null) {
                    creepMenu.ActivateAttachmentApplyButton(true);
                    creepMenu.SetDescriptionText(creepMenu.itemSelected.GetDescription());
                    selectedAttachment = creepMenu.itemSelected;
                }

                // apply item
                if (creepMenu.applyAttachmentButton.Clicked && selectedAttachment != null) {
                    // close description
                    creepMenu.SetDescriptionText("");
                    // hide button
                    creepMenu.ActivateAttachmentApplyButton(false);

                    // apply modification
                    s.playerFunctions.RemoveItem(selectedAttachment);
                    currentSelectCreep.AddModifier(selectedAttachment);

                    creepMenu.SetCreepDetails(currentSelectCreep);

                    // clear cache
                    selectedAttachment = null;

                    // close if inventory empty
                    int numAttachInventory = s.playerFunctions.NumAttachableInInventory(currentSelectCreep);

                    if (numAttachInventory <= 0) {
                        creepMenu.CloseItemSelect();
                        return SubState_Idle.Get(currentSelectCreep);
                    }

                    // close if all attachments full
                    if (currentSelectCreep.NumAttachments() >= 10) {
                        creepMenu.CloseItemSelect();
                        return SubState_Idle.Get(currentSelectCreep);
                    }

                    // update item menu
                    creepMenu.OpenItemSelect();
                }

                return null;
            }
        }
    }
}
