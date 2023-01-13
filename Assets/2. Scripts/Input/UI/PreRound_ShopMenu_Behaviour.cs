using UnityEngine;


namespace Core {
    public class PreRound_ShopMenu_Behaviour : MonoBehaviour {
        public PreRound_ShopMenu_ItemEntry_Behaviour healthUpgradeItem;
        public PreRound_ShopMenu_ItemEntry_Behaviour refreshShopItem;
        public PreRound_ShopMenu_ItemEntry_Behaviour buyCreepItem;
        public PreRound_ShopMenu_ItemEntry_Behaviour attach1Slot;
        public PreRound_ShopMenu_ItemEntry_Behaviour attach2Slot;
        public PreRound_ShopMenu_ItemEntry_Behaviour attach3Slot;
        public PreRound_ShopMenu_ItemEntry_Behaviour attach4Slot;
        public PreRound_ShopMenu_ItemEntry_Behaviour attach5Slot;

        public GameObject itemMenuRoot;
        ScenarioInstance s;
        public void Open(ScenarioInstance s) {
            this.s = s;
            itemMenuRoot.SetActive(true);
            SetShop();
        }

        public void Close() {
            itemMenuRoot.SetActive(false);
        }

        void SetShop() {
            ShopInstance shop = s.playerFunctions.GetShop();
            healthUpgradeItem.Init(s, shop.globalHPItem, SetShop);
            refreshShopItem.Init(s, shop.rerollItem, SetShop);
            buyCreepItem.Init(s, shop.buyCreepItem, SetShop);
            attach1Slot.Init(s, shop.item1, SetShop);
            attach2Slot.Init(s, shop.item2, SetShop);
            attach3Slot.Init(s, shop.item3, SetShop);
            attach4Slot.Init(s, shop.item4, SetShop);
            attach5Slot.Init(s, shop.item5, SetShop);
        }
    }


}
