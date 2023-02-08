using UnityEngine;

namespace Core {
    public class ScenarioUI_CreepMenu_LoadoutSlot_Subpanel : MonoBehaviour {
        [Header("Sub Panels")]
        public RectTransform subpanelRoot;
        public ScenarioUI_CreepMenu_LoadoutSlot_UpgradePanel upgradePanel;
        public ScenarioUI_CreepMenu_LoadoutSlot_SelectionPanel selectPanel;

        [Header("Slot Selection")]
        public ScenarioUI_CreepMenu_Details_AttachmentEntry_Behaviour specialization;
        public ScenarioUI_CreepMenu_Details_AttachmentEntry_Behaviour resource;

        public ScenarioUI_CreepMenu_Details_AttachmentEntry_Behaviour tier1_1;
        public ScenarioUI_CreepMenu_Details_AttachmentEntry_Behaviour tier1_2;
        public ScenarioUI_CreepMenu_Details_AttachmentEntry_Behaviour tier1_3;

        public ScenarioUI_CreepMenu_Details_AttachmentEntry_Behaviour tier2_1;
        public ScenarioUI_CreepMenu_Details_AttachmentEntry_Behaviour tier2_2;

        public ScenarioUI_CreepMenu_Details_AttachmentEntry_Behaviour tier3_A;
        public ScenarioUI_CreepMenu_Details_AttachmentEntry_Behaviour tier3_B;
    }
}
