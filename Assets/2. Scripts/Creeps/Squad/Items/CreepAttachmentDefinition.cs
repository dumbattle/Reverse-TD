using UnityEngine;



namespace Core {
    public abstract class CreepAttachmentDefinition {
        ResourceAmount[] _upgradeCosts;

        public CreepAttachmentDefinition() {
            _upgradeCosts = InitUpgradeCosts();
        }


        public int maxLevel => _upgradeCosts.Length;
        /// <summary>
        /// To unlock provided level. level is 1-indexed
        /// </summary>
        public ResourceAmount GetCost(int level) {
            level = Mathf.Clamp(level - 1, 0, _upgradeCosts.Length - 1);

            return _upgradeCosts[level];
        }

        /// <summary>
        /// Level = 0 for menu description
        /// </summary>
        public abstract string GetDescription(int level);

        /// <summary>
        /// Level = 0 for menu name 
        /// </summary>
        public abstract string GetName(int level);

        /// <summary>
        /// Level is 1-indexed
        /// </summary>
        public abstract Sprite GetIcon(int level);

        public abstract void ApplyModification(int level, CreepStatSet stats);


        protected abstract ResourceAmount[] InitUpgradeCosts();
    }

}