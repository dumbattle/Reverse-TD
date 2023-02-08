using UnityEngine;


namespace Core {
    public class CreepAttachment_Specialization_HP2Count : CreepAttachmentDefinition {
        static CreepAttachment_Specialization_HP2Count instance = new CreepAttachment_Specialization_HP2Count();
        public static CreepAttachment_Specialization_HP2Count Get() => instance;

        CreepAttachment_Specialization_HP2Count() { }

        const float SIZE_DECREASE = 30;
        const float HP_DECREASE = 50;

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
                $"{(upgradeHint ? $"(<color=yellow>+{upAmnt}</color>)" : "")}<color=green>+{amount}%</color>\n" +
                $"<color=yellow>-{SIZE_DECREASE}%</color>\n" +
                $"<color=red>-{HP_DECREASE}%</color>\n" +

                $"<size=10000em> </size>\n" +

                $"Base <color=yellow>Count</color>\n" +
                $"Base <color=yellow>Spawn Rate</color>\n" +
                $"Base <color=yellow>Size</color>\n" +
                $"Base <color=yellow>HP</color>";
        }
        public override string GetName(int level) {
            return "Swarm Gem";
        }

        public override Sprite GetIcon(int level) {
            switch (level) {
                case 1:
                case 2:
                    return CreepItemIconResourceCache.GemBlueOutlineRed1;
                case 3:
                case 4:
                    return CreepItemIconResourceCache.GemBlueOutlineRed2;
                case 5:
                case 6:
                    return CreepItemIconResourceCache.GemBlueOutlineRed3;
                case 7:
                case 8:
                    return CreepItemIconResourceCache.GemBlueOutlineRed4;
                case 9:
                case 10:
                    return CreepItemIconResourceCache.GemBlueOutlineRed5;
            }

            return CreepItemIconResourceCache.GemBlueOutlineRed1;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }
        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
            stage1.AddCountScale(_statScales[level - 1] / 100f);
            stage1.AddSpawnRateScale(_statScales[level - 1] / 100f);
            stage1.AddHpScale(1 - (100 / (100f - HP_DECREASE)));
            stage1.AddSizeScale(1 - (100 / (100 - SIZE_DECREASE)));
        }

        protected override ResourceAmount[] InitUpgradeCosts() {
            return new[] {
                new ResourceAmount(green: 90),
                new ResourceAmount(green: 97, blue: 90),

                new ResourceAmount(green: 108, blue: 116, red: 25),
                new ResourceAmount(green: 123, blue: 167, red: 35),

                new ResourceAmount(green: 142, blue: 243, red: 50),
                new ResourceAmount(green: 165, blue: 344, red: 70),

                new ResourceAmount(green: 192, blue: 470, red: 95),
                new ResourceAmount(green: 223, blue: 621, red: 125),

                new ResourceAmount(green: 258, blue: 797, red: 160),
                new ResourceAmount(green: 300, blue: 1000, red: 200),
            };
        }
    }
}