using UnityEngine;
using Core;
using UnityEngine.SceneManagement;


namespace MainMenu {
    class MainMenuState_SceneTransition : IFSM_State<MainMenu_Main> {
        //**********************************************************************************************
        // Singleton
        //**********************************************************************************************

        static MainMenuState_SceneTransition instance = new MainMenuState_SceneTransition();
        public static MainMenuState_SceneTransition Get(int sceneIndex, int fadeTime) {
            instance.sceneIndex = sceneIndex;
            instance.fadeTime = fadeTime;
            instance.time = 0;
            return instance;
        }

        MainMenuState_SceneTransition() { }
        //**********************************************************************************************
        // State
        //**********************************************************************************************

        int sceneIndex;
        float time;
        int fadeTime;

        //**********************************************************************************************
        // Implementation
        //**********************************************************************************************

        public IFSM_State<MainMenu_Main> Update(MainMenu_Main m) {
            time += FrameUtility.GetFrameMultiplier(false);

            m.fadeImage.color = new Color(0, 0, 0, time / fadeTime);

            if (time > fadeTime) {
                SceneManager.LoadScene(sceneIndex);
            }
            return null;
        }
    }
}
