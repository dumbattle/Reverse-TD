using System.Collections.Generic;


namespace Core {
    public interface ITowerController {
        void Init(ScenarioInstance s);
        void OnRoundEnd(ScenarioInstance s);
        float OnCreepReachMainTower(ScenarioInstance s, CreepInstance c, IMainTower mainTower);
        List<IMainTower> GetAllMainTowers();
    }
}
