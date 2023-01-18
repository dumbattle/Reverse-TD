using UnityEngine;

namespace Core.Campaign {
    public class LevelDefinition_1_2 : ILevelDefinition {
        public ScenarioParameters GetScenarioParameters() {
            var result = new ScenarioParameters();
            result.width = 15;
            result.height = 15;
            result.mainTowerBl = new Vector2Int(7, 7);

            return result;
        }
    }
}