using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LPE;
using System;
using Core;

namespace GameUI.CreepMenus {
    public abstract class CreepLoadoutSlotSelector {
        public abstract CreepLoadoutSlot Select(CreepLoadout l);
    }


    public class CreepLoadoutSlotSelector_Loot : CreepLoadoutSlotSelector {
        public override CreepLoadoutSlot Select(CreepLoadout l) {
            return l.loot;
        }
    }


    public class CreepLoadoutSlotSelector_Build : CreepLoadoutSlotSelector {
        public override CreepLoadoutSlot Select(CreepLoadout l) {
            return l.build;
        }
    }

    public class CreepLoadoutSlotSelector_Armor : CreepLoadoutSlotSelector {
        public override CreepLoadoutSlot Select(CreepLoadout l) {
            return l.armor;
        }
    }


    public class  CreepMenuLoadoutEntryBehaviour : MonoBehaviour {
        public CreepLoadoutSlotSelector selector { get; private set; }

        [SerializeField] LPEButtonBehaviour button;
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI levelText;
        [SerializeField] Image indicator;

     
        public bool Clicked() {
            return button.Clicked;
        }

        public void InitSelector(CreepLoadoutSlotSelector selector) {
            this.selector = selector;
        }

        public void Set(ScenarioInstance s, CreepLoadout l) {
            var slot = selector.Select(l);
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
