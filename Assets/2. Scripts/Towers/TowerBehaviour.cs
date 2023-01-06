using UnityEngine;
using LPE.Shape2D;

namespace Core {
    public abstract class TowerBehaviour : MonoBehaviour, ITower {
        [SerializeField] int size;
        Vector2Int bl;
        Vector2Int tr;
        RectangleShape shape;
        public Vector2 position => shape.position;

        
        public void Init(ScenarioInstance s, Vector2Int bl) {
            this.bl = bl;
            tr = bl + new Vector2Int(size - 1, size - 1);

            shape = new RectangleShape(size - 0.2f, size - 0.2f);
            shape.SetPosition(bl + new Vector2(size - 1, size - 1) / 2f);

            transform.position = (s.mapQuery.TileToWorld(bl) + s.mapQuery.TileToWorld(tr)) / 2;
        }

        public abstract void GameUpdate(ScenarioInstance s);


        public Vector2Int GetBottomLeft() {
            return bl;
        }

        public Vector2Int GetTopRight() {
            return tr;
        }
        public Shape2D GetShape() {
            return shape;
        }
    }
}
