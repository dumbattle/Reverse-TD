using UnityEngine;
using LPE;


namespace Core {
    public class IconResourceCache {
        static LazyLoadResource<SpriteReferenceDictionary> iconDict = new LazyLoadResource<SpriteReferenceDictionary>("Sprites/Icons");

        public static Sprite moneyReward => iconDict.obj["Money Reward"];
    }
}
