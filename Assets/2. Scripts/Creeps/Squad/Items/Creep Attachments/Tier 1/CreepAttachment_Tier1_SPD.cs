//using UnityEngine;



//namespace Core {

//    public class CreepAttachment_Tier1_SPD : CreepAttachmentDefinition {
//        static CreepAttachment_Tier1_SPD instance = new CreepAttachment_Tier1_SPD();
//        public static CreepAttachment_Tier1_SPD Get() => instance;

//        CreepAttachment_Tier1_SPD() { }


//        static float[] _statScales = {
//            30,
//            34,
//            39,
//            45,
//            51,
//            59,
//            67,
//            77,
//            88,
//            100,
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
//            var amount = _statScales[i];
//            var upAmnt = upgradeHint ? _statScales[i + 1] - amount : 0;
//            return
//                $"{(upgradeHint ? $"(<color=yellow>+{upAmnt}</color>)" : "")}<color=green>+{amount}%</color>\n" +
//                $"<size=10000em> </size>\n" +
//                $"<color=yellow>Speed</color>\n";
//        }

       
//        public override string GetName(int level) {
//            return "Speed Gem";
//        }

//        public override Sprite GetIcon(int level) {
//            switch (level) {
//                case 1:
//                    return CreepItemIconResourceCache.GemYellow1;
//                case 2:
//                    return CreepItemIconResourceCache.GemYellow2;
//                case 3:
//                    return CreepItemIconResourceCache.GemYellow3;
//                case 4:
//                    return CreepItemIconResourceCache.GemYellow4;
//                case 5:
//                    return CreepItemIconResourceCache.GemYellow5;
//                case 6:
//                    return CreepItemIconResourceCache.GemYellow6;
//                case 7:
//                    return CreepItemIconResourceCache.GemYellow7;
//                case 8:
//                    return CreepItemIconResourceCache.GemYellow8;
//                case 9:
//                    return CreepItemIconResourceCache.GemYellow9;
//                case 10:
//                    return CreepItemIconResourceCache.GemYellow10;
//            }

//            return CreepItemIconResourceCache.GemYellow1;
//        }

//        public override string GetDescription(int level) {
//            return _descCache[level];
//        }

//        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
//            stage2.AddSpdScale(_statScales[level - 1] / 100f);
//        }

//        protected override ResourceAmount[] InitUpgradeCosts() {
//            return new[] {
//                new ResourceAmount(green: 50),
//                new ResourceAmount(green: 60),

//                new ResourceAmount(green: 80),
//                new ResourceAmount(green: 110, yellow: 95),

//                new ResourceAmount(green: 150, yellow: 125),
//                new ResourceAmount(green: 200, yellow: 170),

//                new ResourceAmount(green: 260, yellow: 230),
//                new ResourceAmount(green: 330, yellow: 305),

//                new ResourceAmount(green: 410, yellow: 395),
//                new ResourceAmount(green: 500, yellow: 500),
//            };
//        }
//    }



//}