using UnityEngine;
using LPE;


namespace Core {
    public class CreepBehaviour : MonoBehaviour {
        public SpriteRenderer sr;
        public SpriteRenderer glowSr;
        public SpriteRenderer hpBar;
        public ParticleSystem deathParticles;

        CreepInstance c;
        ScenarioInstance s;

        ObjectPool<CreepBehaviour> parentPool;
       
        
        public void AssignCreep(ScenarioInstance s, CreepInstance c, ObjectPool<CreepBehaviour> parentPool) {
            this.c = c;
            this.s = s;
            gameObject.SetActive(true);
            sr.sprite = c.GetSprite();
            sr.transform.localScale = new Vector3(c.radius * 2, c.radius * 2, 1);
            transform.position = s.mapQuery.TileToWorld(c.position);
            glowSr.color = c.GetGlowColor();
            this.parentPool = parentPool;
            sr.enabled = true;
            glowSr.enabled = true;
            hpBar.enabled = true;
        }

        void Update() {
            if (c == null) {
                if (!deathParticles.isPlaying) {
                    parentPool.Return(this);
                    gameObject.SetActive(false);
                }
                else {
                    // set simulation speed
                    var speedScale = FrameUtility.GetSimulationScale();
                    var main = deathParticles.main;
                    if (main.simulationSpeed != speedScale) {
                        main.simulationSpeed = speedScale;
                    }
                }
                return;
            }
          
            // update transform
            sr.transform.up = c.direction;
            transform.position = s.mapQuery.TileToWorld(c.position);
            sr.transform.localScale = new Vector3(c.radius * 2, c.radius * 2, 1);


            // update HP bar scale
            var hpRatio = (float)c.health.current / c.health.max;
            hpRatio = Mathf.Clamp01(hpRatio);

            var scale = hpBar.transform.localScale;
            scale.x = hpRatio;
            hpBar.transform.localScale = scale;

            // update HP bar color
            Color col;
            if (hpRatio < 0.5f) {
                col = Color.Lerp(Color.red, Color.yellow, hpRatio * 2);
            }
            else {
                col = Color.Lerp(Color.yellow, Color.green, 2 * hpRatio - 1);
            }
            hpBar.color = col;

        }
    
        public void PlayDeathAnim() {
            if (c==null) {
                return; 
            }
            var m = deathParticles.main;
            var sc = m.startColor;
            sc.color = c.GetGlowColor();
            m.startColor = sc;
            deathParticles.Play();
            c = null;
            sr.enabled = false;
            glowSr.enabled = false;
            hpBar.enabled = false;
            //parentPool.Return(this);
            //gameObject.SetActive(false);
        }
    }
}
