using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
        [SerializeField] RectTransform parentLayout;


        public TextMeshProUGUI nameText;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI descriptionText;

        public TextMeshProUGUI gCostText;
        public TextMeshProUGUI rCostText;
        public TextMeshProUGUI bCostText;
        public TextMeshProUGUI yCostText;
        public TextMeshProUGUI dCostText;

        public TextMeshProUGUI upgradeButtonText;


        RectTransform rt;

        private void Awake() {
            rt = GetComponent<RectTransform>();
        }

        public void Open(CreepLoadoutSlot l) {
            rt.sizeDelta = new Vector2(openWidth, rt.sizeDelta.y);
            nameText.text = l.currentAttactment.GetName();
            levelText.text = levelTextCache[l.currentAttactment.level-1];
            descriptionText.text = l.currentAttactment.GetDescription();

            var cost = l.currentAttactment.GetCostForrUpgrade();
            if (cost != null) {
                gCostText.text = cost[ResourceType.green].ToString();
                rCostText.text = cost[ResourceType.red].ToString();
                bCostText.text = cost[ResourceType.blue].ToString();
                yCostText.text = cost[ResourceType.yellow].ToString();
                dCostText.text = cost[ResourceType.diamond].ToString();
            }
        }

        public void Close() {
            rt.sizeDelta = new Vector2(0, rt.sizeDelta.y);
        }
    }
}
