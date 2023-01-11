using UnityEngine;
using LPE;


namespace Core {
    public class IconResourceCache {
        static LazyLoadResource<SpriteReferenceDictionary> iconDict = new LazyLoadResource<SpriteReferenceDictionary>("Sprites/Icons");

        static string[] ranks = { 
            "Rank 1",
            "Rank 2",
            "Rank 3",
            "Rank 4",
            "Rank 5",
            "Rank 6",
            "Rank 7",
            "Rank 8",
            "Rank 9",
            "Rank 10",
            "Rank 11",
            "Rank 12",
        };

        public static Sprite moneyReward => iconDict.obj["Money Reward"];
        public static Sprite availablePlus=> iconDict.obj["Green Plus"];


        public static Sprite creepAttachmentHP => iconDict.obj["Creep +HP"];
        public static Sprite creepAttachmentSpeed => iconDict.obj["Creep +Speed"];
        public static Sprite creepAttachmentMoney => iconDict.obj["Creep +Money"];
        public static Sprite creepAttachmentCount => iconDict.obj["Creep +Count"];

        public static Sprite Rank(int rank) {
            if (rank <= 0) {
                return null;
            }
            rank--;
            if (rank >= ranks.Length) {
                rank = ranks.Length - 1;
            }
            return iconDict.obj[ranks[rank]];
        }
    }
}
