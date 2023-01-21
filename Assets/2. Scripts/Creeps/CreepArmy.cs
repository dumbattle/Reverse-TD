using System.Collections.Generic;


namespace Core {
    public class CreepArmy {
        List<CreepSquad> squads = new List<CreepSquad>();
        public int count => squads.Count;
        GlobalCreeepUpgrades globalUpgrades;
        CreepStatModification levelModifiers;
        public CreepSquad defaultSquad { get; private set; } // used in UI

        public void Init(GlobalCreeepUpgrades globalUpgrades, CreepStatModification levelModifiers) {
            this.globalUpgrades = globalUpgrades;
            this.levelModifiers = levelModifiers;
            AddNewSquad(CreepSelectionUtility.GetInitialCreep());

            defaultSquad = new CreepSquad(CreepSelectionUtility.GetInitialCreep(), globalUpgrades, levelModifiers);
        }

        public void AddNewSquad(CreepDefinition cdef) {
            squads.Add(new CreepSquad(cdef, globalUpgrades, levelModifiers));
        }

        public CreepSquad GetSquad(int index) {
            return squads[index];
        }

    }

}
