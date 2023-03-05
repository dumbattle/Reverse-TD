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
                case 2:
                    return CreepItemIconResourceCache.GemYellowOutlineBlue1;
                case 3:
                case 4:
                    return CreepItemIconResourceCache.GemYellowOutlineBlue2;
                case 5:
                case 6:
                    return CreepItemIconResourceCache.GemYellowOutlineBlue3;
                case 7:
                case 8:
                    return CreepItemIconResourceCache.GemYellowOutlineBlue4;
                case 9:
                case 10:
                    return CreepItemIconResourceCache.GemYellowOutlineBlue5;
            }

            return CreepItemIconResourceCache.GemYellowOutlineBlue1;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }


        public override void ApplyModification(int level, CreepStatSet stats) {
            stats.spd.AddModification(_statScales[level - 1] / 100f, 0);

            stats.count.AddModification(1 - (100 / (100f - COUNT_DECREASE)), 0);
            stats.spawnRate.AddModification(1 - (100 / (100f - COUNT_DECREASE)), 0);
        }

        protected override ResourceAmount[] InitUpgradeCosts() {
            return new[] {
                new ResourceAmount(green: 90),
                new ResourceAmount(green: 97, yellow: 90),

                new ResourceAmount(green: 108, yellow: 116, blue: 25),
                new ResourceAmount(green: 123, yellow: 167, blue: 35),

                new ResourceAmount(green: 142, yellow: 243, blue: 50),
                new ResourceAmount(green: 165, yellow: 344, blue: 70),

                new ResourceAmount(green: 192, yellow: 470, blue: 95),
                new ResourceAmount(green: 223, yellow: 621, blue: 125),

                new ResourceAmount(green: 258, yellow: 797, blue: 160),
                new ResourceAmount(green: 300, yellow: 1000, blue: 200),
            };
        }
    }
}