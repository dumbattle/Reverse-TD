using System.Collections.Generic;


namespace Core {
    public class CreepArmy {
        List<CreepSquad> squads = new List<CreepSquad>();
        public int count => squads.Count;

        public void Init() {
            AddNewSquad(CreepSelectionUtility.GetInitialCreep());
        }

        public void AddNewSquad(CreepDefinition cdef) {
            squads.Add(new CreepSquad(cdef));
        }

        public CreepSquad GetSquad(int index) {
            return squads[index];
        }
    }

}
