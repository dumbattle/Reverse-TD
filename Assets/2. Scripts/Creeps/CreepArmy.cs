using System.Collections.Generic;


namespace Core {
    public class CreepArmy {
        List<CreepSquad> squads = new List<CreepSquad>();
        public int count => squads.Count;
        GlobalCreeepUpgrades globalUpgrades;
        public void Init(GlobalCreeepUpgrades globalUpgrades) {
            this.globalUpgrades = globalUpgrades;
            AddNewSquad(CreepSelectionUtility.GetInitialCreep());
        }

        public void AddNewSquad(CreepDefinition cdef) {
            squads.Add(new CreepSquad(cdef, globalUpgrades));
        }

        public CreepSquad GetSquad(int index) {
            return squads[index];
        }
    }

}
