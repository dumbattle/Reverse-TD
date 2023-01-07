using UnityEngine;
using LPE;
using TMPro;


namespace Core {
    public class GameUI : MonoBehaviour {
        public LPEButtonBehaviour startButton;
        public EndRoundUnlockBehaviour endRoundUnlockBehaviour;
        public TextMeshProUGUI moneyText;


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
