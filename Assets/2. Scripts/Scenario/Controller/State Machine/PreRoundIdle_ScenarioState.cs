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
            //for (int i = paths.Count - 1; i >= 0; i--) {
            //    var h = paths[i];
            //    h.t--;

            //    if (h.t <= 0) {
            //        if (s.mapQuery.GetTile(h.current.x, h.current.y).distFromTarget == 0) {
            //            h.Return();
            //            paths.RemoveAt(i);
            //        }
            //        else {
            //            h.current = CreepInstance.GetDestinationTile(s, h.current);
            //            s.mapQuery.PingTile(h.current);
            //            h.t = 5;
            //        }
            //    }
            //}

            //pathSpawnTiwmer--;
            //if (pathSpawnTiwmer <= 0) {
            //    CreepPathHighlighter h = CreepPathHighlighter.Get();
            //    h.current = s.mapQuery.GetRandomCreepSpawn();
            //    s.mapQuery.PingTile(h.current);
            //    h.t = 6;
            //    pathSpawnTiwmer = 10;
            //    paths.Add(h);
            //}



            s.HandleMoveZoomInput();
            if (InputManager.Start.requested) {
                return PlayRound_ScenarioState.Get(s);
            }

            if (InputManager.PreRoundUI.creepMenuOpen) {
                return PreRoundIdle_CreepMenu_ScenarioState.Get(s);
            }
            if (InputManager.PreRoundUI.shopMenuOpen) {
                return PreRoundIdle_ShopMenu_ScenarioState.Get(s);
            }
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
