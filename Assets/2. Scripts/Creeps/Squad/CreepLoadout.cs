namespace Core {
    public class CreepLoadout {
        public CreepLoadoutSlot specialization { get; private set; } = GetSpecializationSlot();
        public CreepLoadoutSlot resource { get; private set; } = GetResourceSlot();

        public CreepLoadoutSlot tier1_1 { get; private set; } = GetTier1Slot();
        public CreepLoadoutSlot tier1_2 { get; private set; } = GetTier1Slot();
        public CreepLoadoutSlot tier1_3 { get; private set; } = GetTier1Slot();

        public CreepLoadoutSlot tier2_1 { get; private set; } = new CreepLoadoutSlot();
        public CreepLoadoutSlot tier2_2 { get; private set; } = new CreepLoadoutSlot();

        public CreepLoadoutSlot tier3_A { get; private set; } = new CreepLoadoutSlot();
        public CreepLoadoutSlot tier3_B { get; private set; } = new CreepLoadoutSlot();

        public void GetApplication(CreepStatModification stage1, CreepStatModification stage2) {
            specialization.GetApplication(stage1, stage2);
            resource.GetApplication(stage1, stage2);

            tier1_1.GetApplication(stage1, stage2);
            tier1_2.GetApplication(stage1, stage2);
            tier1_3.GetApplication(stage1, stage2);

            tier2_1.GetApplication(stage1, stage2);
            tier2_2.GetApplication(stage1, stage2);

            tier3_A.GetApplication(stage1, stage2);
            tier3_B.GetApplication(stage1, stage2);
        }

        static CreepLoadoutSlot GetSpecializationSlot() {
            return new CreepLoadoutSlot(
                CreepAttachment_Specialization_Spd2HP.Get(),
                CreepAttachment_Specialization_Count2HP.Get(),
                CreepAttachment_Specialization_HP2Count.Get(),

                CreepAttachment_Specialization_Spd2Count.Get(),
                CreepAttachment_Specialization_HP2SPD.Get(),
                CreepAttachment_Specialization_Count2SPD.Get()
            );
        }
        static CreepLoadoutSlot GetResourceSlot() {
            var result = new CreepLoadoutSlot(
                CreepAttachment_Resource_Green.Get(),
                CreepAttachment_Resource_Blue.Get(),
                CreepAttachment_Resource_Red.Get(),
                CreepAttachment_Resource_Yellow.Get(),
                CreepAttachment_Resource_Diamond.Get()
            );
            //result.currentAttactment.ResetAttachment(CreepAttachment_Resource_Green.Get());
            return result;
        }
        static CreepLoadoutSlot GetTier1Slot() {
            return new CreepLoadoutSlot(
                CreepAttachment_Tier1_HP.Get(),
                CreepAttachment_Tier1_SPD.Get(),
                CreepAttachment_Tier1_SpawnRate.Get(),
                CreepAttachment_Tier1_Count.Get()
            );
        }
    }
}
