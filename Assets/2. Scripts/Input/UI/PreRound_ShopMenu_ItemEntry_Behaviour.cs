using UnityEngine;
using UnityEngine.UI;
using LPE;
using TMPro;


namespace Core {
    public class PreRound_ShopMenu_ItemEntry_Behaviour : MonoBehaviour {
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI costText;
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] LPEButtonBehaviour button;
        [SerializeField] ShopItem shopItem;

        [SerializeField] Image buttonImage;
        [SerializeField] Color buttonAffordColor;
        [SerializeField] Color buttonTooPoorColor;
        [SerializeField] Color costAffordColor;
        [SerializeField] Color costTooPoorColor;

        ScenarioInstance s;
        System.Action shopRefresh;

        private void Awake() {
            button.SetDownListener(() => {
                if (shopItem.purchaseCallback == null) {
                    return;
                }
                if (!CanAfford()) {
                    return;
                }
                s.playerFunctions.AddMoney(-shopItem.cost);
                shopItem.purchaseCallback.OnPurchase();
                shopRefresh();
            });
        }
        public void Init(ScenarioInstance s, ShopItem shopItem, System.Action shopRefresh) {
            this.s = s;
            this.shopRefresh = shopRefresh;


            icon.sprite = shopItem.icon;
            costText.text = shopItem.cost.ToString();
            nameText.text = shopItem.name;
            this.shopItem = shopItem;

            if (shopItem.cost == -1) {
                buttonImage.color = Color.grey;
                costText.color = Color.grey; ;
            }
            else if (CanAfford()) {
                buttonImage.color = buttonAffordColor;
                costText.color = costAffordColor;
            }
            else {
                buttonImage.color = buttonTooPoorColor;
                costText.color = costTooPoorColor;
            }
        }

        bool CanAfford() {
            return shopItem.cost <= s.playerFunctions.CurrentMoney();
        }
    }
}
