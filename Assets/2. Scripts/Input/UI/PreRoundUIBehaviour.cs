using UnityEngine;
using LPE;


namespace Core {
    public class PreRoundUIBehaviour : MonoBehaviour { 
        public LPEButtonBehaviour creepButton;
        public LPEButtonBehaviour shopButton;

        public PreRound_CreepMenu_Behaviour creepMenu;
        public PreRound_ShopMenu_Behaviour shopMenu;


        public void Close() {
            creepButton.gameObject.SetActive(false);
            shopButton.gameObject.SetActive(false);
            creepMenu.Close();
            shopMenu.Close();
        }

        public void Open() {
            creepButton.gameObject.SetActive(true);
            shopButton.gameObject.SetActive(true);
        }
    }
}
