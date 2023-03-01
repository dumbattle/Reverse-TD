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
        public LPEButtonBehaviour replaceButton;

        [Space]
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI upgradeButtonText;
        public Image itemIcon;

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

        public void Open(ScenarioInstance s, CreepLoadoutSlot l) {
            selectedLoadout = l;
            rt.sizeDelta = new Vector2(openWidth, rt.sizeDelta.y);
            nameText.text = l.currentAttactment.GetName();
            levelText.text = levelTextCache[l.currentAttactment.level-1];
            descriptionText.text = l.currentAttactment.GetDescription();
            itemIcon.sprite = l.currentAttactment.GetIcon();

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
                var playerResources = s.playerFunctions.GetCurrentResources();

                if (gCost > 0) {
                    gCostRoot.gameObject.SetActive(true);
                    gCostText.text = gCost.ToString();
                    gCostText.color = playerResources[ResourceType.green] >= gCost ? Color.white : Color.red;
                }

                if (rCost > 0) {
                    rCostRoot.gameObject.SetActive(true);
                    rCostText.text = rCost.ToString();
                    rCostText.color = playerResources[ResourceType.red] >= rCost ? Color.white : Color.red;
                }

                if (bCost > 0) {
                    bCostRoot.gameObject.SetActive(true);
                    bCostText.text = bCost.ToString();
                    bCostText.color = playerResources[ResourceType.blue] >= bCost ? Color.white : Color.red;
                }

                if (yCost > 0) {
                    yCostRoot.gameObject.SetActive(true);
                    yCostText.text = yCost.ToString();
                    yCostText.color = playerResources[ResourceType.yellow] >= yCost ? Color.white : Color.red;
                }

                if (dCost > 0) {
                    dCostRoot.gameObject.SetActive(true);
                    dCostText.text = dCost.ToString();
                    dCostText.color = playerResources[ResourceType.diamond] >= dCost ? Color.white : Color.red;
                }
            }
        }

        public void Close() {
            rt ??= GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(0, rt.sizeDelta.y);
        }
    }
}
