using UnityEngine;


namespace Core {
    public class CreepAttachment_Specialization_Count2SPD : CreepAttachmentDefinition {
        static CreepAttachment_Specialization_Count2SPD instance = new CreepAttachment_Specialization_Count2SPD();
        public static CreepAttachment_Specialization_Count2SPD Get() => instance;

        CreepAttachment_Specialization_Count2SPD() { }

        const float COUNT_DECREASE = 50;

        static float[] _statScales = {
            50,
            57,
            65,
            74,
            84,
            95,
            107,
            120,
            134,
            150,
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
                $"<color=red>-{COUNT_DECREASE}%</color>\n" +
                $"<color=red>-{COUNT_DECREASE}%</color>\n" +

                $"<size=10000em> </size>\n" +

                $"Base <color=yellow>Speed</color>\n" +
                $"Base <color=yellow>Count</color>\n" +
                $"Base <color=yellow>Spawn Rate</color>";
        }


        public override string GetName(int level) {
            return "Fat Gem";
        }

        public override Sprite GetIcon(int level) {
            switch (level) {
                case 1:
                    return CreepItemIconResourceCache.GemYellow1;
                case 2:
                    return CreepItemIconResourceCache.GemYellow2;
                case 3:
                    return CreepItemIconResourceCache.GemYellow3;
                case 4:
                    return CreepItemIconResourceCache.GemYellow4;
                case 5:
                    return CreepItemIconResourceCache.GemYellow5;
                case 6:
                    return CreepItemIconResourceCache.GemYellow6;
                case 7:
                    return CreepItemIconResourceCache.GemYellow7;
                case 8:
                    return CreepItemIconResourceCache.GemYellow8;
                case 9:
                    return CreepItemIconResourceCache.GemYellow9;
                case 10:
                    return CreepItemIconResourceCache.GemYellow10;
            }

            return CreepItemIconResourceCache.GemYellow1;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }

        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
            stage1.AddSpdScale(_statScales[level - 1] / 100f);

            stage1.AddCountScale(1 - (100 / (100f - COUNT_DECREASE)));
            stage1.AddSpawnRateScale(1 - (100 / (100f - COUNT_DECREASE)));
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