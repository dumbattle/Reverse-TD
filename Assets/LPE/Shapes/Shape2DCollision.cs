﻿using UnityEngine;
using LPE.SpacePartition;
using System.Collections.Generic;


namespace LPE.Shape2D {
    public static class Shape2DCollision {
        const float EPSILON = 0.00001f;
        static CastedShape castedShape = new CastedShape();
        public static bool CheckCollision(Shape2D s1, Shape2D s2) {
            CircleShape c1 = s1 as CircleShape;
            CircleShape c2 = s2 as CircleShape;

            if (c1 != null && c2 != null) {
                return CircleCollision(c1, c2);
            }

            foreach (var axis in s1.CollisionAxes()) {
                var shadow1 = s1.Project(axis);
                var shadow2 = s2.Project(axis);

                if (shadow1.x + EPSILON > shadow2.y || shadow1.y < shadow2.x + EPSILON) {
                    return false;
                }
            }
            foreach (var axis in s2.CollisionAxes()) {
                var shadow1 = s1.Project(axis);
                var shadow2 = s2.Project(axis);

                if (shadow1.x + EPSILON > shadow2.y || shadow1.y < shadow2.x + EPSILON) {
                    return false;
                }
            }

            if (c1 != null) {
                var vert = s2.Vertices();
                Vector2 closest = new Vector2(0, 0);
                float closestDist = -1;

                foreach (var v in vert) {
                    float dist = (v - c1.position).sqrMagnitude;
                    if (dist < closestDist || closestDist < 0) {
                        closestDist = dist;
                        closest = v - c1.position;
                    }
                }
                var axis = closest;

                var shadow1 = s1.Project(axis);
                var shadow2 = s2.Project(axis);

                if (shadow1.x + EPSILON > shadow2.y || shadow1.y < shadow2.x + EPSILON) {
                    return false;
                }
            }
            if (c2 != null) {
                var vert = s1.Vertices();
                Vector2 closest = new Vector2(0, 0);
                float closestDist = -1;

                foreach (var v in vert) {
                    float dist = (v - c2.position).sqrMagnitude;
                    if (dist < closestDist || closestDist < 0) {
                        closestDist = dist;
                        closest = v - c2.position;
                    }
                }

                var axis = closest;

                var shadow1 = s1.Project(axis);
                var shadow2 = s2.Project(axis);

                if (shadow1.x + EPSILON > shadow2.y || shadow1.y < shadow2.x + EPSILON) {
                    return false;
                }
            }

            return true;
        }
        public static Vector2 CheckCollisionWithCorrection(Shape2D s1, Shape2D s2) {
            CircleShape c1 = s1 as CircleShape;
            CircleShape c2 = s2 as CircleShape;

            if (c1 != null && c2 != null) {
                return CircleCollisionWithCorrection(c1, c2);
            }

            Vector2 correctionVector = Vector2.zero;
            float minDist = float.PositiveInfinity;

            foreach (var ax in s1.CollisionAxes()) {
                var axis = ax.normalized;

                var shadow1 = s1.Project(axis);
                var shadow2 = s2.Project(axis);

                if (shadow1.x > shadow2.y || shadow1.y < shadow2.x) {
                    return Vector2.zero;
                }
                else {
                    float rightDist = shadow1.y - shadow2.x;
                    float leftDist = shadow2.y - shadow1.x;

                    if (rightDist > leftDist) {
                        //go left
                        if (leftDist < minDist) {
                            correctionVector = -axis;
                            minDist = leftDist;
                        }
                    }
                    else {
                        // go right
                        if (rightDist < minDist) {
                            correctionVector = axis;
                            minDist = rightDist;
                        }
                    }
                }
            }
            foreach (var ax in s2.CollisionAxes()) {
                var axis = ax.normalized;
                var shadow1 = s1.Project(axis);
                var shadow2 = s2.Project(axis);


                if (shadow1.x > shadow2.y || shadow1.y < shadow2.x) {
                    return Vector2.zero;
                }
                else {
                    float rightDist = shadow1.y - shadow2.x;
                    float leftDist = shadow2.y - shadow1.x;

                    if (rightDist > leftDist) {
                        //go left
                        if (leftDist < minDist) {
                            correctionVector = -axis;
                            minDist = leftDist;
                        }
                    }
                    else {
                        // go right
                        if (rightDist < minDist) {
                            correctionVector = axis;
                            minDist = rightDist;
                        }
                    }
                }
            }

            if (c1 != null) {
                var vert = s2.Vertices();
                Vector2 closest = new Vector2(0, 0);
                float closestDist = -1;

                foreach (var v in vert) {
                    float dist = (v - c1.position).sqrMagnitude;
                    if (dist < closestDist || closestDist < 0) {
                        closestDist = dist;
                        closest = v - c1.position;
                    }
                }
                var axis = closest.normalized;

                var shadow1 = s1.Project(axis);
                var shadow2 = s2.Project(axis);


                if (shadow1.x > shadow2.y || shadow1.y < shadow2.x) {
                    return Vector2.zero;
                }
                else {
                    float rightDist = shadow1.y - shadow2.x;
                    float leftDist = shadow2.y - shadow1.x;

                    if (rightDist > leftDist) {
                        //go left
                        if (leftDist < minDist) {
                            correctionVector = -axis;
                            minDist = leftDist;
                        }
                    }
                    else {
                        // go right
                        if (rightDist < minDist) {
                            correctionVector = axis;
                            minDist = rightDist;
                        }
                    }
                }
            }
            if (c2 != null) {
                var vert = s1.Vertices();
                Vector2 closest = new Vector2(0, 0);
                float closestDist = -1;

                foreach (var v in vert) {
                    float dist = (v - c2.position).sqrMagnitude;
                    if (dist < closestDist || closestDist < 0) {
                        closestDist = dist;
                        closest = v - c2.position;
                    }
                }

                var axis = closest.normalized;
                var shadow1 = s1.Project(axis);
                var shadow2 = s2.Project(axis);


                if (shadow1.x > shadow2.y || shadow1.y < shadow2.x) {
                    return Vector2.zero;
                }
                else {
                    float rightDist = shadow1.y - shadow2.x;
                    float leftDist = shadow2.y - shadow1.x;

                    if (rightDist > leftDist) {
                        //go left
                        if (leftDist < minDist) {
                            correctionVector = -axis;
                            minDist = leftDist;
                        }
                    }
                    else {
                        // go right
                        if (rightDist < minDist) {
                            correctionVector = axis;
                            minDist = rightDist;
                        }
                    }
                }
            }


            return correctionVector.normalized * -minDist;
        }

        static bool CircleCollision(CircleShape c1, CircleShape c2) {
            float minDist = c1.radius + c2.radius;

            float x = c1.position.x - c2.position.x;
            float y = c1.position.y - c2.position.y;

            return x * x + y * y < minDist * minDist;
        }
        static Vector2 CircleCollisionWithCorrection(CircleShape c1, CircleShape c2) {
            float minDist = c1.radius + c2.radius;

            float x = c1.position.x - c2.position.x;
            float y = c1.position.y - c2.position.y;

            float dist = Mathf.Sqrt(x * x + y * y);

            if (dist < minDist) {
                return (c1.position - c2.position).normalized * (minDist - dist);
            }

            return Vector2.zero;
        }

        /// <summary>
        /// -1 if no collision
        /// </summary>
        public static float ShapeCast(Shape2D cast, Shape2D target, Vector2 dir) {
            // Special case: 2 circles
            CircleShape c1 = cast as CircleShape;
            CircleShape c2 = target as CircleShape;

            if (c1 != null && c2 != null) {
                float ccResult = LPE.Math.Geometry.CircleCast_Circle(c1.position, c1.radius * c1.scale, c2.position, c2.radius * c2.scale, dir);
                if (ccResult < 0) {
                    return -1; // no collision
                }
                if (ccResult > 1) {
                    return -1; // collided too far
                }
                return dir.magnitude * ccResult;
            }

            // Normal case
            castedShape.Set(cast, dir);
            float dirLength = dir.magnitude;
            float t = Mathf.Infinity;

            // project each axis
            foreach (var axis in cast.CollisionAxes()) {
                float correction = CheckAxis(axis);
                if (correction < 0) {
                    return correction;
                }
                t = Mathf.Min(t, correction);
            }
            foreach (var axis in target.CollisionAxes()) {
                float correction = CheckAxis(axis);
                if (correction < 0) {
                    return correction;
                }
                t = Mathf.Min(t, correction);
            }

            // another axis for dir
            Vector2 dirAxis = new Vector2(dir.y, -dir.x).normalized;
            float c = CheckAxis(dirAxis);
            if (c < 0) {
                return c;
            }
            t = Mathf.Min(t, c);
            // If one is circle, we need another axis
            if (c1 != null) {
                var vert = target.Vertices();
                Vector2 closest = new Vector2(0, 0);
                float closestDist = -1;

                foreach (var v in vert) {
                    float dist = (v - c1.position).sqrMagnitude;
                    if (dist < closestDist || closestDist < 0) {
                        closestDist = dist;
                        closest = v - c1.position;
                    }
                }
                var axis = closest;

                float correction = CheckAxis(axis);
                if (correction < 0) {
                    return correction;
                }
                t = Mathf.Min(t, correction);
            }
            if (c2 != null) {
                var vert = target.Vertices();
                Vector2 closest = new Vector2(0, 0);
                float closestDist = -1;

                foreach (var v in vert) {
                    float dist = (v - c2.position).sqrMagnitude;
                    if (dist < closestDist || closestDist < 0) {
                        closestDist = dist;
                        closest = v - c2.position;
                    }
                }

                var axis = closest;

                float correction = CheckAxis(axis);
                if (correction < 0) {
                    return correction;
                }
                t = Mathf.Min(t, correction);
            }


            return Mathf.Max(dirLength - t, 0);

            float CheckAxis(Vector2 axis) {
                Vector2 castShadow = castedShape.Project(axis);
                Vector2 targetShadow = target.Project(axis);

                if (castShadow.x > targetShadow.y || castShadow.y < targetShadow.x) {
                    // no collision
                    return -1;
                }

                float dirProjLength = Shape2D.Projection(dir, axis);
                float dist = dirProjLength > 0 ? castShadow.y - targetShadow.x : castShadow.x - targetShadow.y;
                dist *= dirLength / dirProjLength;
                return Mathf.Max(dist, 0);
            }
        }
    }
}
