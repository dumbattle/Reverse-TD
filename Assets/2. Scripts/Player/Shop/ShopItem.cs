using UnityEngine;


namespace Core {
    public struct ShopItem {
        public Sprite icon;
        public string name;
        public int cost;

        public IShopItemCallback purchaseCallback;
    }
}
