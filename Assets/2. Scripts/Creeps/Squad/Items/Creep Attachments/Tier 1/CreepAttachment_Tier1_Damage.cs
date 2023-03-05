//using UnityEngine;


//namespace Core {
//    public class CreepAttachment_Tier1_Damage : CreepAttachmentDefinition {
//        static CreepAttachment_Tier1_Damage instance = new CreepAttachment_Tier1_Damage();
//        public static CreepAttachment_Tier1_Damage Get() => instance;

//        CreepAttachment_Tier1_Damage() { }


//        static float[] _dmgScales = {
//            100,
//            120,
//            140,
//            160,
//            180,
//            200,
//            225,
//            250,
//            275,
//            300,
//        };

//        static string[] _descCache = {
//            GetDescriptionTextWithUpgradeHelper(0, false),
//            GetDescriptionTextWithUpgradeHelper(0, true),
//            GetDescriptionTextWithUpgradeHelper(1, true),
//            GetDescriptionTextWithUpgradeHelper(2, true),
//            GetDescriptionTextWithUpgradeHelper(3, true),
//            GetDescriptionTextWithUpgradeHelper(4, true),
//            GetDescriptionTextWithUpgradeHelper(5, true),
//            GetDescriptionTextWithUpgradeHelper(6, true),
//            GetDescriptionTextWithUpgradeHelper(7, true),
//            GetDescriptionTextWithUpgradeHelper(8, true),
//            GetDescriptionTextWithUpgradeHelper(9, false)
//        };

//        static string GetDescriptionTextWithUpgradeHelper(int i, bool upgradeHint) {
//            var amount = _dmgScales[i];
//            var upAmnt = upgradeHint ? _dmgScales[i + 1] - amount : 0;
//            return
//                $"{(upgradeHint ? $"(<color=yellow>+{upAmnt}</color>)" : "")}<color=green>+{amount}%</color>\n" +

//                $"<size=10000em> </size>\n" +

//                $"Base <color=yellow>Damage</color>\n";
//        }

//        public override string GetName(int level) {
//            return "Damage Gem";
//        }

//        public override Sprite GetIcon(int level) {
//            switch (level) {
//                case 1:
//                case 2:
//                    return CreepItemIconResourceCache.GemRedOutlineYellow1;
//                case 3:
//                case 4:
//                    return CreepItemIconResourceCache.GemRedOutlineYellow2;
//                case 5:
//                case 6:
//                    return CreepItemIconResourceCache.GemRedOutlineYellow3;
//                case 7:
//                case 8:
//                    return CreepItemIconResourceCache.GemRedOutlineYellow4;
//                case 9:
//                case 10:
//                    return CreepItemIconResourceCache.GemRedOutlineYellow5;
//            }

//            return CreepItemIconResourceCache.GemRedOutlineYellow1;
//        }

//        public override string GetDescription(int level) {
//            return _descCache[level];
//        }

//        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
//            stage1.AddDamage(_dmgScales[level - 1] / 100f);

//            //stage1.AddCountScale(-1);
//            //stage1.AddSpawnRateScale(-1);
//        }

//        protected override ResourceAmount[] InitUpgradeCosts() {
//            return new[] {
//                new ResourceAmount(green: 100, yellow: 25, red: 25),
//                new ResourceAmount(green: 150, yellow: 40, red: 40),

//                new ResourceAmount(green: 200, yellow: 55, red: 55),
//                new ResourceAmount(green: 250, yellow: 70, red: 70),

//                new ResourceAmount(green: 300, yellow: 85, red: 85),
//                new ResourceAmount(green: 350, yellow: 100, red: 100),

//                new ResourceAmount(green: 400, yellow: 115, red: 115),
//                new ResourceAmount(green: 450, yellow: 130, red: 130),

//                new ResourceAmount(green: 500, yellow: 145, red: 145),
//                new ResourceAmount(green: 550, yellow: 160, red: 160),
//            };
//        }
//    }
//}