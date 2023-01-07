using System.Collections.Generic;
using UnityEngine;
using Core;


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
        s?.DrawGizmos();
    }
}
