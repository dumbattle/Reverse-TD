using UnityEngine;
using LPE;


namespace Core {
    public class IconResourceCache {
        static LazyLoadResource<SpriteReferenceDictionary> iconDict = new LazyLoadResource<SpriteReferenceDictionary>("Sprites/Icons");
        static LazyLoadResource<SpriteReferenceDictionary> rankIconDict = new LazyLoadResource<SpriteReferenceDictionary>("Sprites/Rank Icons");

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
            "Rank 13",
            "Rank 14",
            "Rank 15",
            "Rank 16",
            "Rank 17",
            "Rank 18",
            "Rank 19",
            "Rank 20",
            "Rank 21",
            "Rank 22",
            "Rank 23",
            "Rank 24",
            "Rank 25",
            "Rank 26",
            "Rank 27",
            "Rank 28",
            "Rank 29",
            "Rank 30",
        };

        public static Sprite moneyReward => iconDict.obj["Money Reward"];
        public static Sprite availablePlus => iconDict.obj["Green Plus"];
        public static Sprite health => iconDict.obj["Health"];
        public static Sprite greenCheck => iconDict.obj["Green Check"];
        public static Sprite locked => iconDict.obj["Lock"];
        public static Sprite lockedDark => iconDict.obj["Lock Dark"];
        public static Sprite newCreep => iconDict.obj["New Creep"];


        public static Sprite creepAttachmentHP => iconDict.obj["Creep +HP"];
        public static Sprite creepAttachmentSpeed => iconDict.obj["Creep +Speed"];
        public static Sprite creepAttachmentMoney => iconDict.obj["Creep +Money"];
        public static Sprite creepAttachmentCount => iconDict.obj["Creep +Count"];
        public static Sprite creepAttachmentGroup => iconDict.obj["Creep +Grouping"];
        public static Sprite creepAttachmentRegen => iconDict.obj["Creep +Regen"];

        public static Sprite creepAttachment_TankShift => iconDict.obj["Creep Tank Shift"];
        public static Sprite creepAttachment_SpeedShift => iconDict.obj["Creep Speed Shift"];

        public static Sprite Rank(int rank) {
            if (rank <= 0) {
                return null;
            }
            rank--;
            if (rank >= ranks.Length) {
                rank = ranks.Length - 1;
            }
            return rankIconDict.obj[ranks[rank]];
        }
    }
}
