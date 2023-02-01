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

            ScenarioUI_CreepMenu creepMenu = s.references.ui.creepMenu;
            if (creepMenu.buyCreepButton.Clicked) {
                var newCreep = CreepSelectionUtility.GetRandomNewCreep();
                var squad = s.playerFunctions.GetCreepArmy().AddNewSquad(newCreep);
                squad.AddModifier(new CreepAttachment_Carrier());
                squad.AddModifier(new CreepAttachment_DeathSplit());
                creepMenu.ReDraw(s);
            }
            var creepSelected = creepMenu.creepSelected;
            if (creepSelected != null) {
                creepMenu.OpenDetailsTab(true);
                creepMenu.SetCreepDetails(s, creepSelected);
            }

            if (creepMenu.details.buttons.squadParentButton.Clicked) {
                creepMenu.SelectParentOfCurrentSquad(s);
            }
            if (creepMenu.details.buttons.squadChildCarrierButton.Clicked) {
                creepMenu.SelectCarrierChildOfCurrentSquad(s);
            }
            if (creepMenu.details.buttons.squadChildDeathButton.Clicked) {
                creepMenu.SelectDeathChildOfCurrentSquad(s);
            }


            if (creepMenu.details.buttons.statsSubmenuButton.Clicked) {
                creepMenu.OpenStatsSubmenu();
            }
            if (creepMenu.details.buttons.attachmentsSubmenuButton.Clicked) {
                creepMenu.OpenAttachmentsSubmenu();
            }
            if (InputManager.Cancel.requested) {
                if (creepMenu.DetailsIsOpen()) {
                    creepMenu.OpenDetailsTab(false);
                    InputManager.Consume.Cancel();
                }
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
