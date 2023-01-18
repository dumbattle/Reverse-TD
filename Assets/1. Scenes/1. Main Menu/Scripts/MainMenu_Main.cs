using System;
using System.Collections.Generic;
using UnityEngine;
using LPE;
using Core;
using UnityEngine.SceneManagement;


namespace MainMenu {
    public class MainMenu_Main : MonoBehaviour {
        public MainButtons mainButtons;
        public FreeplayMenu freeplayMenu;


        void Start() {
            freeplayMenu.rootObject.SetActive(false);
            Application.targetFrameRate = 60;
            MainMenuStateController.Init();
            mainButtons.playButton.SetDownListener(MainMenuInputManager.Set.MainButtons.Play);
        }

        void Update() {
            MainMenuStateController.Update(this);
           
            if (freeplayMenu.smallMapButton.Clicked) {
                var p = new ScenarioParameters() { width = 15, height = 15 };
                p.mainTowerBl = new Vector2Int(p.width / 2, p.height / 2);
                InterSceneCommunicator.scenarioParameters = p;
                SceneManager.LoadScene(1);
            }
            if (freeplayMenu.mediumMapButton.Clicked) {
                var p = new ScenarioParameters() { width = 25, height = 25 };
                p.mainTowerBl = new Vector2Int(p.width / 2, p.height / 2);
                InterSceneCommunicator.scenarioParameters = p;
                SceneManager.LoadScene(1);
            }
            if (freeplayMenu.largeMapButton.Clicked) {
                var p = new ScenarioParameters() { width = 35, height = 35 };
                p.mainTowerBl = new Vector2Int(p.width / 2, p.height / 2);
                InterSceneCommunicator.scenarioParameters = p;
                SceneManager.LoadScene(1);
            }
        }

        [Serializable]
        public struct FreeplayMenu {
            public GameObject rootObject;
            public LPEButtonBehaviour smallMapButton;
            public LPEButtonBehaviour mediumMapButton;
            public LPEButtonBehaviour largeMapButton;
        }
        [Serializable]
        public struct MainButtons {
            public LPEButtonBehaviour playButton;
            public RectTransform playRect;
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
            if (MainMenuInputManager.MainButtons.play) {
                return MainMenuState_Play_PlayPressedAnim.Get();
            }
            return null;
        }
    }
   
    class MainMenuState_Play_PlayPressedAnim : IFSM_State<MainMenu_Main> {
        //**********************************************************************************************
        // Singleton
        //**********************************************************************************************

        static MainMenuState_Play_PlayPressedAnim instance = new MainMenuState_Play_PlayPressedAnim();
        public static MainMenuState_Play_PlayPressedAnim Get() {
            instance.timer = 0;
            return instance;
        }

        MainMenuState_Play_PlayPressedAnim() { }

        //**********************************************************************************************
        // State
        //**********************************************************************************************

        int timer = 0;

        //**********************************************************************************************
        // Implementation
        //**********************************************************************************************

        public IFSM_State<MainMenu_Main> Update(MainMenu_Main m) {
            // set scale (shrink pulse)
            //timer++;
            //float scale = 
            //    timer <= 5 ?
            //    Mathf.Lerp(1, .85f, timer / 5f) :
            //    Mathf.Lerp(1, .85f, (10 - timer) / 5f);
            //m.playButton.transform.localScale = new Vector3(scale, scale, 1);
            m.mainButtons.playRect.anchoredPosition = new Vector2(50, 0);
            // next state
            m.freeplayMenu.rootObject.SetActive(true);
            //SceneManager.LoadScene(1);
            return MainMenuState_MainMenu.Get();
        }
    }

    public static class MainMenuInputManager {
        public static class Set {
            public static class MainButtons {
                public static void Play() {
                    MainMenuInputManager.MainButtons.play = true;
                }
            }
        }
        
        public static void Clear() {
            MainButtons.play = false;
        }
        
        public static class MainButtons {
            public static bool play;
        }
    }
}
