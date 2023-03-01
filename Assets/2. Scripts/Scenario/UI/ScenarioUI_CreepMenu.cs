using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using LPE;
using TMPro;
using System;


namespace Core {
    public class ScenarioUI_CreepMenu : MonoBehaviour, IPointerDownHandler {
        [SerializeField] ScrollRect scrollRect;
        List<ScenarioUI_CreepMenu_CreepEntry_Behaviour> creepEntries = new List<ScenarioUI_CreepMenu_CreepEntry_Behaviour>();
        [SerializeField] ScenarioUI_CreepMenu_CreepEntry_Behaviour creepEntrySrc;
        public CreepDetailsReferences details;
        public ScenarioUI_CreepMenu_LoadoutSlot_Subpanel loadout;

        public LPEButtonBehaviour buyCreepButton;
        public TextMeshProUGUI buyCostText;
        public CreepSquad creepSelected { get; private set; }

        public CreepSquad currentFamily { get; private set; }
        public CreepSquad currentSelectedSquad { get; private set; }

        int openMode = -1;
        CreepLoadoutSlot currentSlot;
        ScenarioUI_CreepMenu_Details_AttachmentEntry_Behaviour currnetButtonEntry;

        //*************************************************************************************************************
        // Unity Methods
        //*************************************************************************************************************

        void Start() {
            creepEntrySrc.gameObject.SetActive(false);
            creepEntrySrc.gameObject.SetActive(false);
            buyCreepButton.SetDownListener(InputManager.Set.ButtonDown);
            OpenStatsSubmenu();
            OpenDetailsTab(false);
        }
        
        //private void Update() {
        //    if (openMode == 1 ? openPosition > 1 : openPosition < 0) {
        //        return;
        //    }
        //    openPosition += FrameUtility.DeltaTime(false) * openMode / 0.5f;

        //    var pos = details.root.anchoredPosition;
        //    pos.x = Mathf.Lerp(-details.root.sizeDelta.x, 0, 1 - (1 - openPosition) * (1 - openPosition));
        //    details.root.anchoredPosition = pos;
        //}
        
        void LateUpdate() {
            creepSelected = null;
        }

        public void OnPointerDown(PointerEventData eventData) {
            InputManager.Set.ButtonDown();
        }

        //*************************************************************************************************************
        // Control
        //*************************************************************************************************************

        public void ReDrawCreepList(ScenarioInstance s) {
            var creepArmy = s.playerFunctions.GetCreepArmy();

            for (int i = 0; i < creepArmy.count; i++) {
                // Get entry
                if (creepEntries.Count <= i) {
                    // new entry
                    var newE = Instantiate(creepEntrySrc, creepEntrySrc.transform.parent);
                    newE.button.SetDownListener(InputManager.Set.ButtonDown);
                    newE.button.SetClickListener(() => SelectCreepEntry(newE.squad));
                    creepEntries.Add(newE);
                }

                var e = creepEntries[i];

                // Cache
                var squad = creepArmy.GetSquad(i);
                var c = squad.actualDefinition;

                // Set entry values
                e.icon.sprite = c.sprite;
                e.glowIcon.color = c.glowColor;
                e.iconRoot.localScale = new Vector3(c.radius * 2, c.radius * 2, 1);
                e.gameObject.SetActive(true);
                e.squad = squad;
                e.countText.text = $"x {(int)squad.actualDefinition.count}";
                // set death child icons
                var deathSpawnSquad = squad.GetDeathSplitSquad();
                e.deathChild.gameObject.SetActive(false);
                e.deathGlow.gameObject.SetActive(false);
                if (deathSpawnSquad != null) {
                    e.deathChild.sprite = deathSpawnSquad.actualDefinition.sprite;
                    e.deathGlow.color = deathSpawnSquad.actualDefinition.glowColor;
                    e.deathChild.gameObject.SetActive(true);
                    e.deathGlow.gameObject.SetActive(true);
                }

                // set carrier child icons
                var carrierSquad = squad.GetCarrierSquad();
                e.carrierChild.gameObject.SetActive(false);
                e.carrierGlow.gameObject.SetActive(false);
                if (carrierSquad != null) {
                    e.carrierChild.sprite = carrierSquad.actualDefinition.sprite;
                    e.carrierGlow.color = carrierSquad.actualDefinition.glowColor;
                    e.carrierChild.gameObject.SetActive(true);
                    e.carrierGlow.gameObject.SetActive(true);
                }
            }

            buyCreepButton.transform.SetAsLastSibling();
            var cost = s.playerFunctions.GetNewCreepCost();
            buyCostText.text = cost[ResourceType.green].ToString();
        }

        public void SetCreepDetails(ScenarioInstance s, CreepSquad family) {
            if (family.isChild) {
                SetCreepDetails(s, family.parentSquad);
                return;
            }
            currentFamily = family;
            var c = family.actualDefinition;
            HighlightCreepSelection(family);
            SelectParentOfCurrentSquad(s);
        }

        public void SelectParentOfCurrentSquad(ScenarioInstance s) {
            var c = currentFamily.actualDefinition;

            // set display creep
            details.creepImage.sprite = c.sprite;
            details.creepGlowImage.color = c.glowColor;

            // button update
            ResetFamilySelectButtons(currentFamily);
            details.buttons.parentSelection.sprite = details.buttons.selectedSprite;


            UpdateCreepDetails(s, currentFamily);
        }
      
        public void SelectCarrierChildOfCurrentSquad(ScenarioInstance s) {
            var child = currentFamily.GetCarrierSquad();
            if (child == null) {
                return;
            }

            var c = child.actualDefinition;

            // set display creep
            details.creepImage.sprite = c.sprite;
            details.creepGlowImage.color = c.glowColor;

            // button update
            ResetFamilySelectButtons(currentFamily);
            details.buttons.childCarrierSelection.sprite = details.buttons.selectedSprite;


            UpdateCreepDetails(s, child);
        }
       
        public void SelectDeathChildOfCurrentSquad(ScenarioInstance s) {
            var child = currentFamily.GetDeathSplitSquad();
            if (child == null) {
                return;
            }

            var c = child.actualDefinition;

            // set display creep
            details.creepImage.sprite = c.sprite;
            details.creepGlowImage.color = c.glowColor;

            // button update
            ResetFamilySelectButtons(currentFamily);
            details.buttons.childDeathSelection.sprite = details.buttons.selectedSprite;


            UpdateCreepDetails(s, child);
        }

        public void OpenStatsSubmenu() {
            CloseAllSubmenus();
            details.submenu.stats.rootObj.SetActive(true);
            details.buttons.statsSubmenuSelection.sprite = details.buttons.selectedSprite;
        }
        public void OpenInfoSubmenu() {
            CloseAllSubmenus();
            details.submenu.info.rootObj.SetActive(true);
            details.buttons.infoSubmenuSelection.sprite = details.buttons.selectedSprite;
        }

        public void OpenAttachmentsSubmenu() {
            DeselectLoadoutSelection();
            CloseAllSubmenus();
            details.submenu.loadout.rootObj.SetActive(true);
            details.buttons.attachmentsSubmenuSelection.sprite = details.buttons.selectedSprite;
        }

        public void OpenDetailsTab(bool value) {
            openMode = value ? 1 : -1;
            var pos = details.root.anchoredPosition;
            pos.x = value ? 0 :-details.root.sizeDelta.x;
            details.root.anchoredPosition = pos;
            if (!value) {
                loadout.selectPanel.Close();
                loadout.upgradePanel.Close();
                LayoutRebuilder.ForceRebuildLayoutImmediate(loadout.subpanelRoot);
                HighlightCreepSelection(null);
            }
        }
        
        public void OpenAttachmentmentReplace(ScenarioInstance s) {
            loadout.selectPanel.Open(s, currentSlot);
            LayoutRebuilder.ForceRebuildLayoutImmediate(loadout.subpanelRoot);
        }
        
        /// <summary>
        /// TODO - REFACTOR
        /// </summary>
        public void OpenLoadoutSlot(ScenarioInstance s, CreepLoadoutSlot slot, ScenarioUI_CreepMenu_Details_AttachmentEntry_Behaviour buttonEntry) {
            DeselectLoadoutSelection();
            buttonEntry.SetSelected(true);

            var atch = slot.currentAttactment.definition;

            if (atch == null) {
                loadout.selectPanel.Open(s, slot);
            }
            else {
                loadout.upgradePanel.Open(s, slot);
            }

            currentSlot = slot;
            currnetButtonEntry = buttonEntry;
            LayoutRebuilder.ForceRebuildLayoutImmediate(loadout.subpanelRoot);
        }

        public void ReopenLoadoutSlot(ScenarioInstance s) {
            OpenLoadoutSlot(s, currentSlot, currnetButtonEntry);
        }

        public void RedrawCreepDetails(ScenarioInstance s) {
            UpdateCreepStatSubmenu(s, currentSelectedSquad);
            UpdateAttachmentSubMenu(s, currentSelectedSquad);
        }

        //***********************************************************************************************************
        // Query
        //***********************************************************************************************************

        public bool DetailsIsOpen() {
            return openMode == 1;
        }

        //***********************************************************************************************************
        // Button Callbacks
        //***********************************************************************************************************

        void SelectCreepEntry(CreepSquad s) {
            creepSelected = s;
        }

        //***********************************************************************************************************
        // Helpers
        //***********************************************************************************************************

        void CloseAllSubmenus() {
            details.submenu.stats.rootObj.SetActive(false);
            details.submenu.info.rootObj.SetActive(false);
            details.submenu.loadout.rootObj.SetActive(false);

            details.buttons.statsSubmenuSelection.sprite = details.buttons.unselectedSprite;
            details.buttons.infoSubmenuSelection.sprite = details.buttons.unselectedSprite;
            details.buttons.attachmentsSubmenuSelection.sprite = details.buttons.unselectedSprite;
        }
        
        void ResetFamilySelectButtons(CreepSquad parent) {
            var c = parent.actualDefinition;

            // parent
            details.buttons.parentCreep.sprite = c.sprite;
            details.buttons.parentGlow.color = c.glowColor;
            details.buttons.parentSelection.sprite = details.buttons.unselectedSprite;

            // carrier child
            var carrierChild = parent.GetCarrierSquad();

            if (carrierChild == null) {
                details.buttons.childCarrierSelection.sprite = details.buttons.unavailableSprite;
                details.buttons.childCarrierCreep.gameObject.SetActive(false);
                details.buttons.childCarrierGlow.gameObject.SetActive(false);
            }
            else {
                details.buttons.childCarrierSelection.sprite = details.buttons.unselectedSprite;
                details.buttons.childCarrierCreep.gameObject.SetActive(true);
                details.buttons.childCarrierGlow.gameObject.SetActive(true);
                details.buttons.childCarrierCreep.sprite = carrierChild.actualDefinition.sprite;
                details.buttons.childCarrierGlow.color = carrierChild.actualDefinition.glowColor;
            }
            // death child
            var deathChild = parent.GetDeathSplitSquad();

            if (deathChild == null) {
                details.buttons.childDeathSelection.sprite = details.buttons.unavailableSprite;
                details.buttons.childDeathCreep.gameObject.SetActive(false);
                details.buttons.childDeathGlow.gameObject.SetActive(false);
            }
            else {
                details.buttons.childDeathSelection.sprite = details.buttons.unselectedSprite;
                details.buttons.childDeathCreep.gameObject.SetActive(true);
                details.buttons.childDeathGlow.gameObject.SetActive(true);
                details.buttons.childDeathCreep.sprite = deathChild.actualDefinition.sprite;
                details.buttons.childDeathGlow.color = deathChild.actualDefinition.glowColor;
            }
        }

        void UpdateCreepDetails(ScenarioInstance s, CreepSquad squad) {
            currentSelectedSquad = squad;
            UpdateCreepStatSubmenu(s, squad);
            UpdateAttachmentSubMenu(s, squad);
            DeselectLoadoutSelection();
            LayoutRebuilder.ForceRebuildLayoutImmediate(loadout.subpanelRoot);
        }

        void UpdateCreepStatSubmenu(ScenarioInstance s, CreepSquad squad) {
            var c = squad.actualDefinition;
            var defaultSquad = s.playerFunctions.GetCreepArmy().defaultSquad;
            defaultSquad.Recalculate();
            var defaultCreep = defaultSquad.actualDefinition;

            // set stat texts
            details.submenu.stats.hpText.text = ((int)c.hp).ToString();
            //details.submenu.stats.moneyText.text = ((int)c.moneyReward).ToString();
            details.submenu.stats.speedText.text = c.speed.ToString("f2");
            details.submenu.stats.countText.text = ((int)c.count).ToString();
            //details.submenu.stats.spawnRateText.text = c.spawnRate.ToString("f2");
            details.submenu.stats.sizeText.text = (c.radius * 2).ToString("f2");

            // set stat bars
            details.submenu.stats.hpBarPivot.transform.localScale = new Vector3(Mathf.Clamp01(c.hp / defaultCreep.hp / 4f), 1, 1);
            //details.submenu.stats.moneyBarPivot.transform.localScale = new Vector3(Mathf.Clamp01(c.moneyReward / defaultCreep.moneyReward / 4f), 1, 1);
            details.submenu.stats.speedBarPivot.transform.localScale = new Vector3(Mathf.Clamp01(c.speed / defaultCreep.speed / 4f), 1, 1);
            details.submenu.stats.countBarPivot.transform.localScale = new Vector3(Mathf.Clamp01(c.count / defaultCreep.count / 4f), 1, 1);
            //details.submenu.stats.spawnBarPivot.transform.localScale = new Vector3(Mathf.Clamp01(c.spawnRate / defaultCreep.spawnRate / 4f), 1, 1);
            details.submenu.stats.sizeBarPivot.transform.localScale = new Vector3(Mathf.Clamp01(c.radius / defaultCreep.radius / 4f), 1, 1);
        }

        void UpdateAttachmentSubMenu(ScenarioInstance s, CreepSquad squad) {
            UpdateLoadoutSlot(details.submenu.loadout.specialization, squad.loadout.specialization);
            UpdateLoadoutSlot(details.submenu.loadout.resource, squad.loadout.resource);

            UpdateLoadoutSlot(details.submenu.loadout.tier1_1, squad.loadout.tier1_1);
            UpdateLoadoutSlot(details.submenu.loadout.tier1_2, squad.loadout.tier1_2);
            UpdateLoadoutSlot(details.submenu.loadout.tier1_3, squad.loadout.tier1_3);

            UpdateLoadoutSlot(details.submenu.loadout.tier2_1, squad.loadout.tier2_1);
            UpdateLoadoutSlot(details.submenu.loadout.tier2_2, squad.loadout.tier2_2);

            UpdateLoadoutSlot(details.submenu.loadout.tier3_A, squad.loadout.tier3_A);
            UpdateLoadoutSlot(details.submenu.loadout.tier3_B, squad.loadout.tier3_B);


            void UpdateLoadoutSlot(ScenarioUI_CreepMenu_Details_AttachmentEntry_Behaviour entry, CreepLoadoutSlot slot) {
                var atch = slot.currentAttactment;

                if (atch.definition != null) {
                    entry.icon.sprite = atch.GetIcon();
                    entry.icon.gameObject.SetActive(true);
                }
                else {
                    entry.icon.gameObject.SetActive(false);
                }

                entry.SetSelected(false);
            }
        }

        void DeselectLoadoutSelection() {
            loadout.upgradePanel.Close();
            loadout.selectPanel.Close();

            details.submenu.loadout.specialization.SetSelected(false);
            details.submenu.loadout.resource.SetSelected(false);
            details.submenu.loadout.tier1_1.SetSelected(false);
            details.submenu.loadout.tier1_2.SetSelected(false);
            details.submenu.loadout.tier1_3.SetSelected(false);
            details.submenu.loadout.tier2_1.SetSelected(false);
            details.submenu.loadout.tier2_2.SetSelected(false);
            details.submenu.loadout.tier3_A.SetSelected(false);
            details.submenu.loadout.tier3_B.SetSelected(false);
        }

        void HighlightCreepSelection(CreepSquad s) {
            foreach (var e in creepEntries) {
                e.frameImage.sprite = e.squad == s ? e.selectedSprite : e.unselectedSprite;
            }
        }
        
        [System.Serializable]
        public struct CreepDetailsReferences {
            public RectTransform root;
            public Image creepImage;
            public Image creepGlowImage;

            public ButtonReferences buttons;
            public Submenu submenu;

            [System.Serializable]
            public struct ButtonReferences {
                [Header("Display")]
                public LPEButtonBehaviour squadParentButton;
                public LPEButtonBehaviour squadChildDeathButton;
                public LPEButtonBehaviour squadChildCarrierButton;

                public Image parentCreep;
                public Image childDeathCreep;
                public Image childCarrierCreep;

                public Image parentGlow;
                public Image childDeathGlow;
                public Image childCarrierGlow;

                [Header("Creep Selection")]
                public Image parentSelection;
                public Image childDeathSelection;
                public Image childCarrierSelection;

                public Sprite selectedSprite;
                public Sprite unselectedSprite;
                public Sprite unavailableSprite;

                public Sprite childDeathUnavailable;
                public Sprite childCarrierUnavailable;

                [Header("Sub Menu")]
                public LPEButtonBehaviour statsSubmenuButton;
                public LPEButtonBehaviour infoSubmenuButton;
                public LPEButtonBehaviour attachmentsSubmenuButton;
                public Image statsSubmenuSelection;
                public Image infoSubmenuSelection;
                public Image attachmentsSubmenuSelection;

            }

            [System.Serializable]
            public struct Submenu {
                public Stats stats;
                public Info info;
                public Loadout loadout;

                [System.Serializable]
                public struct Stats {
                    public GameObject rootObj;

                    public TextMeshProUGUI hpText;
                    public Transform hpBarPivot;
                    public TextMeshProUGUI moneyText;
                    public Transform moneyBarPivot;
                    public TextMeshProUGUI speedText;
                    public Transform speedBarPivot;
                    public TextMeshProUGUI countText;
                    public Transform countBarPivot;
                    public TextMeshProUGUI spawnRateText;
                    public Transform spawnBarPivot;
                    public TextMeshProUGUI sizeText;
                    public Transform sizeBarPivot;
                }
                [System.Serializable]
                public struct Info {
                    public GameObject rootObj;
                }

                [System.Serializable]
                public struct Loadout {
                    public GameObject rootObj;

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
        }
    }
}
