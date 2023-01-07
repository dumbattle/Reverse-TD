using LPE;
using UnityEngine;

namespace Core {
    public class CreepResourceCache {
        static LazyLoadResource<SpriteReferenceDictionary> creepDict = new LazyLoadResource<SpriteReferenceDictionary>("Sprites/Creep Sprites");

        public static Sprite circleSpriteGreen => creepDict.obj["Circle Green"];
        public static Sprite circleSpriteBlue => creepDict.obj["Circle Blue"];
        public static Sprite circleSpriteRed => creepDict.obj["Circle Red"];
        public static Sprite circleSpriteYellow => creepDict.obj["Circle Yellow"];
    }
}
