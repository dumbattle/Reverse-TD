namespace Core.Campaign {
    public interface IWorldDefinition {
        int NumLevels();
        ILevelDefinition GetLevelDefinition(int index);
    }
}