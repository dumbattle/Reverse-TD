using LPE.Triangulation;
using UnityEngine;
using System.Collections.Generic;
using LPE.Shape2D;
using LPE.Steering;


namespace Core {
    public class CreepPathfinder {
        DelaunayPathfinder pathfinder;
        TowerManager towerManager;
        CreepManager creepManager;


        List<DelaunayTriangle> triangleResult = new List<DelaunayTriangle>();
        List<Vector2> pathResult = new List<Vector2>();
        CircleShape cachedCircleShape = new CircleShape(1);
        List<ITower> towerResults = new List<ITower>();
        List<CreepInstance> creepResults = new List<CreepInstance>();


        public CreepPathfinder(CreepManager creepManager, TowerManager towerManager, DelaunayPathfinder pathfinder) {
            this.creepManager = creepManager;
            this.towerManager = towerManager;
            this.pathfinder = pathfinder;
        }

        public Vector2 GetDirection(CreepInstance c) {
            triangleResult.Clear();
            pathResult.Clear();
            pathfinder.AStar(c.position, towerManager.targetLocation, 1, triangleResult, c.radius);
            DelaunayAlgorithms.Funnel(triangleResult, c.position, towerManager.targetLocation, pathResult, c.radius);
            var dest = pathResult[1];
            return (dest - c.position).normalized;
        }

        public Vector2 SteerCreep(CreepInstance c, Vector2 dir) {
            creepResults.Clear();

            cachedCircleShape.SetPosition(c.position);
            cachedCircleShape.SetScale(c.radius * 2);
            creepManager.grid.QueryItems(cachedCircleShape.AABB(), creepResults);
            
            return Steering.Basic(c.position + dir, c, creepResults);
        }

        public Vector2 GetUnitMoveDestination(Vector2 position, Vector2 movement, float radius, CreepInstance c) {
            cachedCircleShape.SetScale(radius, false);
            // set destination
            cachedCircleShape.SetPosition(position + movement);

            // get collisions
            towerResults.Clear();
            towerManager.grid.QueryItems(cachedCircleShape.AABB(), towerResults);


            // apply correction
            Vector2 correction = Vector2.zero;
            int count = 0;
           
            foreach (var b in towerResults) {
                var cor = b.GetShape().CheckCollisionWithCorrection(cachedCircleShape);
                correction += cor;
                if (cor != Vector2.zero) {
                    count++;
                }
            }


            if (count > 0) {
                correction /= count;
            }


            // recheck collisions
            cachedCircleShape.SetPosition(position + movement - correction);

            towerResults.Clear();
            towerManager.grid.QueryItems(cachedCircleShape.AABB(), towerResults);


            bool valid = true;

            foreach (var b in towerResults) {
                valid = !b.GetShape().CheckCollision(cachedCircleShape);
                if (!valid) {
                    break;
                }
            }
            if (valid) {
                return cachedCircleShape.position;
            }

            // shape cast
            cachedCircleShape.SetPosition(position);
            float t = 1;


            towerResults.Clear();
            towerManager.grid.QueryItems(cachedCircleShape.AABB(), towerResults);


            foreach (var b in towerResults) {
                float dist = Shape2DCollision.ShapeCast(cachedCircleShape, b.GetShape(), movement);
                if (dist > 0) {
                    t = Mathf.Min(t, dist);
                }
            }
            return position + movement * t * 0.99f;
        }
    }

}
