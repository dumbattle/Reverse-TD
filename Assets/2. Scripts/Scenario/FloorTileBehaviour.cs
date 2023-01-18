using UnityEngine;


namespace Core {
    public class FloorTileBehaviour : MonoBehaviour {
        [SerializeField] SpriteRenderer sr;
        Tile tile;
        float pingTime = 0;


        void Update() {
            //pingTime -= 1 / 25f;
            //var t = Mathf.Clamp01(pingTime);
            //sr.color = Color.Lerp(Color.white, Color.blue, t);
        }

        public void SetTile(Tile t) {
            tile = t;
        }

        public void Ping() {
            pingTime = 1;
        }

        public void SetSprite(Sprite s) {
            sr.sprite = s;
        }
    }
}
