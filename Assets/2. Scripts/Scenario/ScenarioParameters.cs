using System;
using System.Collections.Generic;
using UnityEngine;


namespace Core {
    [Serializable]
    public class ScenarioParameters {
        public int width;
        public int height;

        public Vector2Int mainTowerBl;

        public List<Vector2Int> walls = new List<Vector2Int>();
        public List<(Vector2Int, TowerDefinition)> startingTowers = new List<(Vector2Int, TowerDefinition)>();

        public ICreepPathfinder creepPathfinder = new StochasticCreepPathfinder();
        public TowerDefinition wallTower = TowerDefinitionCatalog.wall1;
        public ITowerController towerController = new TestTowerController();
        public CreepStatModification creepModifiers = new CreepStatModification();
    }
}
