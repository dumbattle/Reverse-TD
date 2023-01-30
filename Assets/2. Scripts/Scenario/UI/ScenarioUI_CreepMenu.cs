using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using LPE;
using TMPro;

namespace Core {
    public class ScenarioUI_CreepMenu : MonoBehaviour, IPointerDownHandler {
        [SerializeField] ScrollRect scrollRect;
        List<PreRoundCreepMenu_CreepEntry_Behaviour> creepEntries = new List<PreRoundCreepMenu_CreepEntry_Behaviour>();
        [SerializeField] PreRoundCreepMenu_CreepEntry_Behaviour creepEntrySrc;


        public LPEButtonBehaviour buyCreepButton;
        public TextMeshProUGUI butCostText;

        //*************************************************************************************************************
        // Unity Methods
        //*************************************************************************************************************
        void Start() {
            creepEntrySrc.gameObject.SetActive(false);
            creepEntrySrc.gameObject.SetActive(false);
            buyCreepButton.SetDownListener(InputManager.Set.ButtonDown);
        }

        // Update is called once per frame
        void Update() {

        }

        public void OnPointerDown(PointerEventData eventData) {
            InputManager.Set.ButtonDown();
        }


        //*************************************************************************************************************
        // Control
        //*************************************************************************************************************
        public void ReDraw(ScenarioInstance s) {
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
        }


        //***********************************************************************************************************
        // Button Callbacks
        //***********************************************************************************************************

        void SelectCreepEntry(CreepSquad s) {
            
        }

    }
}
