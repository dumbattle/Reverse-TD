using UnityEngine;
using Core;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace MainMenu {
    class MainMenuState_Endless : IFSM_State<MainMenu_Main> {
        //**********************************************************************************************
        // Singleton
        //**********************************************************************************************

        static MainMenuState_Endless instance = new MainMenuState_Endless();
        public static MainMenuState_Endless Get(MainMenu_Main m) {
            m.mainButtons.endlessRect.anchoredPosition = new Vector2(50, 0);
            m.endlessMenu.rootObject.SetActive(true);
            return instance;
        }

        MainMenuState_Endless() { }

        //**********************************************************************************************
        // Implementation
        //**********************************************************************************************

        public IFSM_State<MainMenu_Main> Update(MainMenu_Main m) {
            // map selected
            if (m.endlessMenu.smallMapButton.Clicked) {
                var p = new ScenarioParameters() { width = 15, height = 15 };
                p.mainTowers = new List<MainTowerParameters> {
                    new MainTowerParameters(TowerDefinitionCatalog.main_basic_diamond_blue, 100_000,new Vector2Int(p.width / 2, p.height / 2))
                };
                InterSceneCommunicator.scenarioParameters = p;
                return MainMenuState_SceneTransition.Get(1, 23);
            }
            if (m.endlessMenu.mediumMapButton.Clicked) {
                var p = new ScenarioParameters() { width = 25, height = 25 };
                p.mainTowers = new List<MainTowerParameters> {
                    new MainTowerParameters(TowerDefinitionCatalog.main_basic_diamond_blue, 100_000,new Vector2Int(p.width / 2, p.height / 2))
                };
                InterSceneCommunicator.scenarioParameters = p;
                return MainMenuState_SceneTransition.Get(1, 23);
            }
            if (m.endlessMenu.largeMapButton.Clicked) {
                var p = new ScenarioParameters() { width = 35, height = 35 };
                p.mainTowers = new List<MainTowerParameters> {
                    new MainTowerParameters(TowerDefinitionCatalog.main_basic_diamond_blue, 100_000,new Vector2Int(p.width / 2, p.height / 2))
                };
                return MainMenuState_SceneTransition.Get(1, 23);
            }

            // switch to campagin menu
            if (m.mainButtons.campaignButton.Down) {
                m.mainButtons.endlessRect.anchoredPosition = new Vector2(0, 0);
                m.endlessMenu.rootObject.SetActive(false);
                return MainMenuState_Campaign.Get(m);
            }

            // cancel
            if (MainMenuInputManager.cancel) {
                m.mainButtons.endlessRect.anchoredPosition = new Vector2(0, 0);
                m.endlessMenu.rootObject.SetActive(false);
                return MainMenuState_MainMenu.Get();
            }

            return null;
        }
    }
}
