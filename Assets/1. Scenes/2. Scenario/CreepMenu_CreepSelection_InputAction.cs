namespace Core {
    public struct CreepMenu_CreepSelection_InputAction {
        enum Mode {
            none, 
            squadSelect,
            emptySelect,
            up,
            down
        }
        Mode mode;
        public CreepSquad squad { get; private set; }


        public bool hasInput => mode != Mode.none;
        public bool emptySelected => mode == Mode.emptySelect;
        public bool upSelected => mode == Mode.up;
        public bool downSelected => mode == Mode.down;

        public static CreepMenu_CreepSelection_InputAction None() {
            var result = new CreepMenu_CreepSelection_InputAction();
            result.mode = Mode.none;
            return result;
        }

        public static CreepMenu_CreepSelection_InputAction SquadSelect(CreepSquad s) {
            var result = new CreepMenu_CreepSelection_InputAction();
            result.mode = Mode.squadSelect;
            result.squad = s;
            return result;
        }

        public static CreepMenu_CreepSelection_InputAction EmptySelect() {
            var result = new CreepMenu_CreepSelection_InputAction();
            result.mode = Mode.emptySelect;
            return result;
        }

        public static CreepMenu_CreepSelection_InputAction Up() {
            var result = new CreepMenu_CreepSelection_InputAction();
            result.mode = Mode.up;
            return result;
        }

        public static CreepMenu_CreepSelection_InputAction Down() {
            var result = new CreepMenu_CreepSelection_InputAction();
            result.mode = Mode.down;
            return result;
        }
    }

}
