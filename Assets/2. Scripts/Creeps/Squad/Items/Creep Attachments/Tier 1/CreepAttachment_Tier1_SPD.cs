using UnityEngine;



namespace Core {
    public class CreepAttachment_Tier1_SPD : CreepAttachmentDefinition {
        static CreepAttachment_Tier1_SPD instance = new CreepAttachment_Tier1_SPD();
        public static CreepAttachment_Tier1_SPD Get() => instance;

        CreepAttachment_Tier1_SPD() { }


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
            $"Increases Speed by <color=green>{_statScales[0]}</color>%",
            GetDescriptionTextWithUpgradeHelper(0),
            GetDescriptionTextWithUpgradeHelper(1),
            GetDescriptionTextWithUpgradeHelper(2),
            GetDescriptionTextWithUpgradeHelper(3),
            GetDescriptionTextWithUpgradeHelper(4),
            GetDescriptionTextWithUpgradeHelper(5),
            GetDescriptionTextWithUpgradeHelper(6),
            GetDescriptionTextWithUpgradeHelper(7),
            GetDescriptionTextWithUpgradeHelper(8),
            $"Increases Speed by <color=green>{_statScales[9]}</color>%",
        };
        static string GetDescriptionTextWithUpgradeHelper(int i) {
            var amount = _statScales[i];
            var upAmnt = _statScales[i + 1] - amount;
            return $"Increases Speed by <color=green>{amount}</color>(<color=yellow>+{upAmnt}</color>)%";
        }
        public override string GetName(int level) {
            return "Speed Gem";
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
            stage2.AddSpdScale(_statScales[level - 1] / 100f);
        }

        protected override ResourceRequirement[] InitUpgradeCosts() {
            return new[] {
                new ResourceRequirement(green: 100),
                new ResourceRequirement(green: 125, yellow: 50),

                new ResourceRequirement(green: 150, yellow: 100),
                new ResourceRequirement(green: 175, yellow: 150),

                new ResourceRequirement(green: 200, yellow: 200),
                new ResourceRequirement(green: 210, yellow: 250, diamond: 100),

                new ResourceRequirement(green: 220, yellow: 300, diamond: 150),
                new ResourceRequirement(green: 230, yellow: 350, diamond: 200),

                new ResourceRequirement(green: 240, yellow: 400, diamond: 250),
                new ResourceRequirement(green: 250, yellow: 450, diamond: 300),
            };
        }
    }



}