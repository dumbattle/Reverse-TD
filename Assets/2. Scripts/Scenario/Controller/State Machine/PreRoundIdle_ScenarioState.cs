using UnityEngine;
using LPE;
using System.Collections.Generic;


namespace Core {
    public class PreRoundIdle_ScenarioState : IFSM_State<ScenarioInstance> {
        //******************************************************************************
        // Singleton
        //******************************************************************************
        PreRoundIdle_ScenarioState() { }
        static PreRoundIdle_ScenarioState instance = new PreRoundIdle_ScenarioState();

        public static PreRoundIdle_ScenarioState Get(ScenarioInstance s) {
            s.references.ui.BeginPreRound();
            instance.pathSpawnTiwmer = 0;

            foreach (var p in instance.paths) {
                p.Return();
            }

            instance.paths.Clear();
            return instance;
        }

        int pathSpawnTiwmer;
        List<CreepPathHighlighter> paths = new List<CreepPathHighlighter>();

        
        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            s.HandleMoveZoomInput();
            if (InputManager.Start.requested) {
                return PlayRound_ScenarioState.Get(s);
            }

            if (s.references.ui.creepMenu.buyCreepButton.Clicked) {
                var newCreep = CreepSelectionUtility.GetRandomNewCreep();
                s.playerFunctions.GetCreepArmy().AddNewSquad(newCreep);

                s.references.ui.creepMenu.ReDraw(s);
            }
            //if (InputManager.PreRoundUI.creepMenuOpen) {
            //    return PreRoundIdle_CreepMenu_ScenarioState.Get(s);
            //}

            //if (InputManager.PreRoundUI.shopMenuOpen) {
            //    return PreRoundIdle_ShopMenu_ScenarioState.Get(s);
            //}

            return null;
        }

        class CreepPathHighlighter {
            static ObjectPool<CreepPathHighlighter> _pool = new ObjectPool<CreepPathHighlighter>(() => new CreepPathHighlighter());
            CreepPathHighlighter() { }
            public static CreepPathHighlighter Get() {
                return _pool.Get();
            }
            public void Return() {
                _pool.Return(this);
            }
            public Vector2Int current;
            public int t;
        }
    }
}
