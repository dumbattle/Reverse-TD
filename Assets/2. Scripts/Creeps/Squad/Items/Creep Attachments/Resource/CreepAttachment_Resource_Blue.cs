using UnityEngine;


namespace Core {
    public class CreepAttachment_Resource_Blue : CreepAttachmentDefinition {
        static CreepAttachment_Resource_Blue instance = new CreepAttachment_Resource_Blue();
        public static CreepAttachment_Resource_Blue Get() => instance;

        CreepAttachment_Resource_Blue() { }


        static float[] _amounts = {
            100,
            125,

            155,
            190,

            230,
            275,

            325,
            380,

            440,
            500,
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
            var amount = _amounts[i];
            var upAmnt = upgradeHint ? _amounts[i + 1] - amount : 0;
            return
                $"{(upgradeHint ? $"(<color=yellow>+{upAmnt}</color>)" : "")}<color=green>{amount}</color>\n" +

                $"<size=10000em> </size>\n" +

                TMPSpriteAssetUtility.RESOURCE_BLUE_EMBED;
        }


        public override string GetName(int level) {
            return "Blue Collecter";
        }

        public override Sprite GetIcon(int level) {
            return IconResourceCache.resourceBlue;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }

        public override void ApplyModification(int level, CreepStatSet stats) {
            stats.income[ResourceType.blue] += _amounts[level - 1];
        }


        protected override ResourceAmount[] InitUpgradeCosts() {
            return new[] {
                new ResourceAmount(green: 100),
                new ResourceAmount(green: 150),

                new ResourceAmount(green: 225),
                new ResourceAmount(green: 325),

                new ResourceAmount(green: 450),
                new ResourceAmount(green: 600),

                new ResourceAmount(green: 775),
                new ResourceAmount(green: 975),

                new ResourceAmount(green: 1200),
                new ResourceAmount(green: 1500),
            };
        }
    }
}