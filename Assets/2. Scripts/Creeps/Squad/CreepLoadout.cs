namespace Core {
    public class CreepLoadout {
        public CreepLoadoutSlot loot { get; private set; } = GetResourceSlot();
        public CreepLoadoutSlot build { get; private set; } = GetSpecializationSlot();
        public CreepLoadoutSlot armor { get; private set; } = new CreepLoadoutSlot();

        public CreepLoadoutSlot attr1 { get; private set; } = new CreepLoadoutSlot();
        public CreepLoadoutSlot attr2 { get; private set; } = new CreepLoadoutSlot();
        public CreepLoadoutSlot attr3 { get; private set; } = new CreepLoadoutSlot();

        public CreepLoadoutSlot spec1 { get; private set; } = new CreepLoadoutSlot();
        public CreepLoadoutSlot spec2 { get; private set; } = new CreepLoadoutSlot();

        public void Apply(CreepStatSet stats) {
            loot.Apply(stats);
            build.Apply(stats);
            armor.Apply(stats);

            attr1.Apply(stats);
            attr2.Apply(stats);
            attr3.Apply(stats);

            spec1.Apply(stats);
            spec2.Apply(stats);

        }

        static CreepLoadoutSlot GetSpecializationSlot() {
            var result = new CreepLoadoutSlot(
                CreepAttachment_Specialization_Spd2HP.Get(),
                CreepAttachment_Specialization_Count2HP.Get(),
                CreepAttachment_Specialization_HP2Count.Get(),

                CreepAttachment_Specialization_Spd2Count.Get(),
                CreepAttachment_Specialization_HP2SPD.Get(),
                CreepAttachment_Specialization_Count2SPD.Get()
            );
            return result;
        }
        static CreepLoadoutSlot GetResourceSlot() {
            var result = new CreepLoadoutSlot(
                CreepAttachment_Resource_Green.Get(),
                CreepAttachment_Resource_Blue.Get(),
                CreepAttachment_Resource_Red.Get(),
                CreepAttachment_Resource_Yellow.Get(),

                CreepAttachment_Resource_Diamond.Get()
                //CreepAttachment_Resource_RBY.Get()
            );
            result.currentAttactment.ResetAttachment(CreepAttachment_Resource_Green.Get());
            return result;
        }
        //static CreepLoadoutSlot GetTier1Slot() {
        //    return new CreepLoadoutSlot(
        //        CreepAttachment_Tier1_HP.Get(),
        //        CreepAttachment_Tier1_SPD.Get(),
        //        CreepAttachment_Tier1_SpawnRate.Get(),
        //        CreepAttachment_Tier1_Count.Get(),
        //        CreepAttachment_Tier1_Damage.Get()
        //    );
        //}
        //static CreepLoadoutSlot GetTier2Slot() {
        //    return new CreepLoadoutSlot(
        //        CreepAttachment_Tier2_Regen.Get()
        //    );
        //}
    }
}
