using UnityEngine;


namespace Core {
    public class CreepAttachment_Specialization_Spd2HP : CreepAttachmentDefinition {
        static CreepAttachment_Specialization_Spd2HP instance = new CreepAttachment_Specialization_Spd2HP();
        public static CreepAttachment_Specialization_Spd2HP Get() => instance;

        CreepAttachment_Specialization_Spd2HP() { }
        const float SIZE_INCREASE = 15;


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
                $"<color=red>-50%</color>\n" +

                $"<size=10000em> </size>\n" +

                $"Base <color=yellow>HP</color>\n" +
                $"Base <color=yellow>Size</color>\n" +
                $"Base <color=yellow>SPD</color>";
        }

        public override string GetName(int level) {
            return "Tank Gem";
        }

        public override Sprite GetIcon(int level) {
            switch (level) {
                case 1:
                case 2:
                    return CreepItemIconResourceCache.GemRedOutlineYellow1;
                case 3:
                case 4:
                    return CreepItemIconResourceCache.GemRedOutlineYellow2;
                case 5:
                case 6:
                    return CreepItemIconResourceCache.GemRedOutlineYellow3;
                case 7:
                case 8:
                    return CreepItemIconResourceCache.GemRedOutlineYellow4;
                case 9:
                case 10:
                    return CreepItemIconResourceCache.GemRedOutlineYellow5;
            }

            return CreepItemIconResourceCache.GemRedOutlineYellow1;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }

        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
            stage1.AddHpScale(_statScales[level - 1] / 100f);
            stage1.AddSizeScale(SIZE_INCREASE / 100);

            stage1.AddSpdScale(-1);

            //stage1.AddCountScale(-1);
            //stage1.AddSpawnRateScale(-1);
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