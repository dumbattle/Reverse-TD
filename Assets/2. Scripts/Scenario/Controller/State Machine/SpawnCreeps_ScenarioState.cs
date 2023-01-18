﻿namespace Core {
    public class SpawnCreeps_ScenarioState : IFSM_State<ScenarioInstance> {
        //******************************************************************************
        // Singleton
        //******************************************************************************

        SpawnCreeps_ScenarioState() { }
        static SpawnCreeps_ScenarioState instance = new SpawnCreeps_ScenarioState();

        public static SpawnCreeps_ScenarioState Get(ScenarioInstance s) {
            instance.timer = 0;
            instance.currentSquad = 0;
            instance.currentSquadCount = 0;
            s.references.ui.BeginRound();
            return instance;
        }

        //******************************************************************************
        // state
        //******************************************************************************
        float timer = 0;
        int currentSquad;
        int currentSquadCount;

        //******************************************************************************
        // IFSM_State
        //******************************************************************************

        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            s.GamplayUpdate();

            if (timer > 0) {
                timer -= 1f / 60f;
                return null;
            }

            var army = s.playerFunctions.GetCreepArmy();
            var squad = army.GetSquad(currentSquad);
            s.creepFunctions.AddCreep(CreepInstance.Get(s, squad.actualDefinition, s.mapQuery.GetRandomCreepSpawn()));
            currentSquadCount++;

            if (currentSquadCount >= (int)(squad.actualDefinition.count)) {
                currentSquad++;
                currentSquadCount = 0;
                if (currentSquad >= army.count) {
                    return PlayRound_ScenarioState.Get();
                }
                squad = army.GetSquad(currentSquad);
            }
            timer = squad.actualDefinition.spacing;
            return null;
        }
    }
}
