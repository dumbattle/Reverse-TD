using LPE.Triangulation;
using UnityEngine;
using System;
using LPE.Shape2D;

namespace Core {
    public class TowerFunctions {
        TowerManager towerManager;
        Delaunay delaunay;
        DelaunayPathfinder pathfinder;
        ScenarioInstance s;


        CircleShape cachedCircle = new CircleShape(1);


        public TowerFunctions(ScenarioInstance s, TowerManager towerManager, Delaunay delaunay, DelaunayPathfinder pathfinder) {
            this.towerManager = towerManager ?? throw new ArgumentNullException(nameof(towerManager));
            this.delaunay = delaunay ?? throw new ArgumentNullException(nameof(delaunay));
            this.s = s;
            this.pathfinder = pathfinder;
        }
       
        public void AddMainTower(ITower t) {
            AddTower_Helper(t);
            towerManager.target = t;

            Vector2Int bl = t.GetBottomLeft();
            Vector2Int tr = t.GetTopRight();
            towerManager.targetLocation = (Vector2)(bl + tr) / 2f;
        }
        public void AddTower(ITower t) {
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
                for (int y = bl.x; y <= tr.y; y++) {
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
