namespace Core {
    public class Health {
        public float current { get; private set; }
        public float max { get; private set; }

        public Health(float amnt) {
            max = current = amnt;
        }

        public void SetCurrent(float value) {
            current = value;
        }

        public void DealDamage(float amnt) {
            current -= amnt;

            if (current  < 0) {
                current = 0;
            }
        }

        public void AddHealth(float amnt) {
            current += amnt;
            if (current > max) {
                current = max;
            }
        }
        public float Ratio() {
            return (float)current / max;
        }
        public void Reset(float max = -1) {
            if (max <= 0) {
                max = this.max;
            }

            this.max = max;
            current = max;
        }
    }
}
