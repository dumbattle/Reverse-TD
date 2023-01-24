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

            // get bounds for HP, speed, spacing
            var c0 = creepArmy.GetSquad(0).actualDefinition;

            float hpMax = c0.hp;
            float hpMin = c0.hp;

            float spdMax = c0.speed;
            float spdMin = c0.speed;

            float spaceMin = c0.spawnInterval;
            float spaceMax = c0.spawnInterval;

            for (int i = 1; i < creepArmy.count; i++) {
                var c = creepArmy.GetSquad(i).actualDefinition;
                hpMax = Mathf.Max(c.hp, hpMax);
                hpMin = Mathf.Min(c.hp, hpMin);
                spdMax = Mathf.Max(c.speed, spdMax);
                spdMin = Mathf.Min(c.speed, spdMin);
                spaceMax = Mathf.Max(c.spawnInterval, spaceMax);
                spaceMin = Mathf.Min(c.spawnInterval, spaceMin);
            }
            // add buffer

            hpMax += 100;
            hpMin -= 100;
            spdMax += 1;
            spdMin -= 1;
            spaceMax += .5f;
            spaceMin -= .5f;

            var hpDif = hpMax - hpMin;
            var spdDif = spdMax - spdMin;
            var spaceDif = spaceMax - spaceMin;

            // set bars
            CreepSquad result = null;
            int entryIndex = 0;
            for (int i = 0; i < creepArmy.count; i++) {
                var squad = creepArmy.GetSquad(i);
                SetEntry(squad);
                SetEntry(squad.GetDeathSplitSquad());

                void SetEntry(CreepSquad sqd) {
                    if (sqd == null) {
                        return;
                    }
                    if (sqd == openTo) {
                        result = sqd;
                    }
                    if (creepEntries.Count <= entryIndex) {
                        var newE = Instantiate(creepEntrySrc, creepEntrySrc.transform.parent);
                        newE.button.SetClickListener(() => SelectCreepEntry(newE.squad));
                        creepEntries.Add(newE);

                    }
                    var e = creepEntries[entryIndex];
                    var c = sqd.actualDefinition;

                    e.icon.sprite = c.sprite;
                    e.gameObject.SetActive(true);
                    e.hpBarPivot.localScale = new Vector3(1, (c.hp - hpMin) / hpDif, 1);
                    e.spdBarPivot.localScale = new Vector3(1, (c.speed - spdMin) / spdDif, 1);
                    e.countBarPivot.localScale = new Vector3(1, 1 - (c.spawnInterval - spaceMin) / spaceDif, 1);
                    e.squad = sqd;
                    entryIndex++;
                }
            }

            // select first
            result = result ?? creepEntries[0].squad;
            SetCreepDetails(result);
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
            UpdateAttachmentDisplay();
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

        void UpdateAttachmentDisplay() {
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
