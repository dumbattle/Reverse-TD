namespace Core.Campaign {
    public static class WorldCollection {
        static IWorldDefinition[] worlds = { 
            new WorldDefinition_1(),
        };

        public static int NumWorld() {
            return worlds.Length;
        }

        public static IWorldDefinition GetWorld(int index) {
            return worlds[index];
        }
    }
}