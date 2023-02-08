using UnityEngine;
using LPE;
using System.Collections.Generic;


namespace Core {
    public class PreRoundIdle_ScenarioState : IFSM_State<ScenarioInstance> {
        //******************************************************************************
        // Singleton
        //******************************************************************************
        PreRoundIdle_ScenarioState() { }
        static PreRoundIdle_ScenarioState instance = new PreRoundIdle_ScenarioState();

        public static PreRoundIdle_ScenarioState Get(ScenarioInstance s) {
            s.references.ui.BeginPreRound();
            return instance;
        }

        
        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            s.HandleMoveZoomInput();
            ScenarioUI_CreepMenu creepMenu = s.references.ui.creepMenu;

            if (InputManager.Start.requested) {
                if (creepMenu.DetailsIsOpen()) {
                    creepMenu.OpenDetailsTab(false);
                    InputManager.Consume.Cancel();
                }
                return PlayRound_ScenarioState.Get(s);
            }

            if (creepMenu.buyCreepButton.Clicked) {
                var newCreep = CreepSelectionUtility.GetRandomNewCreep();
                var squad = s.playerFunctions.GetCreepArmy().AddNewSquad(newCreep);
                squad.Recalculate();
                creepMenu.ReDrawCreepList(s);
            }
            var creepSelected = creepMenu.creepSelected;
            if (creepSelected != null) {
                creepMenu.OpenDetailsTab(true);
                creepMenu.SetCreepDetails(s, creepSelected);
            }

            if (creepMenu.details.buttons.squadParentButton.Clicked) {
                creepMenu.SelectParentOfCurrentSquad(s);
            }
            if (creepMenu.details.buttons.squadChildCarrierButton.Clicked) {
                creepMenu.SelectCarrierChildOfCurrentSquad(s);
            }
            if (creepMenu.details.buttons.squadChildDeathButton.Clicked) {
                creepMenu.SelectDeathChildOfCurrentSquad(s);
            }


            if (creepMenu.details.buttons.statsSubmenuButton.Clicked) {
                creepMenu.OpenStatsSubmenu();
            }
            if (creepMenu.details.buttons.attachmentsSubmenuButton.Clicked) {
                creepMenu.OpenAttachmentsSubmenu();
            }

            // TODO - this section is horrible, find some way to refactor
            if (creepMenu.details.submenu.loadout.tier1_1.button.Clicked) {
                creepMenu.OpenLoadoutSlot(creepMenu.currentFamily.loadout.tier1_1, creepMenu.details.submenu.loadout.tier1_1);
            }
            if (creepMenu.details.submenu.loadout.tier1_2.button.Clicked) {
                creepMenu.OpenLoadoutSlot(creepMenu.currentFamily.loadout.tier1_2, creepMenu.details.submenu.loadout.tier1_2);
            }
            if (creepMenu.details.submenu.loadout.tier1_3.button.Clicked) {
                creepMenu.OpenLoadoutSlot(creepMenu.currentFamily.loadout.tier1_3, creepMenu.details.submenu.loadout.tier1_3);
            }
            if (creepMenu.details.submenu.loadout.specialization.button.Clicked) {
                creepMenu.OpenLoadoutSlot(creepMenu.currentFamily.loadout.specialization, creepMenu.details.submenu.loadout.specialization);
            }
            if (creepMenu.details.submenu.loadout.resource.button.Clicked) {
                creepMenu.OpenLoadoutSlot(creepMenu.currentFamily.loadout.resource, creepMenu.details.submenu.loadout.resource);
            }

            if (creepMenu.loadout.selectPanel.applyButton.Clicked) {
                creepMenu.loadout.selectPanel.selectedLoadout.currentAttactment.ResetAttachment(creepMenu.loadout.selectPanel.selectedAttachment);
                creepMenu.ReopenLoadoutSlot();
                creepMenu.currentFamily.Recalculate();
                creepMenu.RedrawCreepDetails(s);
                creepMenu.ReDrawCreepList(s);
            }

            if (creepMenu.loadout.upgradePanel.upgradeButton.Clicked) {
                creepMenu.loadout.upgradePanel.selectedLoadout.currentAttactment.UpgradeLevel();
                creepMenu.ReopenLoadoutSlot();
                creepMenu.currentFamily.Recalculate();
                creepMenu.RedrawCreepDetails(s);
                creepMenu.ReDrawCreepList(s);
            }

            if (InputManager.Cancel.requested) {
                if (creepMenu.DetailsIsOpen()) {
                    creepMenu.OpenDetailsTab(false);
                    InputManager.Consume.Cancel();
                }
            }
     
            return null;
        }
    }
}
