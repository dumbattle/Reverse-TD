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

            defaultSquad = new CreepSquad(CreepSelectionUtility.GetInitialCreep());
        }

        public CreepSquad AddNewSquad(CreepDefinition cdef) {
            var result = new CreepSquad(cdef);
            squads.Add(result);

            return result;
        }

        public CreepSquad GetSquad(int index) {
            return squads[index];
        }

    }

}
