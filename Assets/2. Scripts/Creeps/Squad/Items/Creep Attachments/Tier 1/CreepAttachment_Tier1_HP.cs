using UnityEngine;



namespace Core {
    public class CreepAttachment_Tier1_HP : CreepAttachmentDefinition {
        static CreepAttachment_Tier1_HP instance = new CreepAttachment_Tier1_HP();
        public static CreepAttachment_Tier1_HP Get() => instance;

        CreepAttachment_Tier1_HP() { }


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
            $"Increases HP by <color=green>{_statScales[0]}</color>%",
            GetDescriptionTextWithUpgradeHelper(0),
            GetDescriptionTextWithUpgradeHelper(1),
            GetDescriptionTextWithUpgradeHelper(2),
            GetDescriptionTextWithUpgradeHelper(3),
            GetDescriptionTextWithUpgradeHelper(4),
            GetDescriptionTextWithUpgradeHelper(5),
            GetDescriptionTextWithUpgradeHelper(6),
            GetDescriptionTextWithUpgradeHelper(7),
            GetDescriptionTextWithUpgradeHelper(8),
            $"Increases HP by <color=green>{_statScales[9]}</color>%",
        };
        static string GetDescriptionTextWithUpgradeHelper(int i) {
            var amount = _statScales[i];
            var upAmnt = _statScales[i + 1] - amount;
            return $"Increases HP by <color=green>{amount}</color>(<color=yellow>+{upAmnt}</color>)%";
        }
        public override string GetName(int level) {
            return "HP Gem";
        }

        public override Sprite GetIcon(int level) {
            switch (level) {
                case 1:
                    return CreepItemIconResourceCache.GemRed1;
                case 2:
                    return CreepItemIconResourceCache.GemRed2;
                case 3:
                    return CreepItemIconResourceCache.GemRed3;
                case 4:
                    return CreepItemIconResourceCache.GemRed4;
                case 5:
                    return CreepItemIconResourceCache.GemRed5;
                case 6:
                    return CreepItemIconResourceCache.GemRed6;
                case 7:
                    return CreepItemIconResourceCache.GemRed7;
                case 8:
                    return CreepItemIconResourceCache.GemRed8;
                case 9:
                    return CreepItemIconResourceCache.GemRed9;
                case 10:
                    return CreepItemIconResourceCache.GemRed10;
            }

            return CreepItemIconResourceCache.GemRed1;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }

        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
            stage2.AddHpScale(_statScales[level - 1] / 100f);
        }

        protected override ResourceRequirement[] InitUpgradeCosts() {
            return new[] {
                new ResourceRequirement(green: 100),
                new ResourceRequirement(green: 125, red: 50),

                new ResourceRequirement(green: 150, red: 100),
                new ResourceRequirement(green: 175, red: 150),

                new ResourceRequirement(green: 200, red: 200),
                new ResourceRequirement(green: 210, red: 250, diamond: 100),

                new ResourceRequirement(green: 220, red: 300, diamond: 150),
                new ResourceRequirement(green: 230, red: 350, diamond: 200),

                new ResourceRequirement(green: 240, red: 400, diamond: 250),
                new ResourceRequirement(green: 250, red: 450, diamond: 300),
            };
        }
    }



}