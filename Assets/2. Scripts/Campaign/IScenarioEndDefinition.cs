namespace Core {
    public interface IScenarioEndDefinition {
        bool Check(ScenarioInstance s);
        public IFSM_State<ScenarioInstance> GetEndSequence(ScenarioInstance s);
    }
}
