using UnityEngine;


namespace Core {
    public class CreepAttachment_Resource_RBY : CreepAttachmentDefinition {
        static CreepAttachment_Resource_RBY instance = new CreepAttachment_Resource_RBY();
        public static CreepAttachment_Resource_RBY Get() => instance;

        CreepAttachment_Resource_RBY() { }


        static float[] _amounts = {
            40,
            46,

            55,
            67,

            82,
            100,

            121,
            145,

            171,
            200,
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
                $"{(upgradeHint ? $"(<color=yellow>+{upAmnt}</color>)" : "")}<color=green>{amount}</color>\n" +
                $"{(upgradeHint ? $"(<color=yellow>+{upAmnt}</color>)" : "")}<color=green>{amount}</color>\n" +

                $"<size=10000em> </size>\n" +

                TMPSpriteAssetUtility.RESOURCE_RED_EMBED + "\n" +
                TMPSpriteAssetUtility.RESOURCE_BLUE_EMBED + "\n" +
                TMPSpriteAssetUtility.RESOURCE_YELLOW_EMBED;
        }


        public override string GetName(int level) {
            return "Rainbow Collecter";
        }

        public override Sprite GetIcon(int level) {
            return IconResourceCache.resourceRBY;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }

        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
            stage1.AddResourceReward(ResourceType.red, _amounts[level - 1]);
            stage1.AddResourceReward(ResourceType.blue, _amounts[level - 1]);
            stage1.AddResourceReward(ResourceType.yellow, _amounts[level - 1]);
        }

        protected override ResourceAmount[] InitUpgradeCosts() {
            return new[] {
                new ResourceAmount(green: 90, diamond: 25),
                new ResourceAmount(green: 105, diamond: 55),

                new ResourceAmount(green: 130, diamond: 105),
                new ResourceAmount(green: 165, diamond: 175),

                new ResourceAmount(green: 210, diamond: 265),
                new ResourceAmount(green: 265, diamond: 375),

                new ResourceAmount(green: 330, diamond: 505),
                new ResourceAmount(green: 405, diamond: 655),

                new ResourceAmount(green: 490, diamond: 825),
                new ResourceAmount(green: 585, diamond: 1015),
            };
        }

    }
}