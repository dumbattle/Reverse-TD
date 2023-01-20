using UnityEngine;

namespace Core {


    public class TestProjectileBehaviour : ProjectileBehaviour {
        public SpriteRenderer sr;

        Vector2 start; 
        Vector2 direction; 
        float speed; 
        float maxDist;
        float radius;
        int damage;

        float traveled;
        bool active;
        bool hit;


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
            transform.localScale = new Vector3(radius, radius, .5f) * 2;
            transform.position = s.mapQuery.TileToWorld(start);
            hit = false;
            var c = sr.color;
            c.a = 1;
            sr.color = c;
            transform.up = direction;
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
            traveled += 2f * FrameUtility.DeltaTime(true);
            transform.localScale = new Vector3(radius * (1 + traveled), radius * (1 + traveled), .5f) * 2;

            var c = sr.color;
            c.a = 1 - traveled;
            sr.color = c;
            if (traveled > 1) {
                active = false;
            }
        }

        private void InFlight(ScenarioInstance s) {
            // move
            traveled += speed * FrameUtility.DeltaTime(true);

            var position = start + direction * traveled;
            transform.position = s.mapQuery.TileToWorld(position);

            // check for hit
            var creep = s.creepFunctions.GetNearestCreep(position, radius);
            if (creep != null && (creep.position - position).sqrMagnitude < (radius + creep.radius) * (radius + creep.radius)) {
                s.creepFunctions.DamageCreep(creep, damage);
                hit = true;
                traveled = 0;
                return;
            }


            if (traveled > maxDist) {
                active = false;
                return;
            }
        }

        public override bool Active() {
            return active;
        }
    }
}
