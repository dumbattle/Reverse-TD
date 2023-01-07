using UnityEngine;


namespace Core {
    public static class InputManager {
        // set
        public static class Set {
            public static void MoveCamera(Vector2 d) {
                InputManager.MoveCamera.dir = d;
            }

            public static void ZoomCamera(float val) {
                InputManager.ZoomCamera.val = val;
            }

            public static void ButtonDown() {
                InputManager.ButtonDown.yes = true;
            }

            public static void Start() {
                InputManager.Start.requested = true;
            }

            public static void Continue() {
                InputManager.Continue.requested = true;
            }
        }

        // clear
        public static void Clear() {
            MoveCamera.dir = new Vector2(0, 0);
            ZoomCamera.val = 0;
            ButtonDown.yes = false;
            Start.requested = false;
            Continue.requested = false;
        }

        // inputs
        public static class MoveCamera {
            public static Vector2 dir;
            public static bool yes => dir != Vector2.zero;
        }

        public static class ZoomCamera {
            public static float val;
            public static bool yes => val != 0;
        }


        /// <summary>
        /// Whenever any button is pressed (not released)
        /// </summary>
        public static class ButtonDown {
            public static bool yes;
        }

        public static class Start {
            public static bool requested = false;
        }
        public static class Continue {
            public static bool requested = false;
        }
    }
}
