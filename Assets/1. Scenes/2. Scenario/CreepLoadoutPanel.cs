using Core;
using UnityEngine;
using TMPro;
using LPE;


namespace GameUI.CreepMenus {
    [System.Serializable]
    public class CreepLoadoutPanel {
        [SerializeField] GameObject root;

        [SerializeField] CreepMenuLoadoutEntryBehaviour lootEntry;
        [SerializeField] CreepMenuLoadoutEntryBehaviour armorEntry;
        [SerializeField] CreepMenuLoadoutEntryBehaviour classEntry;

        [SerializeField] CreepMenuLoadoutEntryBehaviour attr1Entry;
        [SerializeField] CreepMenuLoadoutEntryBehaviour attr2Entry;
        [SerializeField] CreepMenuLoadoutEntryBehaviour attr3Entry;

        [SerializeField] CreepMenuLoadoutEntryBehaviour special1Entry;
        [SerializeField] CreepMenuLoadoutEntryBehaviour special2Entry;

        public CreepLoadout_DetailsPanel detailsPanel;

        public void Init() {
            lootEntry.InitSelector(new CreepLoadoutSlotSelector_Loot());
            armorEntry.InitSelector(new CreepLoadoutSlotSelector_Armor());
            classEntry.InitSelector(new CreepLoadoutSlotSelector_Build());
        }

        public void Open(ScenarioInstance s, CreepLoadout l) {
            root.gameObject.SetActive(true);
            detailsPanel.Close();
            Redraw(s, l);
        }

        public void Close() {
            root.gameObject.SetActive(false);
            detailsPanel.Close();
        }

        public void Redraw(ScenarioInstance s, CreepLoadout l) {
            lootEntry.Set(s, l);
            armorEntry.Set(s, l);
            classEntry.Set(s, l);
        }
        public CreepMenuLoadoutEntryBehaviour CheckSelection(CreepLoadout l) {
            return
                lootEntry.Clicked() ? lootEntry :
                armorEntry.Clicked() ? armorEntry :
                classEntry.Clicked() ? classEntry :
                null;
        }
    }

    [System.Serializable]
    public class CreepLoadout_DetailsPanel {
        [SerializeField] GameObject root;

        [Header("Description")]
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI levelText;
        [SerializeField] TextMeshProUGUI descriptionText;

        [Header("Purchase")]
        [SerializeField] ResourceDisplayBehaviour costBehaviour;
        [SerializeField] TextMeshProUGUI costLabel;
        [SerializeField] LPEButtonBehaviour purchaseButton;
        [SerializeField] TextMeshProUGUI purchaseText;

        [Header("Upgrade Specific")]
        [SerializeField] LPEButtonBehaviour replaceButton;
        [SerializeField] TextMeshProUGUI replaceText;

        [Header("New Attach Specific")]
        [SerializeField] LPEButtonBehaviour leftButton;
        [SerializeField] LPEButtonBehaviour rightButton;

        public void DisplayCurrent(ScenarioInstance s, CreepLoadoutSlot l) {
            var a = l.currentAttactment;
            var d = a.definition;
            if (d == null) {
                throw new System.ArgumentException("Cannot display current loadout slot with no set attachment instance");
            }

            nameText.text = a.GetName();
            levelText.text = $"Level {a.level}/{a.maxLevel}";
            descriptionText.text = a.GetDescription();

            var upgradeCost = a.GetCostForUpgrade();
            if (upgradeCost == null) {
                costBehaviour.gameObject.SetActive(false);
                costLabel.text = "Max Level";
                purchaseButton.gameObject.SetActive(false);
            }
            else {
                costBehaviour.gameObject.SetActive(true);
                costBehaviour.Display(s, upgradeCost);
                costLabel.text = "Next Level:";
                purchaseButton.gameObject.SetActive(true);
                purchaseText.text = "Upgrade";
            }

            replaceButton.gameObject.SetActive(true);
            replaceText.text = "Replace"; 
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
        }

        public void DisplayPurchaseOption(ScenarioInstance s, CreepLoadoutSlot l, int index) {
            // Cases:
            //  - No current ("Replace"/"Buy")
            var option = l.allowedAttachments[index];
            if (option == l.currentAttactment.definition) {
                throw new System.ArgumentException("Displaying current attachment definition as pruchase option");
            }

            nameText.text = option.GetName(0);
            levelText.text = $"Level 1/{option.maxLevel}";
            descriptionText.text = option.GetDescription(0);

            var purchaseCost = option.GetCost(1);
      
            costBehaviour.gameObject.SetActive(true);
            costBehaviour.Display(s, purchaseCost);
            costLabel.text = "Cost:";
            purchaseButton.gameObject.SetActive(true);
            purchaseText.text =  l.currentAttactment.definition == null ? "Apply" : "Replace";

            replaceButton.gameObject.SetActive(true);
            replaceText.text = l.currentAttactment.definition == null ? "Clost" : "Back"; 
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
        }

        public void Open() {
            root.gameObject.SetActive(true);
        }

        public void Close() {
            root.gameObject.SetActive(false);
        }


        //******************************************************************************************
        // Queries
        //******************************************************************************************
        
        public bool CheckPurchase() {
            return purchaseButton.Clicked;
        }

        public bool CheckSecondaryButton() {
            return replaceButton.Clicked;
        }

        /// <summary>
        /// 1 = Right, -1 = Left, 0 = no input
        /// </summary>
        public int GetDirectionBrowse() {
            if (leftButton.Clicked) {
                return -1;
            }
            
            if (rightButton.Clicked) {
                return 1;
            }

            return 0;
        }
    }
}
