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
            tc.SetAvailableUpgrades();
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
    public class LevelDefinition_1_1 : LevelDefinition {
        protected override (int w, int h) MapSize() {
            return (15, 8);
        }
        protected override List<Vector2Int> Walls() {
            var result = new List<Vector2Int>(); 
            result.Add(new Vector2Int(3, 0));
            result.Add(new Vector2Int(4, 0));
            result.Add(new Vector2Int(5, 0));

            result.Add(new Vector2Int(4, 2));
            //result.Add(new Vector2Int(4, 3));

            result.Add(new Vector2Int(3, 5));
            result.Add(new Vector2Int(2, 5));
            result.Add(new Vector2Int(3, 6));

            result.Add(new Vector2Int(6, 4));
            result.Add(new Vector2Int(6, 5));

            result.Add(new Vector2Int(5, 7));
            //result.Add(new Vector2Int(6, 7));
            result.Add(new Vector2Int(7, 7));

            result.Add(new Vector2Int(7, 1));
            result.Add(new Vector2Int(7, 2));
            //result.Add(new Vector2Int(8, 2));
            return result;
        }
        protected override List<(Vector2Int, TowerDefinition)> StartingTowers() {
            var result = new List<(Vector2Int, TowerDefinition)>();

            result.Add((new Vector2Int(4, 3), TowerDefinitionCatalog.gun_1));
            result.Add((new Vector2Int(6, 7), TowerDefinitionCatalog.gun_1));
            result.Add((new Vector2Int(8, 2), TowerDefinitionCatalog.gun_1));

            return result;
        }
        protected override void SetCreepModifier(CreepStatModification s) {
            s.AddCountScale(-1);
            s.AddSpawnRateScale(-1);
        }
        protected override ICreepPathfinder GetCreepPathfinder() {
            return new SetCreepPathfinder(false,
                 new List<Vector2Int>() {
                    new Vector2Int(1, 3),
                    new Vector2Int(2, 3),
                    new Vector2Int(3, 3),
                    new Vector2Int(3, 2),
                    new Vector2Int(3, 1),
                    new Vector2Int(4, 1),
                    new Vector2Int(5, 1),
                    new Vector2Int(5, 2),
                    new Vector2Int(5, 3),
                    new Vector2Int(5, 4),
                    new Vector2Int(5, 5),
                    new Vector2Int(5, 6),
                    new Vector2Int(6, 6),
                    new Vector2Int(7, 6),
                    new Vector2Int(7, 5),
                    new Vector2Int(7, 4),
                    new Vector2Int(8, 4),
                    new Vector2Int(9, 4),
                    new Vector2Int(10, 4),
                 }
             );
        }
        protected override (int initial, int perRoundFlat, int perRoundScale) GetTowerMoneyParams() {
            return (0, 85, 15);
        }
        protected override List<MainTowerParameters> GetMainTowerParameters() {
            return new List<MainTowerParameters>() {
                new MainTowerParameters(TowerDefinitionCatalog.main_basic_diamond_blue, 10_000, new Vector2Int(10, 4))
            };
        }
    }
}