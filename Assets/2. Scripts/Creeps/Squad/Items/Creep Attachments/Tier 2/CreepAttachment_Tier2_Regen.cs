//using UnityEngine;



//namespace Core {
//    public class CreepAttachment_Tier2_Regen: CreepAttachmentDefinition {
//        static CreepAttachment_Tier2_Regen instance = new CreepAttachment_Tier2_Regen();
//        public static CreepAttachment_Tier2_Regen Get() => instance;

//        CreepAttachment_Tier2_Regen() { }


//        static float[] _regenRate = {
//            5,
//            6,
//            7,
//            8,
//            10,
//            12,
//            14,
//            16,
//            18,
//            20,
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
//            var amount = _regenRate[i];
//            var upAmnt = upgradeHint ? _regenRate[i + 1] - amount : 0;
//            return
//                $"{(upgradeHint ? $"(<color=yellow>+{upAmnt}</color>)" : "")}<color=green>+{amount}%</color>\n" +
//                $"<size=10000em> </size>\n" +
//                $"<color=yellow>HP/s Regeneration</color>\n";
//        }


//        public override string GetName(int level) {
//            return "Regeneration Gem";
//        }

//        public override Sprite GetIcon(int level) {
//            switch (level) {
//                case 1:
//                    return CreepItemIconResourceCache.GemRed1;
//                case 2:
//                    return CreepItemIconResourceCache.GemRed2;
//                case 3:
//                    return CreepItemIconResourceCache.GemRed3;
//                case 4:
//                    return CreepItemIconResourceCache.GemRed4;
//                case 5:
//                    return CreepItemIconResourceCache.GemRed5;
//                case 6:
//                    return CreepItemIconResourceCache.GemRed6;
//                case 7:
//                    return CreepItemIconResourceCache.GemRed7;
//                case 8:
//                    return CreepItemIconResourceCache.GemRed8;
//                case 9:
//                    return CreepItemIconResourceCache.GemRed9;
//                case 10:
//                    return CreepItemIconResourceCache.GemRed10;
//            }

//            return CreepItemIconResourceCache.GemRed1;
//        }

//        public override string GetDescription(int level) {
//            return _descCache[level];
//        }

//        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
//            stage2.regenRate += _regenRate[level - 1] / 100f;
//        }

//        protected override ResourceAmount[] InitUpgradeCosts() {
//            return new[] {
//                new ResourceAmount(red: 50),
//                new ResourceAmount(red: 60),

//                new ResourceAmount(red: 80),
//                new ResourceAmount(red: 110, yellow: 95),

//                new ResourceAmount(red: 150, yellow: 125),
//                new ResourceAmount(red: 200, yellow: 170),

//                new ResourceAmount(red: 260, yellow: 230),
//                new ResourceAmount(red: 330, yellow: 305),

//                new ResourceAmount(red: 410, yellow: 395),
//                new ResourceAmount(red: 500, yellow: 500),
//            };
//        }
//    }



//}