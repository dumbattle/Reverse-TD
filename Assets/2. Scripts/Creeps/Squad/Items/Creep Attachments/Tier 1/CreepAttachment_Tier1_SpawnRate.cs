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
                $"<color=yellow>Spawn Rate</color>\n";
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

        protected override ResourceAmount[] InitUpgradeCosts() {
            return new[] {
                new ResourceAmount(green: 50),
                new ResourceAmount(green: 60),

                new ResourceAmount(green: 80),
                new ResourceAmount(green: 110, blue: 95),

                new ResourceAmount(green: 150, blue: 125),
                new ResourceAmount(green: 200, blue: 170),

                new ResourceAmount(green: 260, blue: 230),
                new ResourceAmount(green: 330, blue: 305),

                new ResourceAmount(green: 410, blue: 395),
                new ResourceAmount(green: 500, blue: 500),
            };
        }
    }



}