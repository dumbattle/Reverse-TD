using Core;

namespace GameUI.CreepMenus.GameStates {
    public class CreepMenu_Open_GameState : IFSM_State<ScenarioInstance> {
        CreepMenu_Open_GameState() { }
        static CreepMenu_Open_GameState instance = new CreepMenu_Open_GameState();

        public static CreepMenu_Open_GameState Get(CreepMenu menu) {
            instance.menu = menu;
            return instance;
        }

        CreepMenu menu;



        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            menu.Open(s);
            menu.CloseAllSubMenus();
            return CreepMenu_StatsPanel_GameState.Get(s, menu);
        }
    }
}