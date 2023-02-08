using UnityEngine;
using UnityEngine.UI;
using LPE;
using TMPro;


namespace Core {
    public class GameUI : MonoBehaviour {
        [Header("HUD")]
        public LPEButtonBehaviour startButton;
        public Transform healthBarPivot;
        [Space]
        public TextMeshProUGUI gMoneyText;
        public TextMeshProUGUI rMoneyText;
        public TextMeshProUGUI bMoneyText;
        public TextMeshProUGUI yMoneyText;
        public TextMeshProUGUI dMoneyText;

        [Header("Menus")]
        public ScenarioUI_CreepMenu creepMenu;
        [Header("Other")]
        public EndRoundUnlockBehaviour endRoundUnlockBehaviour;
        public Image fadeOverlay;


        private void Awake() {
            startButton.gameObject.SetActive(false);
        }


        public void BeginRound() {
            startButton.gameObject.SetActive(false);
        }

        public void BeginPreRound() {
            startButton.gameObject.SetActive(true);
        }
    }

}
