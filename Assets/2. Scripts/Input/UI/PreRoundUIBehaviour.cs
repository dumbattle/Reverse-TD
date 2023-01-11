using UnityEngine;
using LPE;


namespace Core {
    public class PreRoundUIBehaviour : MonoBehaviour { 
        public LPEButtonBehaviour creepButton;
        public PreRoundCreepMenuBehaviour creepMenu;

        public void Close() {
            creepButton.gameObject.SetActive(false);
            creepMenu.gameObject.SetActive(false);
        }

        public void Open() {
            creepButton.gameObject.SetActive(true);
        }
    }


}
