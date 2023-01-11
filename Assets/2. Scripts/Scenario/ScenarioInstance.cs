using LPE.Triangulation;
using UnityEngine;


namespace Core {
    public class ScenarioInstance {
        //******************************************************************************
        // Pooling
        //******************************************************************************

        ScenarioInstance() { }

        public static ScenarioInstance Get(ScenarioParameters p) {
            var result = new ScenarioInstance();
            result.parameters = p;
            result.map = new ScenarioMap(p.width, p.height);
            result.towerManager = new TowerManager(p.width, p.height);
            result.creepManager = new CreepManager(p.width, p.height);
            result.placementManager = new TowerPlacementManager(result, result.towerManager);


            result.mapQuery = new MapQuery(result.map, result.towerManager);
            result.towerFunctions = new TowerFunctions(result, result.towerManager, result.placementManager);
            result.creepFunctions = new CreepFunctions(result, result.creepManager);
            result.playerFunctions = new PlayerFunctions(result.parameters, result.player);
            return result;
        }

        //******************************************************************************
        // Data
        //******************************************************************************
        
        public ScenarioParameters parameters { get; private set; }
        PlayerData player = new PlayerData();

        TowerManager towerManager;
        TowerPlacementManager placementManager;
        ScenarioMap map;
        CreepManager creepManager;

        //******************************************************************************
        // Mediators
        //******************************************************************************

        public MapQuery mapQuery;
        public TowerFunctions towerFunctions;
        public CreepFunctions creepFunctions;
        public PlayerFunctions playerFunctions;
        public RoundManager roundManager = new RoundManager();

        //******************************************************************************
        // Helpers
        //******************************************************************************

        public void DrawGizmos() {
            var offset = mapQuery.TileToWorld(0, 0);
            Gizmos.color = Color.blue;
        }
    }

    public class TowerSet {

    }
}
