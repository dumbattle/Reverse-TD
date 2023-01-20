using Core;


namespace MainMenu {
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
}
