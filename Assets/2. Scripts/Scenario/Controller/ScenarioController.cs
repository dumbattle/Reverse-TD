using LPE;


namespace Core {
    public class ScenarioController {
        //******************************************************************************
        // Pooling
        //******************************************************************************

        ScenarioController() { }

        public static ScenarioController Get(ScenarioInstance s) {
            var result = new ScenarioController();
            result.scenario = s;
            result.currentState = Init_ScenarioState.Get();
            return result;
        }

        //******************************************************************************
        // Fields
        //******************************************************************************

        ScenarioInstance scenario;

        //------------------------------------------------------------------------------
        // Dynamic
        //------------------------------------------------------------------------------

        IFSM_State<ScenarioInstance> currentState;
        IInputSystem inputSystem = new MainGameplay_PC_InputSystem();

        //******************************************************************************
        // Control
        //******************************************************************************

        public void Update() {
            inputSystem.GetInput(scenario);
            currentState = currentState.Update(scenario) ?? currentState;
            InputManager.Clear();
        }
    }
}
