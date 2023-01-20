namespace Core {
    public static class GameplaySpeedExtensions {
        public static int SimulationLoopIterationCount(this GameplaySpeed s) {
            switch (s) {
                case GameplaySpeed.x0_5:
                    return 1;
                case GameplaySpeed.x1:
                    return 2;
                case GameplaySpeed.x2:
                    return 4;
                case GameplaySpeed.x4:
                    return 8;
                case GameplaySpeed.x8:
                    return 16;
            }
            return 1;
        }
    }
}
