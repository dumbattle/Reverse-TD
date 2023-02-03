using UnityEngine;
using LPE;

namespace Core {
    public static class TileSpriteCache {
        public static class Magenta {
            static LazyLoadResource<SpriteReferenceDictionary> dict = new LazyLoadResource<SpriteReferenceDictionary>("Sprites/Tiles Magenta");
            static LazyLoadResource<GameObject> magentaSpawn = new LazyLoadResource<GameObject>("Prefabs/Tiles/Magenta Spawn");
         
            public static Sprite GetPath(bool up, bool right, bool down, bool left) {
                // all 4 - TODO
                // any 3 - TODO
                // any 2 - No nedd to check that other 2 are false
                if (up) {
                    if (right) {
                        return dict.obj["Path UR"];
                    }
                    if (down) {
                        return dict.obj["Path UD"];
                    }
                    if (left) {
                        return dict.obj["Path UL"];
                    }
                }
                if (right) {
                    if (down) {
                        return dict.obj["Path DR"];
                    }
                    if (left) {
                        return dict.obj["Path LR"];
                    }
                }
                if (down) {
                    if (left) {
                        return dict.obj["Path DL"];
                    }
                }
                return null;
            }
        
            public static GameObject GetSpawn() {
                return GameObject.Instantiate<GameObject>(magentaSpawn);
            }
        }

    }
}
