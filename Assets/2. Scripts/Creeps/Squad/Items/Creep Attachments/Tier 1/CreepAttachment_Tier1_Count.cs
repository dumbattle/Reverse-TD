﻿using UnityEngine;


namespace Core {

    public class CreepAttachment_Tier1_Count : CreepAttachmentDefinition {
        static CreepAttachment_Tier1_Count instance = new CreepAttachment_Tier1_Count();
        public static CreepAttachment_Tier1_Count Get() => instance;

        CreepAttachment_Tier1_Count() { }


        static float[] _statScales = {
            30,
            34,
            39,
            45,
            51,
            59,
            67,
            77,
            88,
            100,
        };



        static string[] _descCache = {
            GetDescriptionTextWithUpgradeHelper(0, false),
            GetDescriptionTextWithUpgradeHelper(0, true),
            GetDescriptionTextWithUpgradeHelper(1, true),
            GetDescriptionTextWithUpgradeHelper(2, true),
            GetDescriptionTextWithUpgradeHelper(3, true),
            GetDescriptionTextWithUpgradeHelper(4, true),
            GetDescriptionTextWithUpgradeHelper(5, true),
            GetDescriptionTextWithUpgradeHelper(6, true),
            GetDescriptionTextWithUpgradeHelper(7, true),
            GetDescriptionTextWithUpgradeHelper(8, true),
            GetDescriptionTextWithUpgradeHelper(9, false)
        };

        static string GetDescriptionTextWithUpgradeHelper(int i, bool upgradeHint) {
            var amount = _statScales[i];
            var upAmnt = upgradeHint ? _statScales[i + 1] - amount : 0;
            return
                $"{(upgradeHint ? $"(<color=yellow>+{upAmnt}</color>)" : "")}<color=green>+{amount}%</color>\n" +
                $"<size=10000em> </size>\n" +
                $"<color=yellow>Count</color>\n";
        }

        public override string GetName(int level) {
            return "Number Gem";
        }

        public override Sprite GetIcon(int level) {
            switch (level) {
                case 1:
                    return CreepItemIconResourceCache.GemCyan1;
                case 2:
                    return CreepItemIconResourceCache.GemCyan2;
                case 3:
                    return CreepItemIconResourceCache.GemCyan3;
                case 4:
                    return CreepItemIconResourceCache.GemCyan4;
                case 5:
                    return CreepItemIconResourceCache.GemCyan5;
                case 6:
                    return CreepItemIconResourceCache.GemCyan6;
                case 7:
                    return CreepItemIconResourceCache.GemCyan7;
                case 8:
                    return CreepItemIconResourceCache.GemCyan8;
                case 9:
                    return CreepItemIconResourceCache.GemCyan9;
                case 10:
                    return CreepItemIconResourceCache.GemCyan10;
            }

            return CreepItemIconResourceCache.GemCyan1;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }

        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
            stage2.AddCountScale(_statScales[level - 1] / 100f);
        }

        protected override ResourceRequirement[] InitUpgradeCosts() {
            return new[] {
                new ResourceRequirement(green: 90, diamond: 25),
                new ResourceRequirement(green: 105, diamond: 75),

                new ResourceRequirement(green: 120, diamond: 125),
                new ResourceRequirement(green: 135, diamond: 175),

                new ResourceRequirement(green: 150, diamond: 225),
                new ResourceRequirement(green: 165, diamond: 275, blue: 100),

                new ResourceRequirement(green: 180, diamond: 325, blue: 150),
                new ResourceRequirement(green: 195, diamond: 375, blue: 200),

                new ResourceRequirement(green: 210, diamond: 425, blue: 250),
                new ResourceRequirement(green: 225, diamond: 475, blue: 300),
            };
        }
    }
}