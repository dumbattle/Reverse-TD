using LPE;
using TMPro;
using UnityEngine;



namespace Core {
    [System.Serializable]
    public class CreepPurchasePanel {
        public GameObject root;
        public ResourceDisplayBehaviour costDisplay;
        public LPEButtonBehaviour buyButton;
        public TextMeshProUGUI buyText;

        public ResourceAmount cost { get; private set; }

        public void Open(ScenarioInstance s, ResourceAmount cost) {
            root.gameObject.SetActive(true);
            this.cost = cost;
            costDisplay.Display(s, cost);
        }

        public void Close() {
            root.gameObject.SetActive(false);
        }

    }
}
