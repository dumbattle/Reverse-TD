using UnityEngine;
using LPE;
using TMPro;


namespace Core {
    public class PreRoundUIBehaviour : MonoBehaviour { 
        public LPEButtonBehaviour creepButton;
        public LPEButtonBehaviour shopButton;

        [SerializeField] TextMeshProUGUI creepButtonText;
        [SerializeField] TextMeshProUGUI shopButtonText;

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
            CloseAllMenus();
        }

        public CreepSquad OpenCreepMenu(ScenarioInstance s) {
            CloseAllMenus();

            var result = creepMenu.Open(s);
            creepButtonText.color = Color.white;
            return result;
        }

        public void OpenShopMenu(ScenarioInstance s) {
            CloseAllMenus();
            shopMenu.Open(s);
            shopButtonText.color = Color.white;
        }

        public void CloseAllMenus() {
            creepMenu.Close();
            shopMenu.Close();
            creepButtonText.color = Color.black;
            shopButtonText.color = Color.black;
        }
    }
}
