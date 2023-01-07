namespace Core {
    public class PlayerFunctions {
        ScenarioParameters parameters;
        PlayerData player;

        public PlayerFunctions(ScenarioParameters parameters, PlayerData player) {
            this.parameters = parameters;
            this.player = player;
        }

        public void AddMoney(int amnt) {
            player.money += amnt;
            parameters.ui.moneyText.text = player.money.ToString();
        }

        public CreepArmy GetCreepArmy() {
            return player.creepArmy;
        }
    }
}
