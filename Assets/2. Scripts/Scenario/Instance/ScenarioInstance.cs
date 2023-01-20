using LPE.Triangulation;
using UnityEngine;


namespace Core {
    public class ScenarioInstance {
        //******************************************************************************
        // Pooling
        //******************************************************************************

        ScenarioInstance() { }

        public static ScenarioInstance Get(ScenarioParameters p, ScenarioUnityReferences references) {
            var result = new ScenarioInstance();
            result.parameters = p;
            result.references = references;
            result.map = new ScenarioMap(p.width, p.height);
            result.towerManager = new TowerManager(p.width, p.height);
            result.creepManager = new CreepManager(result, p.width, p.height);
            result.player = new PlayerData();
            result.mapQuery = new MapQuery(result.map, result.towerManager);
            result.towerFunctions = new TowerFunctions(result, result.towerManager);
            result.creepFunctions = new CreepFunctions(result, result.creepManager);
            result.playerFunctions = new PlayerFunctions(references, result.player);
            result.player.Init(result);
            return result;
        }

        //******************************************************************************
        // Data
        //******************************************************************************
        
        public ScenarioParameters parameters { get; private set; }
        public ScenarioUnityReferences references { get; private set; }
        public RoundManager roundManager = new RoundManager();
        PlayerData player;

        TowerManager towerManager;
        ScenarioMap map;
        CreepManager creepManager;


        //******************************************************************************
        // Mediators
        //******************************************************************************

        public MapQuery mapQuery;
        public TowerFunctions towerFunctions;
        public CreepFunctions creepFunctions;
        public PlayerFunctions playerFunctions;

        //******************************************************************************
        // Helpers
        //******************************************************************************

        public void DrawGizmos() {
            var offset = mapQuery.TileToWorld(0, 0);
            Gizmos.color = Color.blue;
        }
    }
}
