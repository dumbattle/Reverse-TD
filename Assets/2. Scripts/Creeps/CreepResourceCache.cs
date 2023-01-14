using LPE;
using UnityEngine;

namespace Core {
    public class CreepResourceCache {
        static LazyLoadResource<SpriteReferenceDictionary> creepDict = new LazyLoadResource<SpriteReferenceDictionary>("Sprites/Creep Sprites");

        public static Sprite circleSpriteGreen => creepDict.obj["Circle Green"];
        public static Sprite circleSpriteBlue => creepDict.obj["Circle Blue"];
        public static Sprite circleSpriteRed => creepDict.obj["Circle Red"];
        public static Sprite circleSpriteYellow => creepDict.obj["Circle Yellow"];

        public static Sprite squareSpriteGreen => creepDict.obj["Square Green"];
        public static Sprite squareSpriteBlue => creepDict.obj["Square Blue"];
        public static Sprite squareSpriteRed => creepDict.obj["Square Red"];
        public static Sprite squareSpriteYellow => creepDict.obj["Square Yellow"];

        public static Sprite triangleSpriteGreen => creepDict.obj["Triangle Green"];
        public static Sprite triangleSpriteBlue => creepDict.obj["Triangle Blue"];
        public static Sprite triangleSpriteRed => creepDict.obj["Triangle Red"];
        public static Sprite triangleSpriteYellow => creepDict.obj["Triangle Yellow"];
    }
}
