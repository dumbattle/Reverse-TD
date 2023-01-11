using UnityEngine;


namespace Core {
    public class RoundManager {
        public int current = 1;

        public void NextRound() {
            current++;
        }

        public int GetCurrentRoundMoneyReward() {
            return 80 + current * 10 + Random.Range(0, 26);
        }
    }
}
