using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameUI.CreepMenus;
using GameUI.CreepMenus.GameStates;


namespace Core {

    public class CreepMenu : MonoBehaviour {
        public CreepMenu_CreepSelection selections;
        public CreepStatEntries statEntries;
        public CreepPurchasePanel purchasePanel;

        public Image creepIcon;
        public Image creepGlow;

        public CreepSquad currentSquad { get; private set; }

        public IFSM_State<ScenarioInstance> GetGameState(ScenarioInstance s) {
            return CreepMenu_Open_GameState.Get(this);
        }


        public void Open(ScenarioInstance s) {
            gameObject.SetActive(true);
            selections.SetCreepMenu(this);
            selections.SetArmy(s.playerFunctions.GetCreepArmy());
        }

        public void Close() {
            gameObject.SetActive(false);
        }


        public void SetSelectedSquad (CreepSquad s) {
            currentSquad = s;
            if (s == null) {
                creepIcon.gameObject.SetActive(false);
                creepGlow.gameObject.SetActive(false);
            }
            else {
                creepIcon.gameObject.SetActive(true);
                creepGlow.gameObject.SetActive(true);
                creepIcon.sprite = s.actualDefinition.sprite;
                creepGlow.color = s.actualDefinition.glowColor;
            }
            selections.ReDraw();
        }
    }
}
