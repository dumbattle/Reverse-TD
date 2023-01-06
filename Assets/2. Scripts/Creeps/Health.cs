﻿namespace Core {
    public class Health {
        public int current { get; private set; }
        public int max { get; private set; }

        public Health(int amnt) {
            max = current = amnt;
        }

        public void DealDamage(int amnt) {
            current -= amnt;
        }
    }
}
