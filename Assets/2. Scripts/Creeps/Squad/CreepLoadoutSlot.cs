namespace Core {
    public class CreepLoadoutSlot {
        public CreepAttachmentInstance currentAttactment { get; } = new CreepAttachmentInstance();
        public CreepAttachmentDefinition[] allowedAttachments { get; private set; }

        public CreepLoadoutSlot(params CreepAttachmentDefinition[] allowed) {
            allowedAttachments = allowed;
        }
        public void GetApplication(CreepStatModification stage1, CreepStatModification stage2) {
            if (currentAttactment.definition == null) {
                return;
            }

            currentAttactment.definition.ApplyModification(currentAttactment.level, stage1, stage2);
        }
    }
}
