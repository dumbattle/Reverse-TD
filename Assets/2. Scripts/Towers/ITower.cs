using UnityEngine;
using LPE.Shape2D;

namespace Core {
    public interface ITower {
        int Size { get; }
        Vector2Int GetBottomLeft();
        Vector2Int GetTopRight();
        void GameUpdate(ScenarioInstance s);
        Shape2D GetShape();
    }
}
