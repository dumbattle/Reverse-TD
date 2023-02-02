namespace Core {
    public class ShopInstance {
        public int rerollCount { get; private set; }
        public int hpBuffCount { get; private set; } = 0;

        public ShopItem globalHPItem { get; private set; }
        public ShopItem rerollItem { get; private set; }
        public ShopItem buyCreepItem => buyCreepCallback.shopItem;
        public ShopItem item1 => attachmentSlot_1.shopItem;
        public ShopItem item2 => attachmentSlot_2.shopItem;
        public ShopItem item3 => attachmentSlot_3.shopItem;
        public ShopItem item4 => attachmentSlot_4.shopItem;
        public ShopItem item5 => attachmentSlot_5.shopItem;

        HPCallback hpCallback;
        RerollCallback rerollCallback;
        BuyCreepCallback buyCreepCallback;

        AttachmentSlot attachmentSlot_1;
        AttachmentSlot attachmentSlot_2;
        AttachmentSlot attachmentSlot_3;
        AttachmentSlot attachmentSlot_4;
        AttachmentSlot attachmentSlot_5;

        public void Init(ScenarioInstance s) {
            hpCallback = new HPCallback();
            hpCallback.s = s;
            hpCallback.shop = this;
            rerollCallback = new RerollCallback();
            rerollCallback.s = s;
            rerollCallback.shop = this;
            buyCreepCallback = new BuyCreepCallback(s);
            attachmentSlot_1 = new AttachmentSlot(s, 0, null, this);
            attachmentSlot_2 = new AttachmentSlot(s, 100, attachmentSlot_1, this);
            attachmentSlot_3 = new AttachmentSlot(s, 250, attachmentSlot_2, this);
            attachmentSlot_4 = new AttachmentSlot(s, 500, attachmentSlot_3, this);
            attachmentSlot_5 = new AttachmentSlot(s, 1000, attachmentSlot_4, this);
            Reroll();
            buyCreepCallback.Set();
        }


        public void Refresh(ScenarioInstance s) {
            rerollCount = 0;
            attachmentSlot_1.Roll();
            attachmentSlot_2.Roll();
            attachmentSlot_3.Roll();
            attachmentSlot_4.Roll();
            attachmentSlot_5.Roll();
            SetHPItem();
            SetRerollItem();
            buyCreepCallback.Set();
        }

        public void Reroll() {
            rerollCount++;
            SetRerollItem();
            SetHPItem();
            attachmentSlot_1.Roll();
            attachmentSlot_2.Roll();
            attachmentSlot_3.Roll();
            attachmentSlot_4.Roll();
            attachmentSlot_5.Roll();
        }
      
        public void OnUnlockAttachmentSlot() {
            attachmentSlot_1.Set();
            attachmentSlot_2.Set();
            attachmentSlot_3.Set();
            attachmentSlot_4.Set();
            attachmentSlot_5.Set();
        }


        void SetHPItem() {
            globalHPItem = new ShopItem() {
                icon = IconResourceCache.health,
                name = $"Creep Health +{hpBuffCount + 1}",
                cost = 100 + hpBuffCount * 5,
                purchaseCallback = hpCallback
            };
        }
        void SetRerollItem() {
            rerollItem = new ShopItem() {
                icon = IconResourceCache.health,
                name = $"Refresh Shop",
                cost = 25 + rerollCount * 25,
                purchaseCallback = rerollCallback
            };
        }

        class HPCallback : IShopItemCallback {
            public ScenarioInstance s;
            public ShopInstance shop;

            public void OnPurchase() {
                shop.hpBuffCount++;
                s.playerFunctions.GetGlobalCreeepUpgrades().hpScale = 1f + shop.hpBuffCount / 20f;

                for (int i = 0; i < s.playerFunctions.GetCreepArmy().count; i++) {
                    var squad = s.playerFunctions.GetCreepArmy().GetSquad(i);
                    squad.Recalculate();
                }
                shop.SetHPItem();
            }
        }
        
        class BuyCreepCallback : IShopItemCallback {
            ScenarioInstance s;
            public ShopItem shopItem;

            public BuyCreepCallback(ScenarioInstance s) {
                this.s = s ;
            }

            public void OnPurchase() {
                var newCreep = CreepSelectionUtility.GetRandomNewCreep();
                s.playerFunctions.GetCreepArmy().AddNewSquad(newCreep);
                Set();
            }

            public void Set() {
                int cCount = s.playerFunctions.GetCreepArmy().count - 1;
                shopItem = new ShopItem() {
                    icon = IconResourceCache.newCreep,
                    name = $"New Squad",
                    cost = 100 + cCount * cCount  * 50 + cCount * 25,
                    purchaseCallback = this
                };
            }
        }

        class RerollCallback : IShopItemCallback {
            public ScenarioInstance s;
            public ShopInstance shop;

            public void OnPurchase() {
                shop.Reroll();
            }
        }
        
        class AttachmentSlot : IShopItemCallback {
            public bool unlocked;
            public int unlockCost;
            public IPlayerItem item;

            public ShopItem shopItem;
            public ScenarioInstance s;
            public AttachmentSlot prereq;
            public ShopInstance shop;

            bool purchased;

            public AttachmentSlot(ScenarioInstance s, int unlockCost, AttachmentSlot unlockPrereq, ShopInstance shop) {
                if (unlockPrereq == null) {
                    unlocked = true;
                }
                else {
                    unlocked = false;
                }
                this.shop = shop;
                this.unlockCost = unlockCost;
                this.s = s;
                prereq = unlockPrereq;
            }


            public void OnPurchase() {
                purchased = true;
                if (!unlocked) {
                    unlocked = true;
                    Roll();
                    shop.OnUnlockAttachmentSlot();
                    return;
                }

                s.playerFunctions.AddItem(item);

                shopItem = new ShopItem() {
                    icon =IconResourceCache.greenCheck,
                    name = "Purchased",
                    cost = -1,
                    purchaseCallback = null
                };
            }
        
            public void Roll() {
                purchased = false;
                item = PlayerItemUtility.GetRandomItem(1);
                Set();
            }
     
            public void Set() {
                if (!unlocked) {
                    int c = prereq.unlocked ? unlockCost : -1;
                    var icon = prereq.unlocked ? IconResourceCache.locked : IconResourceCache.lockedDark;
                    shopItem = new ShopItem() {
                        icon = icon,
                        name = "Locked",
                        cost = c,
                        purchaseCallback = this
                    };
                    return;
                }
                else if (!purchased) {
                    //shopItem = new ShopItem() {
                    //    icon = item.GetIcon(),
                    //    name = item.GetName(),
                    //    cost = 100,
                    //    purchaseCallback = this
                    //};
                }
                else {

                    shopItem = new ShopItem() {
                        icon = IconResourceCache.greenCheck,
                        name = "Purchased",
                        cost = -1,
                        purchaseCallback = null
                    };
                }
            }
        }
    }
}
