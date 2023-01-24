using System.Collections.Generic;
using UnityEngine;
using Core;
using UnityEditor;

public class ScenarioMain : MonoBehaviour {
    public ScenarioParameters parameters;
    public ScenarioUnityReferences unityReferences;
    ScenarioInstance s;
    ScenarioController controller;

    void Start() {
        var p = InterSceneCommunicator.scenarioParameters;
        if (p == null) {
            p = parameters;
            p.mainTowers.Add(new MainTowerParameters(TowerDefinitionCatalog.main_basic_diamond_blue, 100_000, new Vector2Int(p.width / 2, p.height / 2)));
        }
        s = ScenarioInstance.Get(p, unityReferences);
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
