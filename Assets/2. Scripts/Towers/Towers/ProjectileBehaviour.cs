using UnityEngine;



namespace Core {
    public abstract class ProjectileBehaviour : MonoBehaviour {
        public abstract void Init(ScenarioInstance s, Vector2 start, Vector2 direction, float speed, float maxDist, float radius, TowerDamageInstance damage);
        public abstract void GameUpdate(ScenarioInstance s);
        public abstract bool Active();
    }
}
