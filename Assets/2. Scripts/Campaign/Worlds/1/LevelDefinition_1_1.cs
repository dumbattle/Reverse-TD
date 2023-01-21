using UnityEngine;
using System.Collections.Generic;



namespace Core.Campaign {
    public class LevelDefinition_1_1 : ILevelDefinition {
        public ScenarioParameters GetScenarioParameters() {
            var result = new ScenarioParameters();
            result.width = 15;
            result.height = 8;
            result.mainTowerBl = new Vector2Int(10, 4);
            result.mainTowerDef = TowerDefinitionCatalog.main_basic_diamond_blue;
            result.walls.Add(new Vector2Int(3, 0));
            result.walls.Add(new Vector2Int(4, 0));
            result.walls.Add(new Vector2Int(5, 0));

            result.walls.Add(new Vector2Int(4, 2));
            //result.walls.Add(new Vector2Int(4, 3));

            result.walls.Add(new Vector2Int(3, 5));
            result.walls.Add(new Vector2Int(2, 5));
            result.walls.Add(new Vector2Int(3, 6));

            result.walls.Add(new Vector2Int(6, 4));
            result.walls.Add(new Vector2Int(6, 5));

            result.walls.Add(new Vector2Int(5, 7));
            //result.walls.Add(new Vector2Int(6, 7));
            result.walls.Add(new Vector2Int(7, 7));

            result.walls.Add(new Vector2Int(7, 1));
            result.walls.Add(new Vector2Int(7, 2));
            //result.walls.Add(new Vector2Int(8, 2));


            result.startingTowers.Add((new Vector2Int(4, 3), TowerDefinitionCatalog.gun_1));
            result.startingTowers.Add((new Vector2Int(6, 7), TowerDefinitionCatalog.gun_1));
            result.startingTowers.Add((new Vector2Int(8, 2), TowerDefinitionCatalog.gun_1));

            result.creepModifiers.AddCountScale(-1);
            result.creepModifiers.AddSpawnRateScale(-1);

            result.creepPathfinder = new SetCreepPathfinder(false,
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

            var tc = new CampaignTowerController();
            tc.SetAvailableUpgrades();
            tc.SetMoneyStats(0, 85, 15);
            tc.SetMaxHealth(1_000);
            result.towerController = tc;

            result.endDefinitions.Add(new CampaignWin_ScenarioEndDefinition(tc));
            return result;
        }
    }
}