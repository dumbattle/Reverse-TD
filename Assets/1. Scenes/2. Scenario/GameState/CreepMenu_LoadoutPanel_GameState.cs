using Core;
using UnityEngine;


namespace GameUI.CreepMenus.GameStates {
    public class CreepMenu_LoadoutPanel_GameState : IFSM_State<ScenarioInstance> {
        CreepMenu_LoadoutPanel_GameState() { }
        static CreepMenu_LoadoutPanel_GameState instance = new CreepMenu_LoadoutPanel_GameState();

        public static CreepMenu_LoadoutPanel_GameState Get(ScenarioInstance s, CreepMenu menu) {
            instance.menu = menu;
            instance.detailsSubstate = CreepMenu_LoadoutDetails_Idle_GameState.Get(s, menu);
            menu.loadoutPanel.Open(s, menu.currentSquad.loadout);
            return instance;
        }

        CreepMenu menu;
        CreepMenu_DetailsSubState detailsSubstate;

        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            // exit all menus
            if (InputManager.Cancel.requested) {
                InputManager.Consume.Cancel();
                menu.Close();
                return null;
            }

            // select submenu
            if (menu.submenuButtons.StatsClicked()) {
                menu.loadoutPanel.Close();
                return CreepMenu_StatsPanel_GameState.Get(s, menu);
            }

            // select creep
            CreepMenu_CreepSelection_InputAction creepSelectionAction = menu.selections.GetInputAction();

            if (creepSelectionAction.squad != null) {
                menu.SetSelectedSquad(creepSelectionAction.squad);
                menu.loadoutPanel.Redraw(s, creepSelectionAction.squad.loadout);
                detailsSubstate = detailsSubstate.SwapCreep(s, creepSelectionAction.squad);
                return this;
            }

            if (creepSelectionAction.emptySelected) {
                menu.loadoutPanel.Close();
                return CreepMenu_BuyCreepMenu_GameState.Get(s, menu);
            }

            if (creepSelectionAction.upSelected) {
                menu.selections.MovePageUp();
            }

            if (creepSelectionAction.downSelected) {
                menu.selections.MovePageDown();
            }

            // substate (loadout slot)
            detailsSubstate = detailsSubstate.Update(s);
            return this;
        }
    }



    public abstract class CreepMenu_DetailsSubState {
        public abstract CreepMenu_DetailsSubState Update(ScenarioInstance s);
        public abstract CreepMenu_DetailsSubState SwapCreep(ScenarioInstance s, CreepSquad c);
    }

    public static class CreepMenu_LoadoutDetails_Utility {
        public static CreepMenu_DetailsSubState CheckLoadoutSlotSelection(ScenarioInstance s, CreepMenu menu) {
            var loadoutBeaviour = menu.loadoutPanel.CheckSelection(menu.currentSquad.loadout);

            if (loadoutBeaviour != null) {
                return OpenSelection(s, menu, loadoutBeaviour.selector);
            }
            return null;
        }

        public static CreepMenu_DetailsSubState OpenSelection(ScenarioInstance s, CreepMenu menu, CreepLoadoutSlotSelector selector) {
            var slot = selector.Select(menu.currentSquad.loadout);
            var current = slot.currentAttactment.definition;
            menu.loadoutPanel.detailsPanel.Open();
            return
                current != null
                ? (CreepMenu_DetailsSubState)CreepMenu_LoadoutDetails_UpgradeMenu_GameState.Get(s, menu, selector)
                : (CreepMenu_DetailsSubState)CreepMenu_LoadoutDetails_Purchase_GameState.Get(s, menu, selector);
        }
    }






    public class CreepMenu_LoadoutDetails_Idle_GameState : CreepMenu_DetailsSubState {
        CreepMenu_LoadoutDetails_Idle_GameState() { }
        static CreepMenu_LoadoutDetails_Idle_GameState instance = new CreepMenu_LoadoutDetails_Idle_GameState();

        public static CreepMenu_LoadoutDetails_Idle_GameState Get(ScenarioInstance s, CreepMenu menu) {
            instance.menu = menu;
            return instance;
        }

        CreepMenu menu;

        public override CreepMenu_DetailsSubState Update(ScenarioInstance s) {
            return CreepMenu_LoadoutDetails_Utility.CheckLoadoutSlotSelection(s, menu) ?? this;
        }

        public override CreepMenu_DetailsSubState SwapCreep(ScenarioInstance s, CreepSquad c) {
            return this;
        }
    }


    public class CreepMenu_LoadoutDetails_UpgradeMenu_GameState : CreepMenu_DetailsSubState {
        CreepMenu_LoadoutDetails_UpgradeMenu_GameState() { }
        static CreepMenu_LoadoutDetails_UpgradeMenu_GameState instance = new CreepMenu_LoadoutDetails_UpgradeMenu_GameState();

        public static CreepMenu_LoadoutDetails_UpgradeMenu_GameState Get(ScenarioInstance s, CreepMenu menu, CreepLoadoutSlotSelector slotSelector) {
            instance.menu = menu;
            instance.slotSelector = slotSelector;
            instance.slot = slotSelector.Select(menu.currentSquad.loadout);
            menu.loadoutPanel.detailsPanel.DisplayCurrent(s, instance.slot);
            return instance;
        }

        CreepMenu menu;
        CreepLoadoutSlot slot;
        CreepLoadoutSlotSelector slotSelector;

        public override CreepMenu_DetailsSubState Update(ScenarioInstance s) {
            // upgrade
            if (menu.loadoutPanel.detailsPanel.CheckPurchase()) {
                var cost = slot.currentAttactment.GetCostForUpgrade();
                if (cost != null && s.playerFunctions.TrySpend(cost)) {
                    slot.currentAttactment.UpgradeLevel();
                    menu.currentSquad.Recalculate();
                    menu.loadoutPanel.detailsPanel.DisplayCurrent(s, slot);
                    menu.loadoutPanel.Redraw(s, menu.currentSquad.loadout);

                    return this;
                }
            }

            // replace button
            if (menu.loadoutPanel.detailsPanel.CheckSecondaryButton()) {
                return CreepMenu_LoadoutDetails_ReplaceMenu_GameState.Get(s, menu, slotSelector);
            }

            return CreepMenu_LoadoutDetails_Utility.CheckLoadoutSlotSelection(s, menu) ?? this;
        }

        public override CreepMenu_DetailsSubState SwapCreep(ScenarioInstance s, CreepSquad c) {
            return CreepMenu_LoadoutDetails_Utility.OpenSelection(s, menu, slotSelector);
        }
    }


    public class CreepMenu_LoadoutDetails_ReplaceMenu_GameState : CreepMenu_DetailsSubState {
        CreepMenu_LoadoutDetails_ReplaceMenu_GameState() { }
        static CreepMenu_LoadoutDetails_ReplaceMenu_GameState instance = new CreepMenu_LoadoutDetails_ReplaceMenu_GameState();

        public static CreepMenu_LoadoutDetails_ReplaceMenu_GameState Get(ScenarioInstance s, CreepMenu menu, CreepLoadoutSlotSelector slotSelector) {
            instance.menu = menu;
            instance.slotSelector = slotSelector;
            instance.slot = slotSelector.Select(menu.currentSquad.loadout);
            instance.pointer = 0;
            if (instance.slot.allowedAttachments[0] == instance.slot.currentAttactment.definition) {
                instance.pointer++;
            }
            menu.loadoutPanel.detailsPanel.DisplayPurchaseOption(s, instance.slot, instance.pointer);
            return instance;
        }

        CreepMenu menu;
        CreepLoadoutSlot slot;
        CreepLoadoutSlotSelector slotSelector;
        int pointer;


        public override CreepMenu_DetailsSubState Update(ScenarioInstance s) {
            // Back
            if (menu.loadoutPanel.detailsPanel.CheckSecondaryButton()) {
                return CreepMenu_LoadoutDetails_UpgradeMenu_GameState.Get(s, menu, slotSelector);
            }

            // Purcahse
            if (menu.loadoutPanel.detailsPanel.CheckPurchase()) {
                var newAtch = slot.allowedAttachments[pointer];

                var cost = newAtch.GetCost(1);
                if (s.playerFunctions.TrySpend(cost)) {
                    slot.currentAttactment.ResetAttachment(newAtch);
                    menu.currentSquad.Recalculate();
                    menu.loadoutPanel.detailsPanel.DisplayCurrent(s, slot);
                    menu.loadoutPanel.Redraw(s, menu.currentSquad.loadout);
                    return CreepMenu_LoadoutDetails_UpgradeMenu_GameState.Get(s, menu, slotSelector);
                }
            }

            // Browse
            int browse = menu.loadoutPanel.detailsPanel.GetDirectionBrowse();
            if (browse != 0) {
                // move
                pointer += browse;

                // wrap pointer
                if (pointer < 0) {
                    pointer = slot.allowedAttachments.Length - 1;
                }
                else if (pointer >= slot.allowedAttachments.Length) {
                    pointer = 0;
                }

                // skip over current
                if (slot.allowedAttachments[pointer] == slot.currentAttactment.definition) {
                    // move again in same direction
                    pointer += browse;

                    // wrap check
                    if (pointer < 0) {
                        pointer = slot.allowedAttachments.Length - 1;
                    }
                    else if (pointer >= slot.allowedAttachments.Length) {
                        pointer = 0;
                    }
                }

                menu.loadoutPanel.detailsPanel.DisplayPurchaseOption(s, slot, pointer);
                return this;
            }

            return CreepMenu_LoadoutDetails_Utility.CheckLoadoutSlotSelection(s, menu) ?? this;
        }


        public override CreepMenu_DetailsSubState SwapCreep(ScenarioInstance s, CreepSquad c) {
            return CreepMenu_LoadoutDetails_Utility.OpenSelection(s, menu, slotSelector);
        }
    }
   
    
    public class CreepMenu_LoadoutDetails_Purchase_GameState : CreepMenu_DetailsSubState {
        CreepMenu_LoadoutDetails_Purchase_GameState() { }
        static CreepMenu_LoadoutDetails_Purchase_GameState instance = new CreepMenu_LoadoutDetails_Purchase_GameState();

        public static CreepMenu_LoadoutDetails_Purchase_GameState Get(ScenarioInstance s,
                                                                      CreepMenu menu,
                                                                      CreepLoadoutSlotSelector slotSelector) {
            instance.menu = menu;
            instance.slotSelector = slotSelector;
            instance.slot = slotSelector.Select(menu.currentSquad.loadout);
            instance.pointer = 0;
            menu.loadoutPanel.detailsPanel.DisplayPurchaseOption(s, instance.slot, instance.pointer);
            return instance;
        }

        CreepMenu menu;
        CreepLoadoutSlot slot;
        CreepLoadoutSlotSelector slotSelector;
        int pointer;

        public override CreepMenu_DetailsSubState Update(ScenarioInstance s) {
            // Close
            if (menu.loadoutPanel.detailsPanel.CheckSecondaryButton()) {
                menu.loadoutPanel.detailsPanel.Close();
                return CreepMenu_LoadoutDetails_Idle_GameState.Get(s, menu);
            }

            // Purcahse
            if (menu.loadoutPanel.detailsPanel.CheckPurchase()) {
                var newAtch = slot.allowedAttachments[pointer];

                var cost = newAtch.GetCost(1);
                if (s.playerFunctions.TrySpend(cost)) {
                    slot.currentAttactment.ResetAttachment(newAtch);
                    menu.currentSquad.Recalculate();
                    menu.loadoutPanel.detailsPanel.DisplayCurrent(s, slot);
                    menu.loadoutPanel.Redraw(s, menu.currentSquad.loadout);
                    return CreepMenu_LoadoutDetails_UpgradeMenu_GameState.Get(s, menu, slotSelector);
                }
            }

            // Browse
            int browse = menu.loadoutPanel.detailsPanel.GetDirectionBrowse();
            if (browse != 0) {
                // move
                pointer += browse;

                // wrap pointer
                if (pointer < 0) {
                    pointer = slot.allowedAttachments.Length - 1;
                }
                else if (pointer >= slot.allowedAttachments.Length) {
                    pointer = 0;
                }

                menu.loadoutPanel.detailsPanel.DisplayPurchaseOption(s, slot, pointer);
                return this;
            }

            return CreepMenu_LoadoutDetails_Utility.CheckLoadoutSlotSelection(s, menu) ?? this;
        }


        public override CreepMenu_DetailsSubState SwapCreep(ScenarioInstance s, CreepSquad c) {
            return CreepMenu_LoadoutDetails_Utility.OpenSelection(s, menu, slotSelector);
        }
    }
}
