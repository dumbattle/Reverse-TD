using System;
using UnityEngine;


namespace Core {
    public struct MainTowerParameters {
        public Vector2Int position;
        public TowerDefinition definition;
        public int maxHealth;

        public MainTowerParameters(TowerDefinition definition, int maxHealth, Vector2Int position) {
            this.definition = definition ?? throw new ArgumentNullException(nameof(definition));
            this.position = position;
            this.maxHealth = maxHealth;
        }
    }
}
