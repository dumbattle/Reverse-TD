using System;
using System.Collections.Generic;
using UnityEngine;
using LPE;
using Core;
using Core.Campaign;
using UnityEngine.SceneManagement;


namespace MainMenu {
    public class MainMenu_Main : MonoBehaviour {
        public MainButtons mainButtons;
        public CampaignMenu campaignMenu;
        public EndlessMenu endlessMenu;

        void Start() {
            endlessMenu.rootObject.SetActive(false);
            campaignMenu.rootObject.SetActive(false);

            Application.targetFrameRate = 60;
            MainMenuStateController.Init();
        }

        void Update() {
            if (Input.GetKey(KeyCode.Escape)) {
                MainMenuInputManager.Set.Cancel();
            }

            MainMenuStateController.Update(this);

        }

        [Serializable]
        public class CampaignMenu {
            public GameObject rootObject;
            public LPEButtonBehaviour startButton;


            public MainMenu_LevelSelectEntryBehaviour levelEntrySrc;
            public Sprite levelEntryBackground_unselected;
            public Sprite levelEntryBackground_selected;
            public Sprite starBlank;
            public Sprite starFilled1;
            public Sprite starFilled2;
            public Sprite starFilled3;

            List<MainMenu_LevelSelectEntryBehaviour> levelEntries = new List<MainMenu_LevelSelectEntryBehaviour>();

            public int currentWorldIndex { get; private set; } = 0;
            public int currentSelectedLevelIndex { get; private set; }

            public void Open() {
                // precation
                levelEntrySrc.gameObject.SetActive(false); 

                // open current world
                OpenWorld(currentWorldIndex);
            }

            void OpenWorld(int worldIndex) {
                // open menu
                rootObject.SetActive(true);

                // close all level entries
                foreach (var e in levelEntries) {
                    e.gameObject.SetActive(false);
                }

                //get world
                var world = WorldCollection.GetWorld(currentWorldIndex);

                // set entries
                currentSelectedLevelIndex = -1;
                for (int i = 0; i < world.NumLevels(); i++) {
                    if (levelEntries.Count <= i) {
                        // create new entry
                        int index = i;
                        var newEntry = Instantiate(levelEntrySrc, levelEntrySrc.transform.parent);
                        newEntry.button.SetClickListener(() => OnLevelSelected(index));
                        levelEntries.Add(newEntry);
                    }

                    // get entry
                    var e = levelEntries[i];

                    // set values
                    e.gameObject.SetActive(true);
                    e.bakground.sprite = levelEntryBackground_unselected;
                    e.levelText.text = $"{currentWorldIndex + 1}.{i + 1}";
                }

                // select first level
                OnLevelSelected(0);
            }
            void OnLevelSelected(int level) {
                if (currentSelectedLevelIndex >= 0) {
                    levelEntries[currentSelectedLevelIndex].bakground.sprite = levelEntryBackground_unselected;
                }
                currentSelectedLevelIndex = level;
                levelEntries[currentSelectedLevelIndex].bakground.sprite = levelEntryBackground_selected;
            }
        }
        [Serializable]
        public struct EndlessMenu {
            public GameObject rootObject;
            public LPEButtonBehaviour smallMapButton;
            public LPEButtonBehaviour mediumMapButton;
            public LPEButtonBehaviour largeMapButton;
        }
        [Serializable]
        public struct MainButtons {
            public LPEButtonBehaviour campaignButton;
            public RectTransform campaignRect;

            public LPEButtonBehaviour endlessButton;
            public RectTransform endlessRect;
        }
    }

    // Static because there should only ever be 1 main menu controller that does not change
    public static class MainMenuStateController {
        static IFSM_State<MainMenu_Main> currentState;

        public static void Init() {
            currentState = MainMenuState_MainMenu.Get();
        }

        public static void Update(MainMenu_Main m) {
            currentState = currentState.Update(m) ?? currentState;
            MainMenuInputManager.Clear();
        }
    }

    class MainMenuState_MainMenu : IFSM_State<MainMenu_Main> {
        //**********************************************************************************************
        // Singleton
        //**********************************************************************************************

        static MainMenuState_MainMenu instance = new MainMenuState_MainMenu();
        public static MainMenuState_MainMenu Get() {
            return instance;
        }

        MainMenuState_MainMenu() { }

        //**********************************************************************************************
        // Implementation
        //**********************************************************************************************

        public IFSM_State<MainMenu_Main> Update(MainMenu_Main m) {
            if (MainMenuInputManager.MainButtons.Endless || m.mainButtons.endlessButton.Down) {
                return MainMenuState_Endless.Get(m);
            }
            if (m.mainButtons.campaignButton.Down) {
                return MainMenuState_Campaign.Get(m);
            }
            return null;
        }
    }

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
                var level = WorldCollection.GetWorld(m.campaignMenu.currentWorldIndex).GetLevelDefinition(m.campaignMenu.currentSelectedLevelIndex);
                InterSceneCommunicator.scenarioParameters = level.GetScenarioParameters();
                SceneManager.LoadScene(1);
            }

            return null;
        }
    }
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
                p.mainTowerBl = new Vector2Int(p.width / 2, p.height / 2);
                InterSceneCommunicator.scenarioParameters = p;
                SceneManager.LoadScene(1);
            }
            if (m.endlessMenu.mediumMapButton.Clicked) {
                var p = new ScenarioParameters() { width = 25, height = 25 };
                p.mainTowerBl = new Vector2Int(p.width / 2, p.height / 2);
                InterSceneCommunicator.scenarioParameters = p;
                SceneManager.LoadScene(1);
            }
            if (m.endlessMenu.largeMapButton.Clicked) {
                var p = new ScenarioParameters() { width = 35, height = 35 };
                p.mainTowerBl = new Vector2Int(p.width / 2, p.height / 2);
                InterSceneCommunicator.scenarioParameters = p;
                SceneManager.LoadScene(1);
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

    public static class MainMenuInputManager {
        public static class Set {
            public static void Cancel() {
                cancel = true;
            }
            public static class MainButtons {
                public static void Endless() {
                    MainMenuInputManager.MainButtons.Endless = true;
                }
            }
        }

        public static void Clear() {
            MainButtons.Endless = false;
            cancel = false;
        }

        public static class MainButtons {
            public static bool Endless;
        }

        public static bool cancel;
    }
}
