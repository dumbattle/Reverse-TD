using UnityEngine;


namespace Core {
    public static class ScenarioControllerUtility { 
        public static void GamplayUpdate(this ScenarioInstance s) {
            for (int i = 0; i < 1; i++) {
                s.creepFunctions.UpdateAllCreeps(s);
                s.towerFunctions.UpdateAllTowers();
            }

            HandleMoveZoomInput(s);
        }

        public static void HandleMoveZoomInput(this ScenarioInstance s) {
            if (InputManager.MoveCamera.yes) {
                var c = s.parameters.cameraPivot;
                c.transform.position += (Vector3)InputManager.MoveCamera.dir;
            }

            if (InputManager.ZoomCamera.yes) {
                var cam = s.parameters.mainCamera;
                cam.orthographicSize += InputManager.ZoomCamera.val;
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 3, 10);
            }
        }
    }
}
