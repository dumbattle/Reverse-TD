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
        void GetUpgradeOptions(List<UpgradeOption> results);

        void Refresh();
    }

}
