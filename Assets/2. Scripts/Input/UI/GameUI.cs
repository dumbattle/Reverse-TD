using UnityEngine;
using LPE;


namespace Core {
    public class GameUI : MonoBehaviour {
        public LPEButtonBehaviour startButton;

        public void BeginRound() {
            startButton.gameObject.SetActive(false);
        }

        public void BeginPreRound() {
            startButton.gameObject.SetActive(true);
        }
    }
}
