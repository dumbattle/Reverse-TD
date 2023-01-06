using System.Collections.Generic;
using UnityEngine;
using LPE.Math;


namespace LPE.Steering {
    public static class Steering {
        public static Vector2 Basic<T>(Vector2 target, T agent, List<T> nearby) where T : ISteerAgent {
            var pos = agent.position;

            var dir = (target - pos);

            // lerp for smooth-ish rotation
            if (agent.direction != Vector2.zero) {
                dir = Vector2.Lerp(dir.normalized, agent.direction.normalized, .6f);
            }

            dir =
                dir.normalized * 3 +
                Seperation(agent, nearby, 2f) +
                Seperation(agent, nearby, .8f);

            return dir.normalized;
        }

        static Vector2 Seperation<T>(T agent, List<T> nearby, float sepScale) where T : ISteerAgent {
            Vector2 result = new Vector2();
            foreach (var other in nearby) {
                if (EqualityComparer<T>.Default.Equals(other, agent)) {
                    continue;
                }
                var dir = other.position - agent.position;
                var sep = Mathf.Max(agent.radius, other.radius) * sepScale;
                float minRad = sep + Mathf.Min(agent.radius, other.radius);



                var dist = dir.magnitude;
                var scale = (dist) / minRad;
                //too far
                if (scale > 1) {
                    continue;
                }

                if (Mathf.Approximately(scale, 0)) {
                    // on same spot -> rand direction
                    dir = Random.insideUnitCircle;
                }
                dir = dir.normalized;
                scale = Mathf.Lerp(1, 0, scale * scale);
                // correction
                var cv = dir * scale;
                result -= cv;


            }
            return result;
        }
    }



    public interface ISteerAgent {
        Vector2 position { get; set;  }
        Vector2 direction { get; set; }
        float radius { get; }
    }


}
