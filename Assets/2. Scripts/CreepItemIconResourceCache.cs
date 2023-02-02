using UnityEngine;
using LPE;


namespace Core {
    public static class CreepItemIconResourceCache {
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

        public static Sprite GemRed1 => iconDict.obj["Gem Red 1"];
        public static Sprite GemRed2 => iconDict.obj["Gem Red 2"];
        public static Sprite GemRed3 => iconDict.obj["Gem Red 3"];
        public static Sprite GemRed4 => iconDict.obj["Gem Red 4"];
        public static Sprite GemRed5 => iconDict.obj["Gem Red 5"];
        public static Sprite GemRed6 => iconDict.obj["Gem Red 6"];
        public static Sprite GemRed7 => iconDict.obj["Gem Red 7"];
        public static Sprite GemRed8 => iconDict.obj["Gem Red 8"];
        public static Sprite GemRed9 => iconDict.obj["Gem Red 9"];
        public static Sprite GemRed10 => iconDict.obj["Gem Red 10"];

        public static Sprite GemYellow1 => iconDict.obj["Gem Yellow 1"];
        public static Sprite GemYellow2 => iconDict.obj["Gem Yellow 2"];
        public static Sprite GemYellow3 => iconDict.obj["Gem Yellow 3"];
        public static Sprite GemYellow4 => iconDict.obj["Gem Yellow 4"];
        public static Sprite GemYellow5 => iconDict.obj["Gem Yellow 5"];
        public static Sprite GemYellow6 => iconDict.obj["Gem Yellow 6"];
        public static Sprite GemYellow7 => iconDict.obj["Gem Yellow 7"];
        public static Sprite GemYellow8 => iconDict.obj["Gem Yellow 8"];
        public static Sprite GemYellow9 => iconDict.obj["Gem Yellow 9"];
        public static Sprite GemYellow10 => iconDict.obj["Gem Yellow 10"];

        public static Sprite GemBlue1 => iconDict.obj["Gem Blue 1"];
        public static Sprite GemBlue2 => iconDict.obj["Gem Blue 2"];
        public static Sprite GemBlue3 => iconDict.obj["Gem Blue 3"];
        public static Sprite GemBlue4 => iconDict.obj["Gem Blue 4"];
        public static Sprite GemBlue5 => iconDict.obj["Gem Blue 5"];
        public static Sprite GemBlue6 => iconDict.obj["Gem Blue 6"];
        public static Sprite GemBlue7 => iconDict.obj["Gem Blue 7"];
        public static Sprite GemBlue8 => iconDict.obj["Gem Blue 8"];
        public static Sprite GemBlue9 => iconDict.obj["Gem Blue 9"];
        public static Sprite GemBlue10 => iconDict.obj["Gem Blue 10"];

        public static Sprite GemCyan1 => iconDict.obj["Gem Cyan 1"];
        public static Sprite GemCyan2 => iconDict.obj["Gem Cyan 2"];
        public static Sprite GemCyan3 => iconDict.obj["Gem Cyan 3"];
        public static Sprite GemCyan4 => iconDict.obj["Gem Cyan 4"];
        public static Sprite GemCyan5 => iconDict.obj["Gem Cyan 5"];
        public static Sprite GemCyan6 => iconDict.obj["Gem Cyan 6"];
        public static Sprite GemCyan7 => iconDict.obj["Gem Cyan 7"];
        public static Sprite GemCyan8 => iconDict.obj["Gem Cyan 8"];
        public static Sprite GemCyan9 => iconDict.obj["Gem Cyan 9"];
        public static Sprite GemCyan10 => iconDict.obj["Gem Cyan 10"];
    }
}
