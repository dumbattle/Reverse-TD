using UnityEngine;
using LPE;
using TMPro;
using Core;


namespace GameUI.CreepMenus {
    public class CreepStatEntryBehaviour : MonoBehaviour {
        public LPEButtonBehaviour upgradeButton;

        [SerializeField] ResourceDisplayBehaviour resourceDisplay;
        [SerializeField] TextMeshProUGUI valueText;
        [SerializeField] Transform barPivot;

        [SerializeField] string prefix;
        [SerializeField] string format;

        public void Redraw(ScenarioInstance s, BasicCreepStat stat) {
            valueText.text = prefix + stat.GetValueForCurrentLevel().ToString(format);
            resourceDisplay.Display(s, stat.GetCostForNextLevel());
            barPivot.transform.localScale = new Vector3(stat.GetRatioToBase() / 4f, 1, 1);
        }
    }
}
