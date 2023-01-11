using UnityEngine;
using LPE;


namespace Core {
    public class IconResourceCache {
        static LazyLoadResource<SpriteReferenceDictionary> iconDict = new LazyLoadResource<SpriteReferenceDictionary>("Sprites/Icons");

        public static Sprite moneyReward => iconDict.obj["Money Reward"];
        public static Sprite availablePlus=> iconDict.obj["Green Plus"];


        public static Sprite creepAttachmentHP => iconDict.obj["Creep +HP"];
        public static Sprite creepAttachmentSpeed => iconDict.obj["Creep +Speed"];
        public static Sprite creepAttachmentMoney => iconDict.obj["Creep +Money"];
        public static Sprite creepAttachmentCount => iconDict.obj["Creep +Count"];
    }
}
