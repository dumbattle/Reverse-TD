using System.Collections.Generic;
using UnityEngine;
using System.Linq;



namespace Core {
    public class CreepAttachmentInstance {
        public CreepAttachmentDefinition definition;
        /// <summary>
        /// Between [1, 10] inclusive
        /// </summary>
        public int level { get; private set; } = 1;

        //***************************************************************************************
        // Control
        //***************************************************************************************

        public void ResetAttachment(CreepAttachmentDefinition def) {
            definition = def;
            level = 1;
        }

        public void UpgradeLevel() {

            level++;
            if (level > 10) {
                level = 10;
            }
        }

        //***************************************************************************************
        // Query
        //***************************************************************************************

        public Sprite GetIcon() {
            return definition?.GetIcon(level) ?? null;
        }

        public string GetName() {
            return definition?.GetName(level) ?? null;
        }

        public string GetDescription() {
            return definition?.GetDescription(level) ?? null;
        }

        public ResourceRequirement GetCostForUpgrade() {
            if (level == 10) {
                return null;
            }

            return definition?.GetCost(level + 1) ?? null;
        }
    }

}