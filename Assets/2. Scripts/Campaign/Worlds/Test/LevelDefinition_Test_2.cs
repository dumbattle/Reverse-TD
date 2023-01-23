using UnityEngine;
using System.Collections.Generic;

namespace Core.Campaign {
    public class LevelDefinition_1_2 : LevelDefinition {

        protected override (int w, int h) MapSize() {
            return (13, 9);
        }
       
        protected override void SetCreepModifier(CreepStatModification s) {
            //s.AddCountScale(-0.5f);
            //s.AddSpawnRateScale(-0.5f);
        }
        
        protected override (int initial, int perRoundFlat, int perRoundScale) GetTowerMoneyParams() {
            return (0, 145, 15);
        }

        protected override List<MainTowerParameters> GetMainTowerParameters() {
            return new List<MainTowerParameters>() {
                new MainTowerParameters(TowerDefinitionCatalog.main_basic_diamond_blue, 20_000, new Vector2Int(6, 4))
            };
        }

        protected override List<Vector2Int> Walls() {
            var result = new List<Vector2Int>();
            result.Add(new Vector2Int(4, 0));
            result.Add(new Vector2Int(12, 0));
            result.Add(new Vector2Int(2, 1));
            result.Add(new Vector2Int(3, 1));
            result.Add(new Vector2Int(4, 1));
            result.Add(new Vector2Int(5, 1));
            result.Add(new Vector2Int(7, 1));
            result.Add(new Vector2Int(8, 1));
            result.Add(new Vector2Int(9, 1));
            result.Add(new Vector2Int(10, 1));
            result.Add(new Vector2Int(6, 3));
            result.Add(new Vector2Int(7, 3));
            result.Add(new Vector2Int(10, 3));
            result.Add(new Vector2Int(2, 5));
            result.Add(new Vector2Int(5, 5));
            result.Add(new Vector2Int(6, 5));
            result.Add(new Vector2Int(7, 5));
            result.Add(new Vector2Int(12, 5));
            result.Add(new Vector2Int(2, 7));
            result.Add(new Vector2Int(4, 7));
            result.Add(new Vector2Int(5, 7));
            result.Add(new Vector2Int(6, 7));
            result.Add(new Vector2Int(9, 7));
            result.Add(new Vector2Int(10, 7));
            result.Add(new Vector2Int(2, 8));
            result.Add(new Vector2Int(8, 8));
            result.Add(new Vector2Int(12, 8));
            return result;
        }
       
        protected override List<(Vector2Int, TowerDefinition)> StartingTowers() {
            var result = new List<(Vector2Int, TowerDefinition)>();

            result.Add((new Vector2Int(4, 3), TowerDefinitionCatalog.gun_1));
            result.Add((new Vector2Int(8, 3), TowerDefinitionCatalog.gun_1));
            result.Add((new Vector2Int(1, 5), TowerDefinitionCatalog.gun_1));
            result.Add((new Vector2Int(9, 5), TowerDefinitionCatalog.gun_1));
            result.Add((new Vector2Int(12, 6), TowerDefinitionCatalog.gun_1));

            return result;
        }

        protected override ICreepPathfinder GetCreepPathfinder() {
            return new SetCreepPathfinder(false,
                 new List<Vector2Int>() {
                    new Vector2Int(11, 1),
                    new Vector2Int(11, 2),
                    new Vector2Int(10, 2),
                    new Vector2Int(9, 2),
                    new Vector2Int(8, 2),
                    new Vector2Int(7, 2),
                    new Vector2Int(6, 2),
                    new Vector2Int(5, 2),
                    new Vector2Int(4, 2),
                    new Vector2Int(3, 2),
                    new Vector2Int(2, 2),
                    new Vector2Int(1, 2),
                    new Vector2Int(1, 3),
                    new Vector2Int(1, 4),
                    new Vector2Int(2, 4),
                    new Vector2Int(3, 4),
                    new Vector2Int(4, 4),
                    new Vector2Int(5, 4),
                    new Vector2Int(6, 4)
                 },
                 new List<Vector2Int>() {
                    new Vector2Int(1, 7),
                    new Vector2Int(1, 6),
                    new Vector2Int(2, 6),
                    new Vector2Int(3, 6),
                    new Vector2Int(4, 6),
                    new Vector2Int(5, 6),
                    new Vector2Int(6, 6),
                    new Vector2Int(7, 6),
                    new Vector2Int(8, 6),
                    new Vector2Int(9, 6),
                    new Vector2Int(10, 6),
                    new Vector2Int(11, 6),
                    new Vector2Int(11, 5),
                    new Vector2Int(11, 4),
                    new Vector2Int(10, 4),
                    new Vector2Int(9, 4),
                    new Vector2Int(8, 4),
                    new Vector2Int(7, 4),
                    new Vector2Int(6    , 4),
                 }
             );
        }

    }
    public class LevelDefinition_1_3 : LevelDefinition {
        protected override (int w, int h) MapSize() {
            return (11, 10);
        }

        protected override void SetCreepModifier(CreepStatModification s) {
            //s.AddCountScale(-0.5f);
            //s.AddSpawnRateScale(-0.5f);
        }

        protected override (int initial, int perRoundFlat, int perRoundScale) GetTowerMoneyParams() {
            return (0, 145, 15);
        }
       
        protected override List<MainTowerParameters> GetMainTowerParameters() {
            return new List<MainTowerParameters>() {
                new MainTowerParameters(TowerDefinitionCatalog.main_basic_diamond_blue, 10_000, new Vector2Int(5, 4)),
                new MainTowerParameters(TowerDefinitionCatalog.main_basic_diamond_blue, 10_000, new Vector2Int(7, 3)),
            };
        }

        protected override List<Vector2Int> Walls() {
            var result = new List<Vector2Int>();
            result.Add(new Vector2Int(1, 0));
            result.Add(new Vector2Int(2, 0));
            result.Add(new Vector2Int(6, 0));
            result.Add(new Vector2Int(7, 0));
            result.Add(new Vector2Int(9, 0));
            result.Add(new Vector2Int(6, 1));
            result.Add(new Vector2Int(4, 2));
            result.Add(new Vector2Int(6, 3));
            result.Add(new Vector2Int(10, 3));
            result.Add(new Vector2Int(2, 4));
            result.Add(new Vector2Int(6, 4));
            result.Add(new Vector2Int(8, 4));
            result.Add(new Vector2Int(10, 4));
            result.Add(new Vector2Int(0, 5));
            result.Add(new Vector2Int(2, 5));
            result.Add(new Vector2Int(6, 5));
            result.Add(new Vector2Int(10, 5));
            result.Add(new Vector2Int(0, 6));
            result.Add(new Vector2Int(3, 7));
            result.Add(new Vector2Int(4, 7));
            result.Add(new Vector2Int(7, 7));
            result.Add(new Vector2Int(2, 9));
            result.Add(new Vector2Int(3, 9));
            result.Add(new Vector2Int(6, 9));

            return result;
        }

        protected override List<(Vector2Int, TowerDefinition)> StartingTowers() {
            var result = new List<(Vector2Int, TowerDefinition)>();

            result.Add((new Vector2Int(2, 2), TowerDefinitionCatalog.gun_1));
            result.Add((new Vector2Int(8, 2), TowerDefinitionCatalog.gun_1));
            result.Add((new Vector2Int(5, 5), TowerDefinitionCatalog.gun_1));
            result.Add((new Vector2Int(2, 7), TowerDefinitionCatalog.gun_1));
            result.Add((new Vector2Int(8, 7), TowerDefinitionCatalog.gun_1));

            return result;
        }
       
        protected override ICreepPathfinder GetCreepPathfinder() {
            return new SetCreepPathfinder(false,
                 new List<Vector2Int>() {
                    new Vector2Int(3, 3),
                    new Vector2Int(3, 4),
                    new Vector2Int(3, 5),
                    new Vector2Int(3, 6),
                    new Vector2Int(4, 6),
                    new Vector2Int(5, 6),
                    new Vector2Int(6, 6),
                    new Vector2Int(7, 6),
                    new Vector2Int(7, 5),
                    new Vector2Int(7, 4),
                    new Vector2Int(7, 3),
                 },
                 new List<Vector2Int>() {
                    new Vector2Int(7, 1),
                    new Vector2Int(8, 1),
                    new Vector2Int(9, 1),
                    new Vector2Int(9, 2),
                    new Vector2Int(9, 3),
                    new Vector2Int(9, 4),
                    new Vector2Int(9, 5),
                    new Vector2Int(9, 6),
                    new Vector2Int(9, 7),
                    new Vector2Int(9, 8),
                    new Vector2Int(8, 8),
                    new Vector2Int(7, 8),
                    new Vector2Int(6, 8),
                    new Vector2Int(5, 8),
                    new Vector2Int(4, 8),
                    new Vector2Int(3, 8),
                    new Vector2Int(2, 8),
                    new Vector2Int(1, 8),
                    new Vector2Int(1, 7),
                    new Vector2Int(1, 6),
                    new Vector2Int(1, 5),
                    new Vector2Int(1, 4),
                    new Vector2Int(1, 3),
                    new Vector2Int(1, 2),
                    new Vector2Int(1, 1),
                    new Vector2Int(2, 1),
                    new Vector2Int(3, 1),
                    new Vector2Int(4, 1),
                    new Vector2Int(5, 1),
                    new Vector2Int(5, 2),
                    new Vector2Int(5, 3),
                    new Vector2Int(5, 4),
                 }
             );
        }

    }
}