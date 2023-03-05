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
        IFSM_State<ScenarioInstance> detailsSubstate;

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

            // substate slot
            detailsSubstate = detailsSubstate.Update(s);
            return this;
        }
    }





    public static class CreepMenu_LoadoutDetails_Utility {
        public static IFSM_State<ScenarioInstance> CheckLoadoutSlotSelection(ScenarioInstance s, CreepMenu menu) {
            var (lb, ls) = menu.loadoutPanel.CheckSelection(menu.currentSquad.loadout);
            if (ls != null) {
                return CreepMenu_LoadoutDetails_UpgradeMenu_GameState.Get(s, menu, ls);
            }

            return null;
        }
    }






    public class CreepMenu_LoadoutDetails_Idle_GameState : IFSM_State<ScenarioInstance> {
        CreepMenu_LoadoutDetails_Idle_GameState() { }
        static CreepMenu_LoadoutDetails_Idle_GameState instance = new CreepMenu_LoadoutDetails_Idle_GameState();

        public static CreepMenu_LoadoutDetails_Idle_GameState Get(ScenarioInstance s, CreepMenu menu) {
            instance.menu = menu;
            return instance;
        }

        CreepMenu menu;

        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            return CreepMenu_LoadoutDetails_Utility.CheckLoadoutSlotSelection(s, menu) ?? this;
        }
    }


    public class CreepMenu_LoadoutDetails_UpgradeMenu_GameState : IFSM_State<ScenarioInstance> {
        CreepMenu_LoadoutDetails_UpgradeMenu_GameState() { }
        static CreepMenu_LoadoutDetails_UpgradeMenu_GameState instance = new CreepMenu_LoadoutDetails_UpgradeMenu_GameState();

        public static CreepMenu_LoadoutDetails_UpgradeMenu_GameState Get(ScenarioInstance s, CreepMenu menu, CreepLoadoutSlot slot) {
            instance.menu = menu;
            instance.slot = slot;
            menu.loadoutPanel.detailsPanel.Open();
            menu.loadoutPanel.detailsPanel.DisplayCurrent(s, slot);
            return instance;
        }

        CreepMenu menu;
        CreepLoadoutSlot slot;

        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
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

            if (menu.loadoutPanel.detailsPanel.CheckSecondaryButton()) {
                return CreepMenu_LoadoutDetails_ReplaceMenu_GameState.Get(s, menu, slot);
            }

            return CreepMenu_LoadoutDetails_Utility.CheckLoadoutSlotSelection(s, menu) ?? this;
        }
    }


    public class CreepMenu_LoadoutDetails_ReplaceMenu_GameState : IFSM_State<ScenarioInstance> {
        CreepMenu_LoadoutDetails_ReplaceMenu_GameState() { }
        static CreepMenu_LoadoutDetails_ReplaceMenu_GameState instance = new CreepMenu_LoadoutDetails_ReplaceMenu_GameState();

        public static CreepMenu_LoadoutDetails_ReplaceMenu_GameState Get(ScenarioInstance s, CreepMenu menu, CreepLoadoutSlot slot) {
            instance.menu = menu;
            instance.slot = slot;
            instance.pointer = 0;
            if (slot.allowedAttachments[0] == slot.currentAttactment.definition) {
                instance.pointer++;
            }
            menu.loadoutPanel.detailsPanel.DisplayPurchaseOption(s, slot, instance.pointer);
            return instance;
        }

        CreepMenu menu;
        CreepLoadoutSlot slot;
        int pointer;


        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            // Back
            if (menu.loadoutPanel.detailsPanel.CheckSecondaryButton()) {
                return CreepMenu_LoadoutDetails_UpgradeMenu_GameState.Get(s, menu, slot);
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
                    return CreepMenu_LoadoutDetails_UpgradeMenu_GameState.Get(s, menu, slot);
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
    }
   
    
    public class CreepMenu_LoadoutDetails_Purchase_GameState : IFSM_State<ScenarioInstance> {
        CreepMenu_LoadoutDetails_Purchase_GameState() { }
        static CreepMenu_LoadoutDetails_Purchase_GameState instance = new CreepMenu_LoadoutDetails_Purchase_GameState();

        public static CreepMenu_LoadoutDetails_Purchase_GameState Get(ScenarioInstance s, CreepMenu menu) {
            instance.menu = menu;
            return instance;
        }

        CreepMenu menu;

        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            return this;
        }
    }
}
