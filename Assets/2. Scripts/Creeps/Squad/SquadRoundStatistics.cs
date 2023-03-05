namespace Core {
    public class SquadRoundStatistics {
        public float damageDealt { get; private set; }
        public ResourceAmount resourcesEarned => _resourcesearned;

        public float damageRecieved { get; private set; }

        ResourceCollection _resourcesearned = new ResourceCollection();


        public void Reset() {
            damageDealt = 0;
            _resourcesearned.Reset();
            damageRecieved = 0;
        }

        public void AddDamageTaken(float amnt) {
            damageRecieved += amnt;
        }

        public void AddDamageDealtTower(float amnt) {
            damageDealt += amnt;
        }

        public void AddResourceEarned(ResourceAmount amnt, float multiplier) {
            _resourcesearned[ResourceType.green] += amnt[ResourceType.green] * multiplier;
            _resourcesearned[ResourceType.red] += amnt[ResourceType.red] * multiplier;
            _resourcesearned[ResourceType.blue] += amnt[ResourceType.blue] * multiplier;
            _resourcesearned[ResourceType.yellow] += amnt[ResourceType.yellow] * multiplier;
            _resourcesearned[ResourceType.diamond] += amnt[ResourceType.diamond] * multiplier;
        }
    }
}
