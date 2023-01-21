using Core;


namespace MainMenu {
    // Static because there should only ever be 1 main menu controller that does not change
    public static class MainMenuStateController {
        static IFSM_State<MainMenu_Main> currentState;

        public static void Init(MainMenu_Main m) {

            if (InterSceneCommunicator.MainMenu.MissionSelect.yes) {
                InterSceneCommunicator.MainMenu.MissionSelect.yes = false;
                // do stuff
                currentState = MainMenuState_FadeIn.Get(MainMenuState_Campaign.Get(m));

                m.campaignMenu.OpenWorld(InterSceneCommunicator.MainMenu.MissionSelect.world);
                m.campaignMenu.SelectLevel(InterSceneCommunicator.MainMenu.MissionSelect.level);
            }
            else {
                currentState = MainMenuState_FadeIn.Get(MainMenuState_MainMenu.Get());
            }
        }

        public static void Update(MainMenu_Main m) {
            currentState = currentState.Update(m) ?? currentState;
            MainMenuInputManager.Clear();
        }
    }
}
