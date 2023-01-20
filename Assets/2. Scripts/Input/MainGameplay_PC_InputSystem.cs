using UnityEngine;


namespace Core {
    public class MainGameplay_PC_InputSystem : IInputSystem {
        Vector2 mouseDownScreenPos;
        Vector2 lastDragPosition;
        bool isDragging;

        bool buttonPressed;
        
        
        public void GetInput(ScenarioInstance s) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                InputManager.Set.Cancel();
            }
            DirectionalKeyboard();
            Mouse(s);
        }

        void DirectionalKeyboard() {
            Vector2 dir = new Vector2();

            if (Input.GetKey(KeyCode.W)) {
                dir.y = 1;
            }
            if (Input.GetKey(KeyCode.A)) {
                dir.x = -1;
            }
            if (Input.GetKey(KeyCode.S)) {
                dir.y = -1;
            }
            if (Input.GetKey(KeyCode.D)) {
                dir.x = 1;
            }

            if (dir != Vector2.zero) {
                InputManager.Set.MoveCamera(dir.normalized * FrameUtility.DeltaTime(false));
            }
        }

        void Mouse(ScenarioInstance s) {
            // mouse down
            if (Input.GetMouseButtonDown(0)) {
                mouseDownScreenPos = Input.mousePosition;
                lastDragPosition = mouseDownScreenPos;
                if (InputManager.ButtonDown.yes) {
                    buttonPressed = true;
                }
            }

            // mouse drag
            if (Input.GetMouseButton(0) && !buttonPressed) {
                Vector2 downPosWorld = s.references.mainCamera.ScreenToWorldPoint(lastDragPosition);
                Vector2 currentPosWorld = s.references.mainCamera.ScreenToWorldPoint(Input.mousePosition);

                Vector2 drag = currentPosWorld - downPosWorld;

                // -- not yet dragging - check if dist is large enough to begin drag
                if (!isDragging) {
                    float d2 = drag.sqrMagnitude;

                    if (d2 > 0.5f * 0.5f) {
                        isDragging = true;
                    }
                }

                // -- is dragging - set camera move input, update last drag position
                if (isDragging) {
                    InputManager.Set.MoveCamera(-drag);
                    lastDragPosition = Input.mousePosition;
                }
            }

            // mouse up
            if (Input.GetMouseButtonUp(0)) {
                buttonPressed = false;
                // -- is dragging, do nothing
                if (isDragging) {
                    isDragging = false;
                }
                //// -- not draging, get tile select
                //else {
                //    var t = b.mapQueury.WorldToTileIndex(b.parameters.mainCam.ScreenToWorldPoint(lastDragPosition));
                //    InputManager.Set.SelectTile(t);
                //}
            }

            InputManager.Set.ZoomCamera(-Input.mouseScrollDelta.y);
        }
    }
}
