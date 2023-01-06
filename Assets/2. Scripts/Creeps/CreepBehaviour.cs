using UnityEngine;

namespace Core {
    public class CreepBehaviour : MonoBehaviour {
        public SpriteRenderer sr;
        public SpriteRenderer hpBar;

        CreepInstance c;
        ScenarioInstance s;
        
        public void AssignCreep(ScenarioInstance s, CreepInstance c) {
            this.c = c;
            this.s = s;
            gameObject.SetActive(true);
            sr.sprite = c.definition.sprite;
            sr.transform.localScale = new Vector3(c.radius * 2, c.radius * 2, 1);
            transform.position = s.mapQuery.TileToWorld(c.position);
        }

        void Update() {
            sr.transform.up = c.direction;
            transform.position = s.mapQuery.TileToWorld(c.position);

            var hpRatio = (float)c.health.current / c.health.max;
            hpRatio = Mathf.Clamp01(hpRatio);

            var scale = hpBar.transform.localScale;
            scale.x = hpRatio;
            hpBar.transform.localScale = scale;


            Color col;
            if (hpRatio < 0.5f) {
                col = Color.Lerp(Color.red, Color.yellow, hpRatio * 2);
            }
            else {
                col = Color.Lerp(Color.yellow, Color.green, 2 * hpRatio - 1);
            }
            hpBar.color = col;

        }
    }
}
