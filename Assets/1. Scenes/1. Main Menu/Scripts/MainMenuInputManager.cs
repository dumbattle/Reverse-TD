namespace MainMenu {
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
