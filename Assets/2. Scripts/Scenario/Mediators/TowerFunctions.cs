using LPE.Triangulation;
using UnityEngine;
using System;
using LPE.Shape2D;

namespace Core {
    public class TowerFunctions {
        TowerManager towerManager;
        TowerPlacementManager placementManager;
        Delaunay delaunay;
        DelaunayPathfinder pathfinder;
        ScenarioInstance s;


        CircleShape cachedCircle = new CircleShape(1);


        public TowerFunctions(ScenarioInstance s, TowerManager towerManager, Delaunay delaunay, DelaunayPathfinder pathfinder, TowerPlacementManager placementManager) {
            this.towerManager = towerManager ?? throw new ArgumentNullException(nameof(towerManager));
            this.delaunay = delaunay ?? throw new ArgumentNullException(nameof(delaunay));
            this.s = s;
            this.pathfinder = pathfinder;
            this.placementManager = placementManager;
        }

        public void AddTowerRandomPlacement(TowerDefinition towerDef) {
            var t = placementManager.SpawnTowerRandom(towerDef);
            if (t == null) {
                return;
            }
            AddTower(t);
        }
        public void AddStartingGroups(TowerDefinition tower, TowerDefinition wall) {
            int area = s.mapQuery.width * s.mapQuery.height;
            int targetCount = area / 50;

            for (int i = 0; i < targetCount; i++) {
                var tile = placementManager.GetRandomGroupSpawnLocation();
                if (tile == new Vector2Int(-1, -1)) {
                    continue;
                }

                var t = tower.GetNewInstance(s, tile);
                AddTower(t);
                placementManager.StartNewGroup(t, false);
            }

            for (int i = 0; i < targetCount; i++) {
                for (int j = 0; j < 5; j++) {
                    var t = placementManager.SpawnTowerRandom(wall);
                    AddTower(t);
                }
            }
        }
        public void AddMainTower(TowerDefinition main, TowerDefinition startingSupport, Vector2Int bl) {
            var mainTower = main.GetNewInstance(s, bl);
          
            var size = main.size;
            var size2 = startingSupport.size;

            var t1 = startingSupport.GetNewInstance(s, bl + new Vector2Int(-1 - size2, -1 - size2));
            var t2 = startingSupport.GetNewInstance(s, bl + new Vector2Int(1 + size, 1 + size));
            var t3 = startingSupport.GetNewInstance(s, bl + new Vector2Int(-1 - size2, 1 + size));
            var t4 = startingSupport.GetNewInstance(s, bl + new Vector2Int(1 + size, -1 - size2));
            AddMainTower(mainTower);
            AddTower(t1);
            AddTower(t2);
            AddTower(t3);
            AddTower(t4);
            placementManager.StartNewGroup(mainTower, true);
            placementManager.StartNewGroup(t1, false);
            placementManager.StartNewGroup(t2, false);
            placementManager.StartNewGroup(t3, false);
            placementManager.StartNewGroup(t4, false);
        }
        
        void AddMainTower(ITower t) {
            AddTower_Helper(t);
            towerManager.target = t;

            Vector2Int bl = t.GetBottomLeft();
            Vector2Int tr = t.GetTopRight();
            towerManager.targetLocation = (Vector2)(bl + tr) / 2f;
        }
        
        void AddTower(ITower t) {
            AddTower_Helper(t);
            // add to triangulation
            var verts = t.GetShape().Vertices();
            for (int i = 1; i < verts.Length; i++) {
                delaunay.AddConstraint(verts[i - 1], verts[i], 0);
            }
            delaunay.AddConstraint(verts[0], verts[verts.Length - 1], 0);

            // clear pathfinding cache
            pathfinder.ClearCache();

            // add to grid
            towerManager.grid.Add(t, t.GetShape().AABB());

        }
        void AddTower_Helper(ITower t) {
            Vector2Int bl = t.GetBottomLeft();
            Vector2Int tr = t.GetTopRight();

            // add to grid
            for (int x = bl.x; x <= tr.x; x++) {
                for (int y = bl.y; y <= tr.y; y++) {
                    towerManager.towers[x, y] = t;
                }
            }

            // add to list
            towerManager.allTowers.Add(t);

        }

        public void UpdateAllTowers() {
            foreach (var t in towerManager.allTowers) {
                t.GameUpdate(s);
            }
        }

        public bool IsCollidingWithMainTower(Vector2 pos, float radius) {
            cachedCircle.radius = radius;
            cachedCircle.SetPosition(pos);

            var mainShape = towerManager.target.GetShape();
            return mainShape.CheckCollision(cachedCircle);
        }
    }
}
