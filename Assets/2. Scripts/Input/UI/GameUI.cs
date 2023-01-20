using UnityEngine;
using UnityEngine.UI;
using LPE;
using TMPro;


namespace Core {
    public class GameUI : MonoBehaviour {
        [Header("HUD")]
        public LPEButtonBehaviour startButton;
        public TextMeshProUGUI moneyText;
        public Transform healthBarPivot;

        [Header("Menus")]
        public PreRoundUIBehaviour preRoundBehaviour;
        [Header("Other")]
        public EndRoundUnlockBehaviour endRoundUnlockBehaviour;
        public Image fadeOverlay;


        private void Awake() {
            startButton.gameObject.SetActive(false);
            preRoundBehaviour.Close();
        }


        public void BeginRound() {
            startButton.gameObject.SetActive(false);
            preRoundBehaviour.Close();
        }

        public void BeginPreRound() {
            startButton.gameObject.SetActive(true);
            preRoundBehaviour.Open();
        }
    }

}
