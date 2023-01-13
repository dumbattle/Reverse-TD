using System;
using UnityEngine;


namespace Core {
    [Serializable]
    public class ScenarioParameters {
        public int width;
        public int height;
        public FloorTileBehaviour tileSrc;
        public CreepBehaviour creepSrc;

        public GameObject cameraPivot;
        public Camera mainCamera;

        public GameUI ui;
    }
}
