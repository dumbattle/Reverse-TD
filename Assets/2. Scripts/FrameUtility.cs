using UnityEngine;


namespace Core {
    public static class FrameUtility {
        static FPSMode fpsMode = FPSMode._60;
        public static GameplaySpeed gpSpeed = GameplaySpeed.x1;
        static bool inGameLoop;

        public static void SetFrameRate(FPSMode mode) {
            fpsMode = mode;
            switch (fpsMode) {
                case FPSMode._60:
                    Application.targetFrameRate = 60;
                    break;
                case FPSMode._30:
                    Application.targetFrameRate = 30;
                    break;
            }
        }
        public static float GetSimulationScale() {
            return (float)gpSpeed.SimulationLoopIterationCount() / GameplaySpeed.x1.SimulationLoopIterationCount();
        }
        public static float DeltaTime(bool scaleWithGameplaySpeed) {
            float result = 1f / 60f;
            result *= GetFrameMultiplier(scaleWithGameplaySpeed);
            return result;
        }

        public static float GetFrameMultiplier(bool scaleWithGameplaySpeed) {
            float result = 1;
            if (fpsMode == FPSMode._30) {
                result *= 2;
            }

            if (scaleWithGameplaySpeed) {
                result *= (float)gpSpeed.SimulationLoopIterationCount() / GameplaySpeed.x1.SimulationLoopIterationCount();
            }

            if (inGameLoop) {
                result /= GameplaySpeed.x1.SimulationLoopIterationCount();
            }

            return result;
        }

        public static GameLoopContex GetGameLoopContex() {
            inGameLoop = true;
            return new GameLoopContex();
        }

        public struct GameLoopContex : System.IDisposable {
            public void Dispose() {
                inGameLoop = false;
            }
        }
    }
}
