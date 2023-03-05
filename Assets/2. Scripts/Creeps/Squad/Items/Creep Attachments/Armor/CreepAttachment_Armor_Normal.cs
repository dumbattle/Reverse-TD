using UnityEngine;


namespace Core {
    public class CreepAttachment_Armor_Normal : CreepAttachmentDefinition {
        //*******************************************************************************************************************
        // Singleton
        //*******************************************************************************************************************

        static CreepAttachment_Armor_Normal instance = new CreepAttachment_Armor_Normal();
        public static CreepAttachment_Armor_Normal Get() => instance;

        CreepAttachment_Armor_Normal() { }   
        
        //*******************************************************************************************************************
        // Cache
        //*******************************************************************************************************************

        static int[] _statScales = {
            20,
            22,
            24,
            26,
            28,
            30,
            32,
            34,
            37,
            40,
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
                $"{(upgradeHint ? $"(<color=yellow>+{upAmnt}</color>)" : "")}<color=green>+{amount}</color>\n" +
                $"{(upgradeHint ? $"(<color=yellow>+{upAmnt}</color>)" : "")}<color=green>+{amount}</color>\n" +
                $"{(upgradeHint ? $"(<color=yellow>+{upAmnt}</color>)" : "")}<color=green>+{amount}</color>\n" +

                $"<size=10000em> </size>\n" +

                $"<color=yellow>Normal</color> Armor\n" +
                $"<color=orange>Explosive</color> Armor\n" +
                $"<color=cyan>Energy</color> Armor";
        }

        //*******************************************************************************************************************
        // Implementation
        //*******************************************************************************************************************

        public override string GetName(int level) {
            return "Normal";
        }

        public override Sprite GetIcon(int level) {
            return null;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }

        public override void ApplyModification(int level, CreepStatSet stats) {
            var val = _statScales[level - 1];
            stats.armor.normalRating += val;
            stats.armor.explosiveRating += val;
            stats.armor.energyRating += val;
        }

        protected override ResourceAmount[] InitUpgradeCosts() {
            return new[] {
                new ResourceAmount(green: 1),
                new ResourceAmount(green: 1),

                new ResourceAmount(green: 1),
                new ResourceAmount(green: 1),

                new ResourceAmount(green: 1),
                new ResourceAmount(green: 1),

                new ResourceAmount(green: 1),
                new ResourceAmount(green: 1),

                new ResourceAmount(green: 1),
                new ResourceAmount(green: 1),
            };
            //return new[] {
            //    new ResourceAmount(green: 90),
            //    new ResourceAmount(green: 97),

            //    new ResourceAmount(green: 108),
            //    new ResourceAmount(green: 123),

            //    new ResourceAmount(green: 142),
            //    new ResourceAmount(green: 165),

            //    new ResourceAmount(green: 192),
            //    new ResourceAmount(green: 223),

            //    new ResourceAmount(green: 258),
            //    new ResourceAmount(green: 300),
            //};
        }
    }
}