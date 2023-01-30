using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using LPE;

namespace Core {

    public class PreRound_CreepMenu_Behaviour : MonoBehaviour {
        [SerializeField] PreRoundCreepMenu_CreepEntry_Behaviour creepEntrySrc;
        [SerializeField] PreRoundCreepMenu_InventoryItemEntry_Behaviour itemEntrySrc;
        [SerializeField] CreepDetailsReferences creepDetailsReferences;
        [SerializeField] GameObject creepSelectionRoot;
        [SerializeField] GameObject itemSelectionRoot;
        [SerializeField] TextMeshProUGUI descriptionText;
        [SerializeField] Sprite deathChildSprite;
        [SerializeField] Sprite carrierChildSprite;

        List<PreRoundCreepMenu_CreepEntry_Behaviour> creepEntries = new List<PreRoundCreepMenu_CreepEntry_Behaviour>();
        List<PreRoundCreepMenu_InventoryItemEntry_Behaviour> itemEntries = new List<PreRoundCreepMenu_InventoryItemEntry_Behaviour>();
        CreepArmy creepArmy;
        ScenarioInstance s;
        CreepSquad currentSquad;

        public LPEButtonBehaviour applyAttachmentButton;
        public LPEButtonBehaviour removeAttachmentButton;

        public CreepSquad creepSelected { get; private set; }
        public int attachmentSelected { get; private set; }
        public CreepAttatchment itemSelected { get; private set; }

        void Awake() {
            applyAttachmentButton.gameObject.SetActive(false);
            removeAttachmentButton.gameObject.SetActive(false);
            creepEntrySrc.gameObject.SetActive(false);
            itemEntrySrc.gameObject.SetActive(false);

            for (int i = 0; i < creepDetailsReferences.attatchments.Count; i++) {
                int ind = i;
                creepDetailsReferences.attatchments[i].button.SetClickListener(() => SelectAttachmentSlot(ind));
            }
        }

        void LateUpdate() {
            itemSelected = null;
            creepSelected = null;
            attachmentSelected = -1;
        }

        //***********************************************************************************************************
        // Control
        //***********************************************************************************************************

        public CreepSquad Open(ScenarioInstance s, CreepSquad openTo = null) {
            descriptionText.text = "";
            applyAttachmentButton.gameObject.SetActive(false);
            removeAttachmentButton.gameObject.SetActive(false);
            creepSelectionRoot.SetActive(true);
            itemSelectionRoot.SetActive(false);
            creepArmy = s.playerFunctions.GetCreepArmy();
            this.s = s;
            gameObject.SetActive(true);


            CreepSquad result = null;
            int entryIndex = 0;

            for (int i = 0; i < creepArmy.count; i++) {
                // Get entry
                if (creepEntries.Count <= entryIndex) {
                    // new entry
                    var newE = Instantiate(creepEntrySrc, creepEntrySrc.transform.parent);
                    newE.button.SetClickListener(() => SelectCreepEntry(newE.squad));
                    creepEntries.Add(newE);
                }

                var e = creepEntries[entryIndex];
                // Cache
                var squad = creepArmy.GetSquad(i);
                var c = squad.actualDefinition;

                if (squad == openTo) {
                    result = squad;
                }

                // Set entry values
                e.icon.sprite = c.sprite;
                e.glowIcon.color = c.glowColor;
                e.iconRoot.localScale = new Vector3(c.radius * 2, c.radius * 2, 1);
                e.gameObject.SetActive(true);
                e.squad = squad;

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
                entryIndex++;
            }

            // select first
            result = result ?? creepEntries[0].squad;
            SetCreepDetails(result);
            DehighlightAllAttachmentSlots();
            return result;
        }

        public void Close() {
            gameObject.SetActive(false);
            foreach (var e in creepEntries) {
                e.gameObject.SetActive(false);
            }
        }
        
        public void SetCreepDetails(CreepSquad squad) {
            currentSquad = squad;
            var c = currentSquad.actualDefinition;

            // set image
            creepDetailsReferences.creepImage.sprite = c.sprite;

            // set stats
            UpdateCreepStatsDisplay();

            // set attachment images
            ResetAttachmentDisplay();

            // highlight button
            foreach (var e in creepEntries) {
                e.frameImage.sprite = e.squad == squad ? e.selectedSprite : e.unselectedSprite;
            }
        }

        public void CloseItemSelect() {
            // open proper menu
            creepSelectionRoot.SetActive(true);
            itemSelectionRoot.SetActive(false);

            Open(s, currentSquad);
        }
      
        public void OpenItemSelect() {
            // close existing entries
            foreach (var e in itemEntries) {
                e.gameObject.SetActive(false);
            }

            // open proper menu
            creepSelectionRoot.SetActive(false);
            itemSelectionRoot.SetActive(true);

            // set entries
            int entryIndex = 0;

            for (int i = 0; i < s.playerFunctions.GetPlayerInventoryCount(); i++) {
                var item = s.playerFunctions.GetPlayerItemBySlot(i);

                if (!(item is CreepAttatchment atch)) {
                    continue;
                }

                if (!atch.Attachable(currentSquad)) {
                    continue;
                }

                if (itemEntries.Count <= entryIndex) {
                    var newE = Instantiate(itemEntrySrc, itemEntrySrc.transform.parent);
                    newE.button.SetClickListener(() => ItemSelected(newE.atch));
                    itemEntries.Add(newE);

                }
                var e = itemEntries[entryIndex];

                e.icon.sprite = atch.GetIcon();
                e.nameText.text = atch.GetName();
                e.gameObject.SetActive(true);
                e.atch = atch;
                entryIndex++;
            }
        }

        public void HighlightAttachmentSlot(int slot) {
            DehighlightAllAttachmentSlots();
            creepDetailsReferences.attatchments[slot].SetSelected(true);
            SetDescriptionText(currentSquad.GetAttachment(slot).GetDescription());
            removeAttachmentButton.gameObject.SetActive(true);
        }

        public void DehighlightAllAttachmentSlots() {
            removeAttachmentButton.gameObject.SetActive(false);
            foreach (var a in creepDetailsReferences.attatchments) {
                a.SetSelected(false);
            }
            SetDescriptionText("");
        }

        public void SetDescriptionText(string txt) {
            descriptionText.text = txt;
        }
        
        public void ActivateAttachmentApplyButton(bool value) {
            applyAttachmentButton.gameObject.SetActive(value);
        }

        //***********************************************************************************************************
        // Button Callbacks
        //***********************************************************************************************************

        void SelectCreepEntry(CreepSquad s) {
            creepSelected = s;
        }

        void SelectAttachmentSlot(int slot) {
            attachmentSelected = slot;
        }

        void ItemSelected(CreepAttatchment item) {
            itemSelected = item;
        }

        //***********************************************************************************************************
        // Helpers
        //***********************************************************************************************************
      
        void UpdateCreepStatsDisplay() {
            var c = currentSquad.actualDefinition;

            creepDetailsReferences.hp.valueText.text = ((int)c.hp).ToString();
            creepDetailsReferences.money.valueText.text = ((int)c.moneyReward).ToString();
            creepDetailsReferences.speed.valueText.text = c.speed.ToString("f2");
            creepDetailsReferences.count.valueText.text = ((int)c.count).ToString();
            creepDetailsReferences.spawnRate.valueText.text = c.spawnRate.ToString("f2");
            creepDetailsReferences.size.valueText.text = (c.radius * 2).ToString("f2");

            var defaultSquad = s.playerFunctions.GetCreepArmy().defaultSquad;
            defaultSquad.Recalculate();
            var defaultCreep = defaultSquad.actualDefinition;
            creepDetailsReferences.hp.SetBars(c.hp, defaultCreep.hp);
            creepDetailsReferences.money.SetBars(c.moneyReward, defaultCreep.moneyReward);
            creepDetailsReferences.speed.SetBars(c.speed, defaultCreep.speed);
            creepDetailsReferences.count.SetBars(c.count, defaultCreep.count);
            creepDetailsReferences.spawnRate.SetBars(c.spawnRate, defaultCreep.spawnRate);
            creepDetailsReferences.size.SetBars(c.radius, defaultCreep.radius);

        }

        void ResetAttachmentDisplay() {
            int atchIndex = 0;
            int atchCount = currentSquad.NumAttachments();

            int numAttachInventory = s.playerFunctions.NumAttachableInInventory(currentSquad);
            foreach (var atchEntry in creepDetailsReferences.attatchments) {
                if (atchIndex < atchCount) {
                    var a = currentSquad.GetAttachment(atchIndex);
                    atchEntry.icon.sprite = a.GetIcon();
                    atchEntry.icon.gameObject.SetActive(true);
                    atchIndex++;

                }
                else if (numAttachInventory > 0) {
                    atchEntry.icon.sprite = IconResourceCache.availablePlus;
                    numAttachInventory--;
                    atchEntry.icon.gameObject.SetActive(true);
                }
                else {
                    atchEntry.icon.gameObject.SetActive(false);
                }
            }
        }
        
        [System.Serializable]
        struct CreepDetailsReferences {
            public Image creepImage;
            public Image creepGlowImage;
            public List<PreRoundCreepMenu_Details_AttachmentEntry_Behaviour> attatchments;

            public PreRound_CreepMenu_Details_StatEntry_Behaviour hp;
            public PreRound_CreepMenu_Details_StatEntry_Behaviour money;
            public PreRound_CreepMenu_Details_StatEntry_Behaviour speed;
            public PreRound_CreepMenu_Details_StatEntry_Behaviour count;
            public PreRound_CreepMenu_Details_StatEntry_Behaviour spawnRate;
            public PreRound_CreepMenu_Details_StatEntry_Behaviour size;
        }
    }
}
