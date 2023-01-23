using UnityEngine;
using System.Collections.Generic;



namespace Core.Campaign {
    public abstract class LevelDefinition : ILevelDefinition {
        public ScenarioParameters GetScenarioParameters() {
            var result = new ScenarioParameters();
            var (w, h) = MapSize();
            result.width = w;
            result.height = h;
            result.mainTowers = GetMainTowerParameters();
            result.walls = Walls();

            result.startingTowers = StartingTowers();
            SetCreepModifier(result.creepModifiers);

            result.creepPathfinder = GetCreepPathfinder();
            var tc = new CampaignTowerController();
            tc.SetAvailableUpgrades(); // TODO - Abstract
            var m = GetTowerMoneyParams();
            tc.SetMoneyStats(m.initial, m.perRoundFlat, m.perRoundScale);
            result.towerController = tc;

            result.endDefinitions.Add(new CampaignWin_ScenarioEndDefinition(tc));
            return result;
        }

        protected abstract (int w, int h) MapSize();

        protected abstract List<MainTowerParameters> GetMainTowerParameters();

        protected abstract List<Vector2Int> Walls();
        protected abstract List<(Vector2Int, TowerDefinition)> StartingTowers();
        protected abstract void SetCreepModifier(CreepStatModification s);
        protected abstract ICreepPathfinder GetCreepPathfinder();
        protected abstract (int initial, int perRoundFlat, int perRoundScale) GetTowerMoneyParams();
    }
}