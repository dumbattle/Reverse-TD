using UnityEngine;
using Core;


namespace MainMenu {
    class MainMenuState_FadeIn : IFSM_State<MainMenu_Main> {
        //**********************************************************************************************
        // Singleton
        //**********************************************************************************************

        static MainMenuState_FadeIn instance = new MainMenuState_FadeIn();
        public static MainMenuState_FadeIn Get(IFSM_State<MainMenu_Main> nextState) {
            instance.time = 0;
            instance.nextState = nextState;
            return instance;
        }

        MainMenuState_FadeIn() { }
        //**********************************************************************************************
        // State
        //**********************************************************************************************

        IFSM_State<MainMenu_Main> nextState;
        float time;
        const float fadeTime = 30;

        //**********************************************************************************************
        // Implementation
        //**********************************************************************************************

        public IFSM_State<MainMenu_Main> Update(MainMenu_Main m) {
            time += FrameUtility.GetFrameMultiplier(false);

            m.fadeImage.color = new Color(0, 0, 0, 1 - time / fadeTime);

            if (time > fadeTime) {
                return nextState;
            }
            return null;
        }
    }
}
