using System;
using UnityEngine;


namespace Core {
    [Serializable]
    public class ScenarioUnityReferences {
        public FloorTileBehaviour tileSrc;
        public CreepBehaviour creepSrc;

        public GameObject cameraPivot;
        public Camera mainCamera;

        public GameUI ui;
    }
}
