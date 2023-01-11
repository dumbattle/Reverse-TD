using System.Collections.Generic;
using UnityEngine;
using Core;
using UnityEditor;

public class Main : MonoBehaviour {
    public ScenarioParameters parameters;

    ScenarioInstance s;
    ScenarioController controller;
    void Start() {
        Application.targetFrameRate = 60;
        s = ScenarioInstance.Get(parameters);
        controller = ScenarioController.Get(s);
    }


    void Update() {
        controller.Update();
    }
    private void OnDrawGizmos() {
        if (s == null) {
            return;
        }
        s?.DrawGizmos();

        for (int x = 0; x < s.mapQuery.width; x++) {
            for (int y = 0; y < s.mapQuery.height; y++) {
                var t = s.mapQuery.GetTile(x, y);
                var dist = t.distFromTarget;

                var pos = s.mapQuery.TileToWorld(x, y);
                Handles.Label(pos, dist.ToString());
            }
        }
    }
}
