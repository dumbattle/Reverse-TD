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
                new ResourceAmount(red: 35, blue: 35, yellow: 35),
                new ResourceAmount(red: 50, blue: 50, yellow: 50),

                new ResourceAmount(red: 75, blue: 75, yellow: 75),
                new ResourceAmount(red: 110, blue: 110, yellow: 110),

                new ResourceAmount(red: 155, blue: 155, yellow: 155),
                new ResourceAmount(red: 210, blue: 210, yellow: 210),

                new ResourceAmount(red: 275, blue: 275, yellow: 275),
                new ResourceAmount(red: 350, blue: 350, yellow: 350),

                new ResourceAmount(red: 435, blue: 435, yellow: 435),
                new ResourceAmount(red: 530, blue: 530, yellow: 530),
            };
        }
    }
}