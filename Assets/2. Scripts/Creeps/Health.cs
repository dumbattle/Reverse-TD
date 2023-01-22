namespace Core {
    public class Health {
        public int current { get; private set; }
        public int max { get; private set; }

        public Health(int amnt) {
            max = current = amnt;
        }

        public void DealDamage(int amnt) {
            current -= amnt;
        }

        public void AddHealth(int amnt) {
            current += amnt;
            if (current > max) {
                current = max;
            }
        }
        public float Ratio() {
            return (float)current / max;
        }
        public void Reset(int max = -1) {
            if (max <= 0) {
                max = this.max;
            }

            this.max = max;
            current = max;
        }
    }
}
