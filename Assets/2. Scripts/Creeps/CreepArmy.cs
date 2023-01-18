using System.Collections.Generic;


namespace Core {
    public class CreepArmy {
        List<CreepSquad> squads = new List<CreepSquad>();
        public int count => squads.Count;
        GlobalCreeepUpgrades globalUpgrades;
        CreepStatModification levelModifiers;

        public void Init(GlobalCreeepUpgrades globalUpgrades, CreepStatModification levelModifiers) {
            this.globalUpgrades = globalUpgrades;
            this.levelModifiers = levelModifiers;
            AddNewSquad(CreepSelectionUtility.GetInitialCreep());
        }

        public void AddNewSquad(CreepDefinition cdef) {
            squads.Add(new CreepSquad(cdef, globalUpgrades, levelModifiers));
        }

        public CreepSquad GetSquad(int index) {
            return squads[index];
        }
    }

}
