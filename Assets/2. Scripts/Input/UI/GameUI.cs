using UnityEngine;
using LPE;
using TMPro;


namespace Core {
    public class GameUI : MonoBehaviour {
        public LPEButtonBehaviour startButton;
        public EndRoundUnlockBehaviour endRoundUnlockBehaviour;
        public TextMeshProUGUI moneyText;
        public PreRoundUIBehaviour preRoundBehaviour;


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
