using UnityEngine;
using LPE;


namespace Core {
    public class CreepItemIconResourceCache {
        static LazyLoadResource<SpriteReferenceDictionary> iconDict = new LazyLoadResource<SpriteReferenceDictionary>("Sprites/Creep Items Icons");


        public static Sprite basicHp => iconDict.obj["Creep +HP"];
        public static Sprite basicSpeed => iconDict.obj["Creep +Speed"];
        public static Sprite basicMoney => iconDict.obj["Creep +Money"];
        public static Sprite basicCount => iconDict.obj["Creep +Count"];
        public static Sprite basicSpawnRate => iconDict.obj["Creep +Grouping"];
        public static Sprite basicRegen => iconDict.obj["Creep +Regen"];

        public static Sprite speedGreedy => iconDict.obj["Creep Speed Greedy"];
        public static Sprite shiftSpeed2Hp => iconDict.obj["Creep Shift spd hp"];
        public static Sprite shiftCount2Hp => iconDict.obj["Creep Shift count hp"];
        public static Sprite shiftCount2Spd => iconDict.obj["Creep Shift count spd"];
        public static Sprite shiftHp2Speed => iconDict.obj["Creep Shift hp spd"];
        public static Sprite shiftHp2Count => iconDict.obj["Creep Shift hp count"];
        public static Sprite shiftSpeed2Count => iconDict.obj["Creep Shift spd count"];
    }
}
