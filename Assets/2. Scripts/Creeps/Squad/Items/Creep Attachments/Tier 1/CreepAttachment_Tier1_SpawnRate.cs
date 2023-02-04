using UnityEngine;



namespace Core {
    public class CreepAttachment_Tier1_SpawnRate : CreepAttachmentDefinition {
        static CreepAttachment_Tier1_SpawnRate instance = new CreepAttachment_Tier1_SpawnRate();
        public static CreepAttachment_Tier1_SpawnRate Get() => instance;

        CreepAttachment_Tier1_SpawnRate() { }


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
            $"Increases Spawn Rate by <color=green>{_statScales[0]}</color>%",
            GetDescriptionTextWithUpgradeHelper(0),
            GetDescriptionTextWithUpgradeHelper(1),
            GetDescriptionTextWithUpgradeHelper(2),
            GetDescriptionTextWithUpgradeHelper(3),
            GetDescriptionTextWithUpgradeHelper(4),
            GetDescriptionTextWithUpgradeHelper(5),
            GetDescriptionTextWithUpgradeHelper(6),
            GetDescriptionTextWithUpgradeHelper(7),
            GetDescriptionTextWithUpgradeHelper(8),
            $"Increases Spawn Rate by <color=green>{_statScales[9]}</color>%",
        };
        static string GetDescriptionTextWithUpgradeHelper(int i) {
            var amount = _statScales[i];
            var upAmnt = _statScales[i + 1] - amount;
            return $"Increases Spawn Rate by <color=green>{amount}</color>(<color=yellow>+{upAmnt}</color>)%";
        }
        public override string GetName(int level) {
            return "Spawn Rate Gem";
        }

        public override Sprite GetIcon(int level) {
            switch (level) {
                case 1:
                    return CreepItemIconResourceCache.GemBlue1;
                case 2:
                    return CreepItemIconResourceCache.GemBlue2;
                case 3:
                    return CreepItemIconResourceCache.GemBlue3;
                case 4:
                    return CreepItemIconResourceCache.GemBlue4;
                case 5:
                    return CreepItemIconResourceCache.GemBlue5;
                case 6:
                    return CreepItemIconResourceCache.GemBlue6;
                case 7:
                    return CreepItemIconResourceCache.GemBlue7;
                case 8:
                    return CreepItemIconResourceCache.GemBlue8;
                case 9:
                    return CreepItemIconResourceCache.GemBlue9;
                case 10:
                    return CreepItemIconResourceCache.GemBlue10;
            }

            return CreepItemIconResourceCache.GemBlue1;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }

        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
            stage2.AddSpawnRateScale(_statScales[level - 1] / 100f);
        }

        protected override ResourceRequirement[] InitUpgradeCosts() {
            return new[] {
                new ResourceRequirement(green: 100),
                new ResourceRequirement(green: 125, blue: 50),

                new ResourceRequirement(green: 150, blue: 100),
                new ResourceRequirement(green: 175, blue: 150),

                new ResourceRequirement(green: 200, blue: 200),
                new ResourceRequirement(green: 210, blue: 250, diamond: 100),

                new ResourceRequirement(green: 220, blue: 300, diamond: 150),
                new ResourceRequirement(green: 230, blue: 350, diamond: 200),

                new ResourceRequirement(green: 240, blue: 400, diamond: 250),
                new ResourceRequirement(green: 250, blue: 450, diamond: 300),
            };
        }
    }



}