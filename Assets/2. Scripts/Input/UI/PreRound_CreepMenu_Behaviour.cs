﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


namespace Core {
    public class PreRound_CreepMenu_Behaviour : MonoBehaviour {
        [SerializeField] PreRoundCreepMenu_CreepEntry_Behaviour creepEntrySrc;
        [SerializeField] PreRoundCreepMenu_InventoryItemEntry_Behaviour itemEntrySrc;
        [SerializeField] CreepDetailsReferences creepDetailsReferences;
        [SerializeField] GameObject creepSelectionRoot;
        [SerializeField] GameObject itemSelectionRoot;

        List<PreRoundCreepMenu_CreepEntry_Behaviour> creepEntries = new List<PreRoundCreepMenu_CreepEntry_Behaviour>();
        List<PreRoundCreepMenu_InventoryItemEntry_Behaviour> itemEntries = new List<PreRoundCreepMenu_InventoryItemEntry_Behaviour>();
        CreepArmy creepArmy;
        ScenarioInstance s;
        CreepSquad currentSquad;
       
        private void Awake() {
            creepEntrySrc.gameObject.SetActive(false);
            itemEntrySrc.gameObject.SetActive(false);

            for (int i = 0; i < creepDetailsReferences.attatchments.Count; i++) {
                creepDetailsReferences.attatchments[i].button.SetClickListener(() => SelectAttachmentSlot(i));
            }
        }

        public void Open(ScenarioInstance s) {
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

            float spaceMin = c0.spacing;
            float spaceMax = c0.spacing;

            for (int i = 1; i < creepArmy.count; i++) {
                var c = creepArmy.GetSquad(i).actualDefinition;
                hpMax = Mathf.Max(c.hp, hpMax);
                hpMin = Mathf.Min(c.hp, hpMin);
                spdMax = Mathf.Max(c.speed, spdMax);
                spdMin = Mathf.Min(c.speed, spdMin);
                spaceMax = Mathf.Max(c.spacing, spaceMax);
                spaceMin = Mathf.Min(c.spacing, spaceMin);
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
            for (int i = 0; i < creepArmy.count; i++) {
                if (creepEntries.Count <= i) {
                    int tempI = i; // don't capture 'i'
                    var newE = Instantiate(creepEntrySrc, creepEntrySrc.transform.parent);
                    newE.button.SetClickListener(() => SelectCreepEntry(tempI));
                    creepEntries.Add(newE);

                }
                var e = creepEntries[i];
                var c = creepArmy.GetSquad(i).actualDefinition;

                e.icon.sprite = c.sprite;
                e.gameObject.SetActive(true);
                e.hpBarPivot.localScale = new Vector3(1, (c.hp - hpMin)/ hpDif, 1);
                e.spdBarPivot.localScale = new Vector3(1, (c.speed - spdMin) / spdDif, 1);
                e.countBarPivot.localScale = new Vector3(1, 1 - (c.spacing - spaceMin) / spaceDif, 1);
            }

            // select first
            SelectCreepEntry(0);
        }

        public void Close() {
            gameObject.SetActive(false);
            foreach (var e in creepEntries) {
                e.gameObject.SetActive(false);
            }
        }
        
        //***********************************************************************************************************
        // Button Callbacks
        //***********************************************************************************************************

        void SelectCreepEntry(int i) {
            currentSquad = creepArmy.GetSquad(i);
            var c = currentSquad.actualDefinition;

            // set image
            creepDetailsReferences.creepImage.sprite = c.sprite;

            // set stats
            UpdateCreepStatsDisplay();

            // set attachment images
            UpdateAttachmentDisplay();
        }


        void SelectAttachmentSlot(int slot) {
            // Show available attachments in inventory
            UpdateItemSelectionMenu();
        }

        void ItemSelected(CreepAttatchment item) {
            s.playerFunctions.RemoveItem(item);
            currentSquad.AddModifier(item);
            UpdateCreepStatsDisplay();
            UpdateItemSelectionMenu();
            UpdateAttachmentDisplay();
        }

        //***********************************************************************************************************
        // Helpers
        //***********************************************************************************************************

        void UpdateCreepStatsDisplay() {
            var c = currentSquad.actualDefinition;

            creepDetailsReferences.hpText.text = ((int)c.hp).ToString();
            creepDetailsReferences.moneyText.text = ((int)c.moneyReward).ToString();
            creepDetailsReferences.speedText.text = c.speed.ToString("f2");
            creepDetailsReferences.countText.text = ((int)c.count).ToString();
            creepDetailsReferences.spacingText.text = c.spawnRate.ToString("f2");
            creepDetailsReferences.diameterText.text = (c.radius * 2).ToString("f2");
        }

        void UpdateItemSelectionMenu() {
            int numAttachInventory = s.playerFunctions.NumAttachableInInventory(currentSquad);

            if (numAttachInventory <= 0 || currentSquad.NumModifications() >= 10) {
                creepSelectionRoot.SetActive(true);
                itemSelectionRoot.SetActive(false);
                return;
            }

            foreach (var e in itemEntries) {
                e.gameObject.SetActive(false);
            }
            creepSelectionRoot.SetActive(false);
            itemSelectionRoot.SetActive(true);
            int entryIndex = 0;

            for (int i = 0; i < s.playerFunctions.GetPlayerInventoryCount(); i++) {
                var item = s.playerFunctions.GetPlayerItemBySlot(i);

                if (!(item is CreepAttatchment atch)) {
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

        void UpdateAttachmentDisplay() {
            int atchIndex = 0;
            int atchCount = currentSquad.NumModifications();

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

            public TextMeshProUGUI hpText;
            public TextMeshProUGUI moneyText;
            public TextMeshProUGUI speedText;
            public TextMeshProUGUI countText;
            public TextMeshProUGUI spacingText;
            public TextMeshProUGUI diameterText;
        }
    }
}