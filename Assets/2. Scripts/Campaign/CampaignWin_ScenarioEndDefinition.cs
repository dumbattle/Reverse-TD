using UnityEngine;


namespace Core {
    public class CampaignWin_ScenarioEndDefinition : IScenarioEndDefinition, IFSM_State<ScenarioInstance> {
        CampaignTowerController tc;

        public CampaignWin_ScenarioEndDefinition(CampaignTowerController tc) {
            this.tc = tc;
        }

        //**************************************************************************************************
        // IScenarioEndDefinition
        //**************************************************************************************************

        public bool Check(ScenarioInstance s) {
            return tc.IsDefeated();
        }

        public IFSM_State<ScenarioInstance> GetEndSequence(ScenarioInstance s) {
            FrameUtility.gpSpeed = GameplaySpeed.x0_5;
            return this;
        }


        //**************************************************************************************************
        // State
        //**************************************************************************************************

        float time = 0;
        //**************************************************************************************************
        // IFSM_State
        //**************************************************************************************************
        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            // update entities
            using (FrameUtility.GetGameLoopContex()) {
                for (int i = 0; i < FrameUtility.gpSpeed.SimulationLoopIterationCount(); i++) {
                    s.creepFunctions.UpdateAllCreeps(s);
                    s.towerFunctions.UpdateAllTowers();
                }
            }

            // pan camera to lastdestroy main tower
            var lastMain = tc.lastDestroyedTower;
            var targetCamPosition = s.mapQuery.TileToWorld(lastMain.GetShape().position);
            var delta = FrameUtility.DeltaTime(false) * 3;
            s.references.cameraPivot.transform.position = (targetCamPosition * delta + (Vector2)s.references.cameraPivot.transform.position) /(1 + delta);


            time += FrameUtility.DeltaTime(false);
            if (time > 6) {
                FrameUtility.gpSpeed = GameplaySpeed.x1;
                return FadeOut_ToMainMenu_ScenarioState.Get(35);
            }
            return null;
        }
    }
}
