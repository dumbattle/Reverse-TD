using LPE;
using UnityEngine;

namespace Core {
    public class CreepResourceCache {
        static LazyLoadResource<Sprite> _circleSpriteGreen = new LazyLoadResource<Sprite>("Creeps/Circle Green");
        static LazyLoadResource<Sprite> _circleSpriteBlue = new LazyLoadResource<Sprite>("Creeps/Circle Blue");
        static LazyLoadResource<Sprite> _circleSpriteRed= new LazyLoadResource<Sprite>("Creeps/Circle Red");
        static LazyLoadResource<Sprite> _circleSpriteYellow = new LazyLoadResource<Sprite>("Creeps/Circle Yellow");


        public static Sprite circleSpriteGreen => _circleSpriteGreen;
        public static Sprite circleSpriteBlue => _circleSpriteBlue;
        public static Sprite circleSpriteRed => _circleSpriteRed;
        public static Sprite circleSpriteYellow => _circleSpriteYellow;
    }
}
