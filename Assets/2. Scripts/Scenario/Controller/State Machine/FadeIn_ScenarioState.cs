using UnityEngine;
using UnityEngine.SceneManagement;


namespace Core {
    public class FadeIn_ScenarioState : IFSM_State<ScenarioInstance> {
        //******************************************************************************
        // Singleton
        //******************************************************************************

        FadeIn_ScenarioState() { }
        static FadeIn_ScenarioState instance = new FadeIn_ScenarioState();

        public static FadeIn_ScenarioState Get(int duration) {
            instance.timer = 0;
            instance.duration = duration;
            return instance;
        }
        //******************************************************************************
        // State
        //******************************************************************************
        float timer;
        int duration;


        //******************************************************************************
        // IFSM_State
        //******************************************************************************

        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            timer += FrameUtility.GetFrameMultiplier(false);

            s.references.ui.fadeOverlay.color = new Color(0, 0, 0, 1 - (float)timer / duration);
            if (timer > duration) {
                return PreRoundIdle_ScenarioState.Get(s);
            }
            return null;
        }
    }
    public class FadeOut_ToMainMenu_ScenarioState : IFSM_State<ScenarioInstance> {
        //******************************************************************************
        // Singleton
        //******************************************************************************

        FadeOut_ToMainMenu_ScenarioState() { }
        static FadeOut_ToMainMenu_ScenarioState instance = new FadeOut_ToMainMenu_ScenarioState();

        public static FadeOut_ToMainMenu_ScenarioState Get(int duration) {
            instance.timer = 0;
            instance.duration = duration;
            return instance;
        }
        //******************************************************************************
        // State
        //******************************************************************************
        float timer;
        int duration;


        //******************************************************************************
        // IFSM_State
        //******************************************************************************

        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            // continue simulation
            using (FrameUtility.GetGameLoopContex()) {
                for (int i = 0; i < FrameUtility.gpSpeed.SimulationLoopIterationCount(); i++) {
                    s.creepFunctions.UpdateAllCreeps(s);
                    s.towerFunctions.UpdateAllTowers();
                }
            }


            // fade out
            timer += FrameUtility.GetFrameMultiplier(false);
            s.references.ui.fadeOverlay.color = new Color(0, 0, 0, timer / duration);

            // done
            if (timer > duration) {
                SceneManager.LoadScene(0);
            }

            return null;
        }
    }
}
