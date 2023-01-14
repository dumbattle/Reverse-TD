namespace Core {
    public interface IShopItemCallback {
        /// <summary>
        /// Do not deduct money from player
        /// </summary>
        void OnPurchase();
    }
}
