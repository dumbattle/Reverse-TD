using UnityEngine;


namespace Core {
    public class CreepAttachment_Specialization_Count2HP : CreepAttachmentDefinition {
        static CreepAttachment_Specialization_Count2HP instance = new CreepAttachment_Specialization_Count2HP();
        public static CreepAttachment_Specialization_Count2HP Get() => instance;

        CreepAttachment_Specialization_Count2HP() { }
        const float SIZE_INCREASE = 30;
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
                $"<color=yellow>-{SIZE_INCREASE}%</color>\n" +
                $"<color=red>-{COUNT_DECREASE}%</color>\n" +
                $"<color=red>-{COUNT_DECREASE}%</color>\n" +

                $"<size=10000em> </size>\n" +

                $"Base <color=yellow>HP</color>\n" +
                $"Base <color=yellow>Size</color>\n" +
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
                    return CreepItemIconResourceCache.GemRedOutlineBlue1;
                case 3:
                case 4:
                    return CreepItemIconResourceCache.GemRedOutlineBlue2;
                case 5:
                case 6:
                    return CreepItemIconResourceCache.GemRedOutlineBlue3;
                case 7:
                case 8:
                    return CreepItemIconResourceCache.GemRedOutlineBlue4;
                case 9:
                case 10:
                    return CreepItemIconResourceCache.GemRedOutlineBlue5;
            }

            return CreepItemIconResourceCache.GemRedOutlineBlue1;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }


        public override void ApplyModification(int level, CreepStatSet stats) {
            stats.hp.AddModification(_statScales[level - 1] / 100f, 0);
            stats.ScaleRadius((100f + SIZE_INCREASE) / 100f);

            stats.count.AddModification(1 - (100 / (100f - COUNT_DECREASE)), 0);
            stats.spawnRate.AddModification(1 - (100 / (100f - COUNT_DECREASE)), 0);
        }


        protected override ResourceAmount[] InitUpgradeCosts() {
            return new[] {
                new ResourceAmount(green: 90),
                new ResourceAmount(green: 97, red: 90),

                new ResourceAmount(green: 108, red: 116, blue: 25),
                new ResourceAmount(green: 123, red: 167, blue: 35),

                new ResourceAmount(green: 142, red: 243, blue: 50),
                new ResourceAmount(green: 165, red: 344, blue: 70),

                new ResourceAmount(green: 192, red: 470, blue: 95),
                new ResourceAmount(green: 223, red: 621, blue: 125),

                new ResourceAmount(green: 258, red: 797, blue: 160),
                new ResourceAmount(green: 300, red: 1000, blue: 200),
            };
        }
    }
}