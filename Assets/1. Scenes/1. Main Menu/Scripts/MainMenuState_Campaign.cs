using UnityEngine;
using Core;
using Core.Campaign;


namespace MainMenu {
    class MainMenuState_Campaign : IFSM_State<MainMenu_Main> {
        //**********************************************************************************************
        // Singleton
        //**********************************************************************************************

        static MainMenuState_Campaign instance = new MainMenuState_Campaign();
        public static MainMenuState_Campaign Get(MainMenu_Main m) {
            m.mainButtons.campaignRect.anchoredPosition = new Vector2(50, 0);
            m.campaignMenu.Open();
            return instance;
        }

        MainMenuState_Campaign() { }
        
        //**********************************************************************************************
        // Implementation
        //**********************************************************************************************

        public IFSM_State<MainMenu_Main> Update(MainMenu_Main m) {
            // switch to endless menu
            if (MainMenuInputManager.MainButtons.Endless || m.mainButtons.endlessButton.Down) {
                m.mainButtons.campaignRect.anchoredPosition = new Vector2(0, 0);
                m.campaignMenu.rootObject.SetActive(false);
                return MainMenuState_Endless.Get(m);
            }
            // cancel
            if (MainMenuInputManager.cancel) {
                m.mainButtons.campaignRect.anchoredPosition = new Vector2(0, 0);
                m.campaignMenu.rootObject.SetActive(false);
                return MainMenuState_MainMenu.Get();
            }
            // start
            if (m.campaignMenu.startButton.Clicked) {
                InterSceneCommunicator.MainMenu.MissionSelect.yes = true;
                InterSceneCommunicator.MainMenu.MissionSelect.world = m.campaignMenu.currentWorldIndex;
                InterSceneCommunicator.MainMenu.MissionSelect.level = m.campaignMenu.currentSelectedLevelIndex;
                var level = WorldCollection.GetWorld(m.campaignMenu.currentWorldIndex).GetLevelDefinition(m.campaignMenu.currentSelectedLevelIndex);
                InterSceneCommunicator.scenarioParameters = level.GetScenarioParameters();
                return MainMenuState_SceneTransition.Get(1, 23);
            }

            return null;
        }
    }
}
