using Core;
using UnityEngine;


namespace GameUI.CreepMenus.GameStates {
    public class CreepMenu_BuyCreepMenu_GameState : IFSM_State<ScenarioInstance> {
        CreepMenu_BuyCreepMenu_GameState() { }
        static CreepMenu_BuyCreepMenu_GameState instance = new CreepMenu_BuyCreepMenu_GameState();

        public static CreepMenu_BuyCreepMenu_GameState Get(ScenarioInstance s, CreepMenu menu) {
            instance.menu = menu;
            menu.SetSelectedSquad(null);
            menu.purchasePanel.Open(s, s.playerFunctions.GetNewCreepCost());

            return instance;
        }

        CreepMenu menu;


        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            // close all
            if (InputManager.Cancel.requested) {
                InputManager.Consume.Cancel();
                menu.purchasePanel.Close();
                menu.Close();
                return null;
            }

            // purchase
            if (menu.purchasePanel.buyButton.Clicked) {
                if (s.playerFunctions.GetCurrentResources().Satisfies(menu.purchasePanel.cost)) {
                    s.playerFunctions.Spend(menu.purchasePanel.cost);
                    var newSquad = s.playerFunctions.GetCreepArmy().AddNewSquad(CreepSelectionUtility.GetRandomNewCreep());
                    menu.purchasePanel.Close();
                    menu.selections.ReDraw();
                    menu.SetSelectedSquad(newSquad);
                    return CreepMenu_StatsPanel_GameState.Get(s, menu);
                }
            }


            return this;
        }
    }

    public class CreepMenu_StatsPanel_GameState : IFSM_State<ScenarioInstance> {
        CreepMenu_StatsPanel_GameState() { }
        static CreepMenu_StatsPanel_GameState instance = new CreepMenu_StatsPanel_GameState();

        public static CreepMenu_StatsPanel_GameState Get(ScenarioInstance s, CreepMenu menu) {
            instance.menu = menu;
            menu.statEntries.Redraw(s, menu.currentSquad);
            return instance;
        }

        CreepMenu menu;

        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            if (InputManager.Cancel.requested) {
                InputManager.Consume.Cancel();
                menu.Close();
                return null;
            }

            CreepMenu_CreepSelection_InputAction creepSelectionAction = menu.selections.GetInputAction();

            if (creepSelectionAction.squad != null) {
                menu.SetSelectedSquad(creepSelectionAction.squad);
                menu.statEntries.Redraw(s, creepSelectionAction.squad);
                menu.purchasePanel.Close();
            }

            if (creepSelectionAction.emptySelected) {
                menu.statEntries.Close();
                return CreepMenu_BuyCreepMenu_GameState.Get(s, menu);
            }

            if (creepSelectionAction.upSelected) {
                menu.selections.MovePageUp();
            }


            if (creepSelectionAction.downSelected) {
                menu.selections.MovePageDown();
            }

            if (menu.statEntries.hp.upgradeButton.Clicked) {
                TryUpgradeStat(s, menu.currentSquad.stats.hp);
            }
            if (menu.statEntries.count.upgradeButton.Clicked) {
                TryUpgradeStat(s, menu.currentSquad.stats.count);
            }
            if (menu.statEntries.dmgMult.upgradeButton.Clicked) {
                TryUpgradeStat(s, menu.currentSquad.stats.damageMult);
            }
            if (menu.statEntries.spd.upgradeButton.Clicked) {
                TryUpgradeStat(s, menu.currentSquad.stats.spd);
            }
            if (menu.statEntries.spawnRate.upgradeButton.Clicked) {
                TryUpgradeStat(s, menu.currentSquad.stats.spawnRate);
            }
            if (menu.statEntries.incomeMult.upgradeButton.Clicked) {
                TryUpgradeStat(s, menu.currentSquad.stats.incomeMult);
            }

            return this;
        }

        void TryUpgradeStat(ScenarioInstance s, BasicCreepStat stat) {
            var cost = stat.GetCostForNextLevel();
            if (s.playerFunctions.GetCurrentResources().Satisfies(cost)) {
                stat.LevelUp();
                s.playerFunctions.Spend(cost);
                menu.currentSquad.Recalculate();
                menu.statEntries.Redraw(s, menu.currentSquad);
            }
        }
    }
}
