using UnityEngine;
using System.Collections.Generic;


namespace Core {
    public class BombProjectileBehaviour : ProjectileBehaviour {
        public SpriteRenderer sr;
        public SpriteRenderer xplodeSr;
        public Color xplodeColor;

        Vector2 start;
        Vector2 direction;
        float speed;
        float maxDist;
        float radius;
        int damage;

        float traveled;
        bool active;
        bool hit;


        float splashRadius;
        float splashScale; // damage scale at edge
        
        List<CreepInstance> creepResults = new List<CreepInstance>();

        public override void Init(ScenarioInstance s, Vector2 start, Vector2 direction, float speed, float maxDist, float radius, int damage) {
            this.start = start;
            this.direction = (direction + Random.insideUnitCircle * 0.2f).normalized;
            this.speed = speed;
            this.maxDist = maxDist;
            this.radius = radius;
            this.damage = damage;
            traveled = 0;
            gameObject.SetActive(true);
            active = true;
            sr.transform.localScale = new Vector3(radius, radius, .5f) * 2;
            transform.position = s.mapQuery.TileToWorld(start);
            hit = false;
            var c = sr.color;
            c.a = 1;
            sr.color = c;
            transform.up = direction;
            sr.gameObject.SetActive(true);
            xplodeSr.gameObject.SetActive(false);
        }

        public void InitSplash(float radius, float scale) {
            splashRadius = radius;
            splashScale = scale;
        }
        public override void GameUpdate(ScenarioInstance s) {
            if (!hit) {
                InFlight(s);
            }
            else {
                Explode();
            }
        }

        void Explode() {
            traveled += 2f / 60f;

            var c = xplodeColor;
            c.a = 1 - traveled;
            xplodeSr.color = c;

            if (traveled > 1) {
                active = false;
            }
        }

        void InFlight(ScenarioInstance s) {
            // move
            traveled += speed / 60f;

            var position = start + direction * traveled;
            transform.position = s.mapQuery.TileToWorld(position);

            // check for hit
            var creep = s.creepFunctions.GetNearestCreep(position, radius);
            if (creep != null && (creep.position - position).sqrMagnitude < (radius + creep.radius) * (radius + creep.radius)) {
                hit = true;

                OnHit(s);
                return;
            }


            if (traveled > maxDist) {
                active = false;
                return;
            }
        }
        void OnHit(ScenarioInstance s) {
            creepResults.Clear();
            sr.gameObject.SetActive(false);
            xplodeSr.gameObject.SetActive(true);
            xplodeSr.transform.localScale = new Vector3(splashRadius, splashRadius, 1) * 2;

            var position = start + direction * traveled;
            s.creepFunctions.QueryCreeps(position, splashRadius, creepResults);
            traveled = 0;

            foreach (var c in creepResults) {
                var dist = (c.position - position).magnitude - c.radius - radius;
               
                if (dist > splashRadius) {
                    continue;
                }

                if (dist < 0) {
                    dist = 0;
                }

                float scale = dist / splashRadius;
                scale = Mathf.Lerp(1, splashScale, scale);

                int scaledDamage = (int)(damage * scale);
                if (scaledDamage < 1) {
                    scaledDamage = 1;
                }
                s.creepFunctions.DamageCreep(c, scaledDamage);
            }
        }

        public override bool Active() {
            return active;
        }
    }
}
