using System.Collections.Generic;
using UnityEngine;



namespace Core {
    public class MainTower_NoAttackBehaviour : TowerBehaviour, IMainTower {
        bool defeated = false;
        public SpriteRenderer explosionSr;
        public GameObject towerSpriteRoot;
        public GameObject destroyedSpriteRoot;
        float xplodeTimer = 0;

        void Awake() {
            explosionSr.gameObject.SetActive(false);
            destroyedSpriteRoot.SetActive(false);
        }
        void Update() {
            if (explosionSr.gameObject.activeInHierarchy) {
                xplodeTimer += FrameUtility.DeltaTime(true);
                float scale = xplodeTimer * 5 + 1;
                explosionSr.transform.localScale = new Vector3(scale, scale, 1);
                var c = explosionSr.color;
                c.a  = 1 - xplodeTimer / 3f;
                explosionSr.color = c;
            }
        }
        //***************************************************************************************
        // Abstract Implementation
        //***************************************************************************************

        public override void GameUpdate(ScenarioInstance s) { }

        public override void GetGeneralUpgradeOptions(List<UpgradeOption> results) { }

        public override void GetSpecializationUpgradeOptions(ScenarioInstance s, List<SpecializationUpgradeOptions> results) { }

        public override int GetTotalUpgradeLevel() {
            return 0;
        }

        protected override List<TowerUpgradeDetails> GetTowerUpgradeDetails() {
            return new List<TowerUpgradeDetails>();
        }

        //***************************************************************************************
        // IMainTower Implementation
        //***************************************************************************************

        public void SetAsDefeated() {
            defeated = true;
            active = false;
            explosionSr.gameObject.SetActive(true);
            towerSpriteRoot.SetActive(false);
            destroyedSpriteRoot.SetActive(true);
            xplodeTimer = 0;
        }

        public bool IsDefeated() => defeated;

    }
}
