using UnityEngine;
using LPE.Shape2D;
using System.Collections.Generic;


namespace Core {
    public abstract class TowerBehaviour : MonoBehaviour, ITower {
        public SpriteRenderer rankIcon;
        [SerializeField] int size;

        Vector2Int bl;
        Vector2Int tr;
        RectangleShape shape;
        public Vector2 position => shape.position;
        public int Size => size;

        List<TowerUpgradeDetails> upgrades;

        protected bool active = true;

        public void Init(ScenarioInstance s, Vector2Int bl) {
            this.bl = bl;
            tr = bl + new Vector2Int(size - 1, size - 1);

            shape = new RectangleShape(size - 0.2f, size - 0.2f);
            shape.SetPosition(bl + new Vector2(size - 1, size - 1) / 2f);

            transform.position = (s.mapQuery.TileToWorld(bl) + s.mapQuery.TileToWorld(tr)) / 2;

            if (rankIcon != null) {
                rankIcon.sprite = IconResourceCache.Rank(GetTotalUpgradeLevel()); 
            }
            upgrades = GetTowerUpgradeDetails();
        }



        public Vector2Int GetBottomLeft() {
            return bl;
        }

        public Vector2Int GetTopRight() {
            return tr;
        }
        public Shape2D GetShape() {
            return shape;
        }






        public void Refresh() {
            if (rankIcon != null) {
                rankIcon.sprite = IconResourceCache.Rank(GetTotalUpgradeLevel());
            }
        }
        public void Destroy() {
            GameObject.Destroy(this.gameObject);
        }
      
        List<TowerUpgradeDetails> ITower.GetGeneralUpgrades() {
            return upgrades;
        }
        
        public void TransferGeneralUpgrade(TowerUpgradeDetails d) {
            foreach (var u in upgrades) {
                if (u.ID != d.ID) {
                    continue;
                }

                while(u.currentLevel < d.currentLevel) {
                    u.IncrmentLevel();
                    if (!u.UpgradeAvailable()) {
                        break;
                    }
                }
            }
        }
        //************************************************************************************************************
        // Abstract/Virtual
        //************************************************************************************************************
        public virtual void OnBeforeUpgrade(ScenarioInstance s) { }
        public abstract void GameUpdate(ScenarioInstance s);
        protected abstract int GetTotalUpgradeLevel();
        public abstract void GetGeneralUpgradeOptions(List<UpgradeOption> results);
        public virtual void EndRound() { }
        public abstract void GetSpecializationUpgradeOptions(ScenarioInstance s, List<SpecializationUpgradeOptions> results);
        protected abstract List<TowerUpgradeDetails> GetTowerUpgradeDetails();

        [System.Serializable]
        struct SpecializationOption {
            public TowerBehaviour tower;
            public int cost;
        }
    }
}
