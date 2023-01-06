using UnityEngine;
using System.Collections.Generic;


namespace Core {
    public class CreepDefinition {
        public float speed;
        public float radius;
        public Sprite sprite;
        public int hp;

        // group data
        public int count;
        public float spacing;
    }

    public class CreepArmy {
        List<CreepDefinition> squads = new List<CreepDefinition>();
        public int count => squads.Count;
        public void Init() {
            var cdef = new CreepDefinition();
            cdef.radius = 0.3f;
            cdef.speed = 1;
            cdef.hp = 10;
            cdef.sprite = CreepResourceCache.circleSpriteGreen;

            cdef.count = 10;
            cdef.spacing = 1;
            squads.Add(cdef);
        }

        public CreepDefinition GetSquad(int index) {
            return squads[index];
        }

    }
}
