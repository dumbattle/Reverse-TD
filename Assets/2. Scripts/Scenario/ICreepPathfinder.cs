using UnityEngine;
using System.Collections.Generic;


namespace Core {
    public interface ICreepPathfinder {
        List<Vector2Int> GetPath(ScenarioInstance s);
        void DrawBehaviours(ScenarioInstance s);
    }
}
