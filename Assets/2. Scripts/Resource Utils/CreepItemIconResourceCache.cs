using UnityEngine;
using LPE;


namespace Core {

    public static class CreepItemIconResourceCache {
        static LazyLoadResource<SpriteReferenceDictionary> tier1Dict
            = new LazyLoadResource<SpriteReferenceDictionary>("Sprites/Creep Items Tier 1");
        static LazyLoadResource<SpriteReferenceDictionary> specializationDict
            = new LazyLoadResource<SpriteReferenceDictionary>("Sprites/Creep Items Specialization");


        public static Sprite basicHp => tier1Dict.obj["Creep +HP"];
        public static Sprite basicSpeed => tier1Dict.obj["Creep +Speed"];
        public static Sprite basicMoney => tier1Dict.obj["Creep +Money"];
        public static Sprite basicCount => tier1Dict.obj["Creep +Count"];
        public static Sprite basicSpawnRate => tier1Dict.obj["Creep +Grouping"];
        public static Sprite basicRegen => tier1Dict.obj["Creep +Regen"];

        public static Sprite speedGreedy => tier1Dict.obj["Creep Speed Greedy"];
        public static Sprite shiftSpeed2Hp => tier1Dict.obj["Creep Shift spd hp"];
        public static Sprite shiftCount2Hp => tier1Dict.obj["Creep Shift count hp"];
        public static Sprite shiftCount2Spd => tier1Dict.obj["Creep Shift count spd"];
        public static Sprite shiftHp2Speed => tier1Dict.obj["Creep Shift hp spd"];
        public static Sprite shiftHp2Count => tier1Dict.obj["Creep Shift hp count"];
        public static Sprite shiftSpeed2Count => tier1Dict.obj["Creep Shift spd count"];

        public static Sprite GemRed1 => tier1Dict.obj["Gem Red 1"];
        public static Sprite GemRed2 => tier1Dict.obj["Gem Red 2"];
        public static Sprite GemRed3 => tier1Dict.obj["Gem Red 3"];
        public static Sprite GemRed4 => tier1Dict.obj["Gem Red 4"];
        public static Sprite GemRed5 => tier1Dict.obj["Gem Red 5"];
        public static Sprite GemRed6 => tier1Dict.obj["Gem Red 6"];
        public static Sprite GemRed7 => tier1Dict.obj["Gem Red 7"];
        public static Sprite GemRed8 => tier1Dict.obj["Gem Red 8"];
        public static Sprite GemRed9 => tier1Dict.obj["Gem Red 9"];
        public static Sprite GemRed10 => tier1Dict.obj["Gem Red 10"];

        public static Sprite GemYellow1 => tier1Dict.obj["Gem Yellow 1"];
        public static Sprite GemYellow2 => tier1Dict.obj["Gem Yellow 2"];
        public static Sprite GemYellow3 => tier1Dict.obj["Gem Yellow 3"];
        public static Sprite GemYellow4 => tier1Dict.obj["Gem Yellow 4"];
        public static Sprite GemYellow5 => tier1Dict.obj["Gem Yellow 5"];
        public static Sprite GemYellow6 => tier1Dict.obj["Gem Yellow 6"];
        public static Sprite GemYellow7 => tier1Dict.obj["Gem Yellow 7"];
        public static Sprite GemYellow8 => tier1Dict.obj["Gem Yellow 8"];
        public static Sprite GemYellow9 => tier1Dict.obj["Gem Yellow 9"];
        public static Sprite GemYellow10 => tier1Dict.obj["Gem Yellow 10"];

        public static Sprite GemBlue1 => tier1Dict.obj["Gem Blue 1"];
        public static Sprite GemBlue2 => tier1Dict.obj["Gem Blue 2"];
        public static Sprite GemBlue3 => tier1Dict.obj["Gem Blue 3"];
        public static Sprite GemBlue4 => tier1Dict.obj["Gem Blue 4"];
        public static Sprite GemBlue5 => tier1Dict.obj["Gem Blue 5"];
        public static Sprite GemBlue6 => tier1Dict.obj["Gem Blue 6"];
        public static Sprite GemBlue7 => tier1Dict.obj["Gem Blue 7"];
        public static Sprite GemBlue8 => tier1Dict.obj["Gem Blue 8"];
        public static Sprite GemBlue9 => tier1Dict.obj["Gem Blue 9"];
        public static Sprite GemBlue10 => tier1Dict.obj["Gem Blue 10"];

        public static Sprite GemCyan1 => tier1Dict.obj["Gem Cyan 1"];
        public static Sprite GemCyan2 => tier1Dict.obj["Gem Cyan 2"];
        public static Sprite GemCyan3 => tier1Dict.obj["Gem Cyan 3"];
        public static Sprite GemCyan4 => tier1Dict.obj["Gem Cyan 4"];
        public static Sprite GemCyan5 => tier1Dict.obj["Gem Cyan 5"];
        public static Sprite GemCyan6 => tier1Dict.obj["Gem Cyan 6"];
        public static Sprite GemCyan7 => tier1Dict.obj["Gem Cyan 7"];
        public static Sprite GemCyan8 => tier1Dict.obj["Gem Cyan 8"];
        public static Sprite GemCyan9 => tier1Dict.obj["Gem Cyan 9"];
        public static Sprite GemCyan10 => tier1Dict.obj["Gem Cyan 10"];

        public static Sprite GemBlueOutlineRed1 => specializationDict.obj["Blue Red 0"];
        public static Sprite GemBlueOutlineRed2 => specializationDict.obj["Blue Red 1"];
        public static Sprite GemBlueOutlineRed3 => specializationDict.obj["Blue Red 2"];
        public static Sprite GemBlueOutlineRed4 => specializationDict.obj["Blue Red 3"];
        public static Sprite GemBlueOutlineRed5 => specializationDict.obj["Blue Red 4"];

        public static Sprite GemBlueOutlineYellow1 => specializationDict.obj["Blue Yellow 0"];
        public static Sprite GemBlueOutlineYellow2 => specializationDict.obj["Blue Yellow 1"];
        public static Sprite GemBlueOutlineYellow3 => specializationDict.obj["Blue Yellow 2"];
        public static Sprite GemBlueOutlineYellow4 => specializationDict.obj["Blue Yellow 3"];
        public static Sprite GemBlueOutlineYellow5 => specializationDict.obj["Blue Yellow 4"];

        public static Sprite GemRedOutlineBlue1 => specializationDict.obj["Red Blue 0"];
        public static Sprite GemRedOutlineBlue2 => specializationDict.obj["Red Blue 1"];
        public static Sprite GemRedOutlineBlue3 => specializationDict.obj["Red Blue 2"];
        public static Sprite GemRedOutlineBlue4 => specializationDict.obj["Red Blue 3"];
        public static Sprite GemRedOutlineBlue5 => specializationDict.obj["Red Blue 4"];

        public static Sprite GemRedOutlineYellow1 => specializationDict.obj["Red Yellow 0"];
        public static Sprite GemRedOutlineYellow2 => specializationDict.obj["Red Yellow 1"];
        public static Sprite GemRedOutlineYellow3 => specializationDict.obj["Red Yellow 2"];
        public static Sprite GemRedOutlineYellow4 => specializationDict.obj["Red Yellow 3"];
        public static Sprite GemRedOutlineYellow5 => specializationDict.obj["Red Yellow 4"];

        public static Sprite GemYellowOutlineBlue1 => specializationDict.obj["Yellow Blue 0"];
        public static Sprite GemYellowOutlineBlue2 => specializationDict.obj["Yellow Blue 1"];
        public static Sprite GemYellowOutlineBlue3 => specializationDict.obj["Yellow Blue 2"];
        public static Sprite GemYellowOutlineBlue4 => specializationDict.obj["Yellow Blue 3"];
        public static Sprite GemYellowOutlineBlue5 => specializationDict.obj["Yellow Blue 4"];

        public static Sprite GemYellowOutlineRed1 => specializationDict.obj["Yellow Red 0"];
        public static Sprite GemYellowOutlineRed2 => specializationDict.obj["Yellow Red 1"];
        public static Sprite GemYellowOutlineRed3 => specializationDict.obj["Yellow Red 2"];
        public static Sprite GemYellowOutlineRed4 => specializationDict.obj["Yellow Red 3"];
        public static Sprite GemYellowOutlineRed5 => specializationDict.obj["Yellow Red 4"];
    }
}
