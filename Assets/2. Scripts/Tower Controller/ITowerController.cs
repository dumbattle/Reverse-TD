using System.Collections.Generic;


namespace Core {
    public interface ITowerController {
        void Init(ScenarioInstance s);
        void OnRoundEnd(ScenarioInstance s);
        void OnCreepReachMainTower(ScenarioInstance s, CreepInstance c, IMainTower mainTower);
        List<IMainTower> GetAllMainTowers();

        bool IsDefeated();
    }
}
