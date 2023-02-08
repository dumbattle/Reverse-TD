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

        public static Sprite resourceGreen => iconDict.obj["Shard Green"];
        public static Sprite resourceRed => iconDict.obj["Shard Red"];
        public static Sprite resourceBlue => iconDict.obj["Shard Blue"];
        public static Sprite resourceYellow => iconDict.obj["Shard Yellow"];
        public static Sprite resourceDiamond => iconDict.obj["Shard Diamond"];
        public static Sprite resourceRBY => iconDict.obj["Shard RBY"];




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
