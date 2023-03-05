using Core;


namespace GameUI.CreepMenus.GameStates {
    public class CreepMenu_BuyCreepMenu_GameState : IFSM_State<ScenarioInstance> {
        //************************************************************************************
        // Singleton
        //************************************************************************************

        CreepMenu_BuyCreepMenu_GameState() { }
        static CreepMenu_BuyCreepMenu_GameState instance = new CreepMenu_BuyCreepMenu_GameState();

        public static CreepMenu_BuyCreepMenu_GameState Get(ScenarioInstance s, CreepMenu menu) {
            instance.menu = menu;
            menu.SetSelectedSquad(null);
            menu.purchasePanel.Open(s, s.playerFunctions.GetNewCreepCost());

            return instance;
        }


        //************************************************************************************
        // Implementation
        //************************************************************************************

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

            // selection
            CreepMenu_CreepSelection_InputAction creepSelectionAction = menu.selections.GetInputAction();

            if (creepSelectionAction.squad != null) {
                menu.SetSelectedSquad(creepSelectionAction.squad);
                menu.purchasePanel.Close();
                return CreepMenu_StatsPanel_GameState.Get(s, menu);
            }

            if (creepSelectionAction.emptySelected) {
                // do nothing
            }

            if (creepSelectionAction.upSelected) {
                menu.selections.MovePageUp();
            }

            if (creepSelectionAction.downSelected) {
                menu.selections.MovePageDown();
            }

            return this;
        }
    }
}
