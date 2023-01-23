using UnityEngine;
using LPE.Shape2D;
using System.Collections.Generic;

namespace Core {
    public interface ITower {
        int Size { get; }
        Vector2Int GetBottomLeft();
        Vector2Int GetTopRight();
        void GameUpdate(ScenarioInstance s);
        Shape2D GetShape();

        void EndRound();
        void GetGeneralUpgradeOptions(List<UpgradeOption> results);
        void GetSpecializationUpgradeOptions(ScenarioInstance s, List<SpecializationUpgradeOptions> results);
        void Refresh();

        void Destroy();
        List<TowerUpgradeDetails> GetGeneralUpgrades();
        void TransferGeneralUpgrade(TowerUpgradeDetails d);
        void OnBeforeUpgrade(ScenarioInstance s);
        int GetTotalUpgradeLevel();
    }
}
