namespace Core {
    public class CreepLoadoutSlot {
        public CreepAttachmentInstance currentAttactment { get; } = new CreepAttachmentInstance();
        public CreepAttachmentDefinition[] allowedAttachments { get; private set; }

        public CreepLoadoutSlot(params CreepAttachmentDefinition[] allowed) {
            allowedAttachments = allowed;
        }
        public void Apply(CreepStatSet stats) {
            if (currentAttactment.definition == null) {
                return;
            }

            currentAttactment.definition.ApplyModification(currentAttactment.level, stats);
        }
    }
}
