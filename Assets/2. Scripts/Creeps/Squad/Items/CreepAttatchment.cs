using System.Collections.Generic;
using UnityEngine;
using System.Linq;



namespace Core {
    public class CreepAttachmentInstance {
        public CreepAttachmentDefinition definition;
        /// <summary>
        /// 1 - indexed
        /// </summary>
        public int level { get; private set; } = 1;
        public int maxLevel => definition?.maxLevel ?? -1;

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


        /// <summary>
        /// Return null if max level or if there is no attachment selected
        /// </summary>
        /// <returns></returns>
        public ResourceAmount GetCostForUpgrade() {
            if (level == 10) {
                return null;
            }

            return definition?.GetCost(level + 1) ?? null;
        }
    }

}