using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameUI.CreepMenus;
using GameUI.CreepMenus.GameStates;



namespace Core {
    public class CreepMenu : MonoBehaviour {
        //************************************************************************************
        // References
        //************************************************************************************


        //--------------------------------------------------------
        // Sub panels (bottom)
        //--------------------------------------------------------

        public CreepStatPanel statEntries;
        public CreepLoadoutPanel loadoutPanel;

        //--------------------------------------------------------
        // Data (Top Right)
        //--------------------------------------------------------

        public CreepPurchasePanel purchasePanel;

        //--------------------------------------------------------
        // Other
        //--------------------------------------------------------

        public CreepMenu_SubmenuButtons submenuButtons;
        public CreepMenu_CreepSelection selections;


        public Image creepIcon;
        public Image creepGlow;

        //************************************************************************************
        // State
        //************************************************************************************

        public CreepSquad currentSquad { get; private set; }

        //************************************************************************************
        // Unity
        //************************************************************************************

        private void Awake() {
            loadoutPanel.Init();
        }
        //************************************************************************************
        // Control
        //************************************************************************************

        public IFSM_State<ScenarioInstance> GetGameState(ScenarioInstance s) {
            return CreepMenu_Open_GameState.Get(this);
        }


        public void Open(ScenarioInstance s) {
            selections.SetCreepMenu(this);
            selections.SetArmy(s.playerFunctions.GetCreepArmy());
            if (currentSquad == null) {
                SetSelectedSquad(s.playerFunctions.GetCreepArmy().GetSquad(0));
            }

            gameObject.SetActive(true);
            selections.SetPageToCreep(currentSquad);
        }

        public void Close() {
            gameObject.SetActive(false);
        }
        public void CloseAllSubMenus() {
            statEntries.Close();
            loadoutPanel.Close();
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
