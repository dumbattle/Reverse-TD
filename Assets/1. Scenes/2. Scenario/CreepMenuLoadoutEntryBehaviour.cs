using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LPE;
using System;
using Core;

namespace GameUI.CreepMenus {
    public class  CreepMenuLoadoutEntryBehaviour : MonoBehaviour {
        [SerializeField] LPEButtonBehaviour button;
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI levelText;
        [SerializeField] Image indicator;

     
        public bool Clicked() {
            return button.Clicked;
        }

        public void Set(ScenarioInstance s, CreepLoadoutSlot slot) {
            var a = slot.currentAttactment;

            if (a.definition == null) {
                nameText.text = "Not Selected";
                levelText.text = "Lvl -/-";
                indicator.color = Color.red;
            }
            else {

                nameText.text = a.GetName();
                levelText.text = $"Lvl {a.level}/{a.maxLevel}";

                var upgradeCost = a.GetCostForUpgrade();
                bool canUpgrade = upgradeCost != null && s.playerFunctions.GetCurrentResources().Satisfies(upgradeCost);
                indicator.color = canUpgrade  ? Color.yellow : Color.green;
            }
        }
    }
}
