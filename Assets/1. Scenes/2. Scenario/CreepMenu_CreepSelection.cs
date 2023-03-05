using System;
using UnityEngine;
using UnityEngine.UI;
using LPE;
using TMPro;



namespace Core {
    [Serializable]
    public class CreepMenu_CreepSelection {
        [SerializeField] Sprite buySprite;
        [SerializeField] Sprite frameSelected;
        [SerializeField] Sprite frameUnselected;
        [SerializeField] Entry[] entries;
        [SerializeField] LPEButtonBehaviour upButton;
        [SerializeField] LPEButtonBehaviour downButton;


        int page = 0;
        CreepArmy army;
        CreepMenu mainCreepMenu;

        public void SetCreepMenu(CreepMenu c) {
            mainCreepMenu = c;
        }

        public void SetArmy(CreepArmy army) {
            this.army = army;
        }

        public void ReDraw() {
            ValidatePage();
            int startIndex = page * entries.Length;

            for (int i = 0; i < entries.Length; i++) {
                var index = startIndex + i;
                var e = entries[i];

                if (index < army.count) {
                    var s = army.GetSquad(index);
                    e.DrawSquad(s, index + 1);
                    e.frame.sprite = s == mainCreepMenu.currentSquad ? frameSelected : frameUnselected;
                }
                else {
                    e.DrawEmpty(buySprite, index + 1);
                    e.frame.sprite = frameUnselected;
                }
            }
        }

        public void MovePageUp() {
            page--;
            ReDraw();
        }

        public void MovePageDown() {
            page++;
            ReDraw();
        }

        public void SetPageToCreep(CreepSquad squad) {
            for (int i = 0; i < army.count; i++) {
                var s = army.GetSquad(i);

                if (s == squad) {
                    page = i / entries.Length;
                    break;
                }
            }
            ReDraw();
        }


        void ValidatePage() {
            if (page < 0) {
                page = 0;
                return;
            }
            int pageSize = entries.Length;
            int squadCount = army.count;

            int maxPage = squadCount / pageSize;

            if (page > maxPage) {
                page = maxPage;
            }
        }


        public CreepMenu_CreepSelection_InputAction GetInputAction() {
            // check squad buttons
            foreach (var e in entries) {
                if (!e.button.Clicked) {
                    continue;
                }

                return
                    e.squad != null 
                    ? CreepMenu_CreepSelection_InputAction.SquadSelect(e.squad) 
                    : CreepMenu_CreepSelection_InputAction.EmptySelect();
            }

            // check up
            if (upButton.Clicked) {
                return CreepMenu_CreepSelection_InputAction.Up();
            }

            // check down
            if (downButton.Clicked) {
                return CreepMenu_CreepSelection_InputAction.Down();
            }

            return CreepMenu_CreepSelection_InputAction.None();
        }


        [Serializable]
        class Entry {
            public LPEButtonBehaviour button;
            public Image frame;
            public Image creepIcon;
            public Image creepGlow;
            public TextMeshProUGUI numberText;
            public CreepSquad squad;

            public void DrawSquad(CreepSquad s, int label) {
                creepGlow.gameObject.SetActive(true);
                creepIcon.sprite = s.actualDefinition.sprite;
                creepGlow.color = s.actualDefinition.glowColor;
                numberText.text = label.ToString();
                squad = s;
            }

            public void DrawEmpty(Sprite buySprite, int label) {
                creepIcon.sprite = buySprite;
                creepGlow.gameObject.SetActive(false);
                numberText.text = label.ToString();
                squad = null;
            }

        }
    }

}
