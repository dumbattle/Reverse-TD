//namespace Core {
//    public class Idle_ScenarioState : IFSM_State {
//        //******************************************************************************
//        // Singleton
//        //******************************************************************************
       
//        Idle_ScenarioState() { }
//        static Idle_ScenarioState instance = new Idle_ScenarioState();

//        public static Idle_ScenarioState Get() {
//            return instance;
//        }

//        //******************************************************************************
//        // IFSM_State
//        //******************************************************************************
        
//        public IFSM_State Update(ScenarioInstance s) {
//            s.creepFunctions.UpdateAllCreeps(s);
//            s.towerFunctions.UpdateAllTowers();
//            return null;
//        }
//    }

//}
