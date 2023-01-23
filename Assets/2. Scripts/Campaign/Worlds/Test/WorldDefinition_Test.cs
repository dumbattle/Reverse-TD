namespace Core.Campaign {
    public class WorldDefinition_1 : IWorldDefinition {
        ILevelDefinition[] levels = {
            new LevelDefinition_1_1(),
            new LevelDefinition_1_2(),
            new LevelDefinition_1_3(),
        };


        public int NumLevels() {
            return levels.Length;
        }
        public ILevelDefinition GetLevelDefinition(int index) {
            return levels[index];
        }
    }
}