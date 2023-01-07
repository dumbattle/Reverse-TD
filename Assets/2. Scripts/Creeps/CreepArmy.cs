using System.Collections.Generic;


namespace Core {
    public class CreepArmy {
        List<CreepDefinition> squads = new List<CreepDefinition>();
        public int count => squads.Count;


        public void Init() {
            squads.Add(CreepSelectionUtility.GetInitialCreep());
        }

        public void AddNewSquad(CreepDefinition cdef) {
            squads.Add(cdef);
        }

        public CreepDefinition GetSquad(int index) {
            return squads[index];
        }
    }
}
