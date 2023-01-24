namespace Core {
    public class PlayRound_ScenarioState : IFSM_State<ScenarioInstance> {
        //******************************************************************************
        // Singleton
        //******************************************************************************

        PlayRound_ScenarioState() { }
        static PlayRound_ScenarioState instance = new PlayRound_ScenarioState();

        public static PlayRound_ScenarioState Get(ScenarioInstance s) {
            instance.bufferFrames = 40;
            instance.timer = 0;
            instance.currentSquad = 0;
            instance.currentSquadCount = 0;
            s.references.ui.BeginRound();
            return instance;
        }

        //******************************************************************************
        // State
        //******************************************************************************

        int bufferFrames = 0;

        float timer = 0;
        int currentSquad;
        int currentSquadCount;

        //******************************************************************************
        // IFSM_State
        //******************************************************************************

        public IFSM_State<ScenarioInstance> Update(ScenarioInstance s) {
            using (FrameUtility.GetGameLoopContex()) {
                for (int i = 0; i < FrameUtility.gpSpeed.SimulationLoopIterationCount(); i++) {
                    // update entities
                    s.creepFunctions.UpdateAllCreeps(s);
                    s.towerFunctions.UpdateAllTowers();

                    // spawn creeps
                    bool doneSpawning = SpawnCreeps(s);

                    if (doneSpawning) {
                        // check for no more creeps
                        if (s.creepFunctions.CreepCount() <= 0) {
                            bufferFrames--;
                            if (bufferFrames <= 0) {
                                s.towerFunctions.EndRound();
                                return EndRound_ScenarioState.Get();
                            }
                        }
                    }

                    // check for end
                    foreach (var end in s.parameters.endDefinitions) {
                        if (!end.Check(s)) {
                            continue;
                        }

                        return end.GetEndSequence(s);
                    }
                    // check for tower defeat
                    //if (s.parameters.towerController.IsDefeated()) {
                    //    FrameUtility.gpSpeed = GameplaySpeed.x0_5;
                    //}
                }
            }
           

            s.HandleMoveZoomInput();
           
            return null;
        }

        //******************************************************************************
        // Helpers
        //******************************************************************************

        /// <summary>
        /// returns true if all creeps have been spawned
        /// </summary>
        bool SpawnCreeps(ScenarioInstance s) {
            var army = s.playerFunctions.GetCreepArmy();

            // check done
            if (currentSquad >= army.count) {
                return true;
            }

            // check timer
            if (timer > 0) {
                timer -= FrameUtility.DeltaTime(true);
                return false;
            }

            // get squad
            var squad = army.GetSquad(currentSquad);
            
            // next creep
            s.creepFunctions.AddCreep(CreepInstance.Get(s, squad.actualDefinition));

            // update
            
            currentSquadCount++;
            if (currentSquadCount >= (int)(squad.actualDefinition.count)) {
                currentSquad++;
                currentSquadCount = 0;

                // check done
                if (currentSquad >= army.count) {
                    return true;
                }

                squad = army.GetSquad(currentSquad);
            }
            
            timer += squad.actualDefinition.spawnInterval;
            return false;
        }
    }
}
