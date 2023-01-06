﻿using LPE.Triangulation;
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
            result.creepArmy = new CreepArmy();


            result.delaunay = new Delaunay();
            result.delaunay.AddPoint(new Vector2(-5.5f, -0.5f));
            result.delaunay.AddPoint(new Vector2(p.width + 5.5f, -5.5f));
            result.delaunay.AddPoint(new Vector2(-5.5f, p.height + 5.5f));
            result.delaunay.AddPoint(new Vector2(p.width + 5.5f, p.height + 5.5f));
            result.pathfinder = new DelaunayPathfinder();



            result.pathfinder.Set(result.delaunay);
            result.mapQuery = new MapQuery(result.map);
            result.towerFunctions = new TowerFunctions(result, result.towerManager, result.delaunay, result.pathfinder);
            result.creepFunctions = new CreepFunctions(result, result.creepManager);
            result.creepPathfinder = new CreepPathfinder(result.creepManager, result.towerManager, result.pathfinder);
            return result;
        }

        //******************************************************************************
        // Data
        //******************************************************************************
        
        public ScenarioParameters parameters { get; private set; }
        public CreepArmy creepArmy;

        TowerManager towerManager;
        ScenarioMap map;
        Delaunay delaunay;
        DelaunayPathfinder pathfinder;
        CreepManager creepManager;

        //******************************************************************************
        // Mediators
        //******************************************************************************

        public MapQuery mapQuery;
        public TowerFunctions towerFunctions;
        public CreepFunctions creepFunctions;
        public CreepPathfinder creepPathfinder;

        //******************************************************************************
        // Helpers
        //******************************************************************************

        public void DrawGizmos() {
            var offset = mapQuery.TileToWorld(0, 0);
            Gizmos.color = Color.blue;
            delaunay.DrawGizmos(offset);
        }
    }
}
