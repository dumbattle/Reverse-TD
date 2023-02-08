using UnityEngine;


namespace Core {
    public class CreepAttachment_Resource_Diamond : CreepAttachmentDefinition {
        static CreepAttachment_Resource_Diamond instance = new CreepAttachment_Resource_Diamond();
        public static CreepAttachment_Resource_Diamond Get() => instance;

        CreepAttachment_Resource_Diamond() { }


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

                TMPSpriteAssetUtility.RESOURCE_DIAMOND_EMBED;
        }


        public override string GetName(int level) {
            return "Diamond Collecter";
        }

        public override Sprite GetIcon(int level) {
            return IconResourceCache.resourceDiamond;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }

        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
            stage1.AddResourceReward(ResourceType.diamond, _amounts[level - 1]);
        }

        protected override ResourceAmount[] InitUpgradeCosts() {
            return new[] {
                new ResourceAmount(green: 90, diamond: 25),
                new ResourceAmount(green: 105, diamond: 75),

                new ResourceAmount(green: 120, diamond: 125),
                new ResourceAmount(green: 135, diamond: 175),

                new ResourceAmount(green: 150, diamond: 225),
                new ResourceAmount(green: 165, diamond: 275, blue: 100),

                new ResourceAmount(green: 180, diamond: 325, blue: 150),
                new ResourceAmount(green: 195, diamond: 375, blue: 200),

                new ResourceAmount(green: 210, diamond: 425, blue: 250),
                new ResourceAmount(green: 225, diamond: 475, blue: 300),
            };
        }
    }
}