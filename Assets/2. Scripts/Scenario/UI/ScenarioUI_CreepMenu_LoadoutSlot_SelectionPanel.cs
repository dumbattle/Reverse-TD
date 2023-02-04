using UnityEngine;
using TMPro;
using System.Collections.Generic;
using LPE;


namespace Core {
    public class ScenarioUI_CreepMenu_LoadoutSlot_SelectionPanel : MonoBehaviour {
        [SerializeField] float openWidth;
        [SerializeField] ScenarioUI_CreepMenu_LoadoutSlot_SelectionPanel_OptionEntry entrySrc;

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;

        public TextMeshProUGUI gCostText;
        public TextMeshProUGUI rCostText;
        public TextMeshProUGUI bCostText;
        public TextMeshProUGUI yCostText;
        public TextMeshProUGUI dCostText;

        public GameObject gCostRoot;
        public GameObject rCostRoot;
        public GameObject bCostRoot;
        public GameObject yCostRoot;
        public GameObject dCostRoot;

        public TextMeshProUGUI applyButtonText;
        public LPEButtonBehaviour applyButton;

        public CreepAttachmentDefinition selectedAttachment { get; private set; }
        public CreepLoadoutSlot selectedLoadout { get; private set; }
        List<ScenarioUI_CreepMenu_LoadoutSlot_SelectionPanel_OptionEntry> entries = new List<ScenarioUI_CreepMenu_LoadoutSlot_SelectionPanel_OptionEntry>();
        RectTransform rt;



        void Awake() {
            rt = GetComponent<RectTransform>();
            entrySrc.gameObject.SetActive(false);
        }
        
        public void Open(CreepLoadoutSlot l) {
            // track selection
            selectedLoadout = l;

            // open panel
            rt.sizeDelta = new Vector2(openWidth, rt.sizeDelta.y);

            // close all entries
            foreach (var e in entries) {
                e.gameObject.SetActive(false);
            }

            // set entries
            for (int i = 0; i < l.allowedAttachments.Length; i++) {
                if (entries.Count <= i) {
                    var newE = Instantiate(entrySrc, entrySrc.transform.parent);
                    entries.Add(newE);

                    int ind = i; // do not capture 'i'
                    newE.button.SetClickListener(() => OptionSelected(ind, newE.attachment));
                }

                var opt = l.allowedAttachments[i];
                var entry = entries[i];
                entry.gameObject.SetActive(true);
                entry.icon.sprite = opt.GetIcon(1);
                entry.selectHighlight.SetActive(false);
                entry.attachment = opt;
            }

            // default select 1st option
            OptionSelected(0, l.allowedAttachments[0]);
        }

        public void Close() {
            rt.sizeDelta = new Vector2(0, rt.sizeDelta.y);
        }


        void OptionSelected(int i, CreepAttachmentDefinition atch) {
            // track selection
            selectedAttachment = atch;

            // close all entries
            foreach (var e in entries) {
                e.selectHighlight.SetActive(false);
            }

            // highlight
            entries[i].selectHighlight.SetActive(true);

            // set details
            nameText.text = atch.GetName(0);
            descriptionText.text = atch.GetDescription(0);

            var cost = atch.GetCost(1);
            var gCost = cost[ResourceType.green];
            var rCost = cost[ResourceType.red];
            var bCost = cost[ResourceType.blue];
            var yCost = cost[ResourceType.yellow];
            var dCost = cost[ResourceType.diamond];
            gCostRoot.gameObject.SetActive(false);
            rCostRoot.gameObject.SetActive(false);
            bCostRoot.gameObject.SetActive(false);
            yCostRoot.gameObject.SetActive(false);
            dCostRoot.gameObject.SetActive(false);

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
}
