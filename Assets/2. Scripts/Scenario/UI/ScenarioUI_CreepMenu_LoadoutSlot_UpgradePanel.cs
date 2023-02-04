using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LPE;

namespace Core {
    public class ScenarioUI_CreepMenu_LoadoutSlot_UpgradePanel: MonoBehaviour {
        static string[] levelTextCache = { 
            "Level 1",
            "Level 2",
            "Level 3",
            "Level 4",
            "Level 5",
            "Level 6",
            "Level 7",
            "Level 8",
            "Level 9",
            "Level 10",
        };

        [SerializeField] float openWidth;
        public LPEButtonBehaviour upgradeButton;

        [Space]
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI upgradeButtonText;

        [Header("Costs")]
        public TextMeshProUGUI gCostText;
        public TextMeshProUGUI rCostText;
        public TextMeshProUGUI bCostText;
        public TextMeshProUGUI yCostText;
        public TextMeshProUGUI dCostText;

        [Space]
        public GameObject gCostRoot;
        public GameObject rCostRoot;
        public GameObject bCostRoot;
        public GameObject yCostRoot;
        public GameObject dCostRoot;



        RectTransform rt;
        public CreepLoadoutSlot selectedLoadout { get; private set; }


        void Awake() {
            rt = GetComponent<RectTransform>();
        }

        public void Open(CreepLoadoutSlot l) {
            selectedLoadout = l;
            rt.sizeDelta = new Vector2(openWidth, rt.sizeDelta.y);
            nameText.text = l.currentAttactment.GetName();
            levelText.text = levelTextCache[l.currentAttactment.level-1];
            descriptionText.text = l.currentAttactment.GetDescription();

            gCostRoot.gameObject.SetActive(false);
            rCostRoot.gameObject.SetActive(false);
            bCostRoot.gameObject.SetActive(false);
            yCostRoot.gameObject.SetActive(false);
            dCostRoot.gameObject.SetActive(false);

            var cost = l.currentAttactment.GetCostForUpgrade();
            if (cost != null) {
                var gCost = cost[ResourceType.green];
                var rCost = cost[ResourceType.red];
                var bCost = cost[ResourceType.blue];
                var yCost = cost[ResourceType.yellow];
                var dCost = cost[ResourceType.diamond];
                if (gCost > 0) {
                    gCostRoot.gameObject.SetActive(true);
                    gCostText.text = gCost.ToString();
                }

                if (rCost > 0) {
                    rCostRoot.gameObject.SetActive(true);
                    rCostText.text = rCost.ToString();
                }

                if (bCost > 0) {
                    bCostRoot.gameObject.SetActive(true);
                    bCostText.text = bCost.ToString();
                }

                if (yCost > 0) {
                    yCostRoot.gameObject.SetActive(true);
                    yCostText.text = yCost.ToString();
                }

                if (dCost > 0) {
                    dCostRoot.gameObject.SetActive(true);
                    dCostText.text = dCost.ToString();
                }
            }
        }

        public void Close() {
            rt.sizeDelta = new Vector2(0, rt.sizeDelta.y);
        }
    }
}
