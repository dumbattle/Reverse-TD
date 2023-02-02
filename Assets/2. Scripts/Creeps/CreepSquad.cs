using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Core {
    public class ResourceType {
        public static ResourceType green { get; }
        public static ResourceType red { get; }
        public static ResourceType blue { get; }
        public static ResourceType yellow { get; }
        public static ResourceType diamond { get; }

        ResourceType() { }
    }
    public class ResourceRequirement {
        public float this[ResourceType r] {
            get {
                if (r == ResourceType.green) {
                    return green;
                }
                if (r == ResourceType.red) {
                    return red;
                }
                if (r == ResourceType.blue) {
                    return blue;
                }
                if (r == ResourceType.yellow) {
                    return yellow;
                }
                if (r == ResourceType.diamond) {
                    return diamond;
                }

                throw new System.ArgumentException($"INVALID RESOURCE TYPE: '{r}'");
            }
        }
       
        protected float green;
        protected float red;
        protected float blue;
        protected float yellow;
        protected float diamond;


        public ResourceRequirement(float green = 0, float red = 0, float blue = 0, float yellow = 0, float diamond = 0) {
            this.green = green;
            this.red = red;
            this.blue = blue;
            this.yellow = yellow;
            this.diamond = diamond;
        }
    }

    public class ResourceCollection : ResourceRequirement {
        public new float this[ResourceType r] {
            get {
                return base[r];
            }
            set {
                if (r == ResourceType.green) {
                    green = value;
                }
                if (r == ResourceType.red) {
                    red = value;
                }
                if (r == ResourceType.blue) {
                    blue = value;
                }
                if (r == ResourceType.yellow) {
                    yellow = value;
                }
                if (r == ResourceType.diamond) {
                    diamond = value;
                }

                throw new System.ArgumentException($"INVALID RESOURCE TYPE: '{r}'");
            }
        }
    }

    public class CreepAttachment {
        public CreepAttachmentDefinition definition;
        /// <summary>
        /// Between [1, 10] inclusive
        /// </summary>
        public int level = 1;


        public Sprite GetIcon() {
            return definition?.GetIcon(level) ?? null;
        }
    }

    public class CreepLoadout {
        public CreepLoadoutSlot specialization { get; private set; } = new CreepLoadoutSlot();
        public CreepLoadoutSlot resource { get; private set; } = new CreepLoadoutSlot();

        public CreepLoadoutSlot tier1_1 { get; private set; } = GetTier1Slot();
        public CreepLoadoutSlot tier1_2 { get; private set; } = GetTier1Slot();
        public CreepLoadoutSlot tier1_3 { get; private set; } = GetTier1Slot();

        public CreepLoadoutSlot tier2_1 { get; private set; } = new CreepLoadoutSlot();
        public CreepLoadoutSlot tier2_2 { get; private set; } = new CreepLoadoutSlot();

        public CreepLoadoutSlot tier3_A { get; private set; } = new CreepLoadoutSlot();
        public CreepLoadoutSlot tier3_B { get; private set; } = new CreepLoadoutSlot();

        public void GetApplication(CreepStatModification stage1, CreepStatModification stage2) {
            specialization.GetApplication(stage1, stage2);
            resource.GetApplication(stage1, stage2);

            tier1_1.GetApplication(stage1, stage2);
            tier1_2.GetApplication(stage1, stage2);
            tier1_3.GetApplication(stage1, stage2);

            tier2_1.GetApplication(stage1, stage2);
            tier2_2.GetApplication(stage1, stage2);

            tier3_A.GetApplication(stage1, stage2);
            tier3_B.GetApplication(stage1, stage2);
        }

        static CreepLoadoutSlot GetTier1Slot() {
            return new CreepLoadoutSlot(
                CreepAttachment_Tier1_HP.Get()
            );
        }
    }

    public class CreepLoadoutSlot {
        public CreepAttachment currentAttactment { get; } = new CreepAttachment();
        public CreepAttachmentDefinition[] allowedAttachments { get; private set; }

        public CreepLoadoutSlot(params CreepAttachmentDefinition[] allowed) {
            allowedAttachments = allowed;
        }
        public void GetApplication(CreepStatModification stage1, CreepStatModification stage2) {
            if (currentAttactment.definition == null) {
                return;
            }

            currentAttactment.definition.ApplyModification(currentAttactment.level, stage1, stage2);
        }
    }


    public abstract class CreepAttachmentDefinition {
        ResourceRequirement[] _upgradeCosts;

        public CreepAttachmentDefinition() {
            _upgradeCosts = InitUpgradeCosts();
        }

        /// <summary>
        /// To unlock provided level.
        /// </summary>
        public ResourceRequirement  GetCost(int level) {
            level = Mathf.Clamp(level, 0, _upgradeCosts.Length - 1);

            return _upgradeCosts[level];
        }

        /// <summary>
        /// Level = 0 for menu description
        /// </summary>
        public abstract string GetDescription(int level);

        /// <summary>
        /// Level = 0 for menu name 
        /// </summary>
        public abstract string GetName(int level);

        public abstract Sprite GetIcon(int level);

        public abstract void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2);


        protected abstract ResourceRequirement[] InitUpgradeCosts();
    }

    public class CreepAttachment_Tier1_HP : CreepAttachmentDefinition {
        static CreepAttachment_Tier1_HP instance = new CreepAttachment_Tier1_HP();
        public static CreepAttachment_Tier1_HP Get() => instance;

        CreepAttachment_Tier1_HP() { }
        
        
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
        
        static string[] _nameCache = {
            "HP Gem",
            "HP Gem - I ",
            "HP Gem - II",
            "HP Gem - III",
            "HP Gem - IV",
            "HP Gem - V",
            "HP Gem - VI",
            "HP Gem - VII",
            "HP Gem - VIII",
            "HP Gem - IX",
            "HP Gem - X",
        };

        static string[] _descCache = {
            $"Increases HP by {_statScales[0]}%",
            $"Increases HP by {_statScales[0]}%",
            $"Increases HP by {_statScales[1]}%",
            $"Increases HP by {_statScales[2]}%",
            $"Increases HP by {_statScales[3]}%",
            $"Increases HP by {_statScales[4]}%",
            $"Increases HP by {_statScales[5]}%",
            $"Increases HP by {_statScales[6]}%",
            $"Increases HP by {_statScales[7]}%",
            $"Increases HP by {_statScales[8]}%",
            $"Increases HP by {_statScales[9]}%",
        };

        public override string GetName(int level) {
            return _nameCache[level];
        }

        public override Sprite GetIcon(int level) {
            switch (level) {
                case 1:
                    return CreepItemIconResourceCache.GemRed1;
                case 2:
                    return CreepItemIconResourceCache.GemRed2;
                case 3:
                    return CreepItemIconResourceCache.GemRed3;
                case 4:
                    return CreepItemIconResourceCache.GemRed4;
                case 5:
                    return CreepItemIconResourceCache.GemRed5;
                case 6:
                    return CreepItemIconResourceCache.GemRed6;
                case 7:
                    return CreepItemIconResourceCache.GemRed7;
                case 8:
                    return CreepItemIconResourceCache.GemRed8;
                case 9:
                    return CreepItemIconResourceCache.GemRed9;
                case 10:
                    return CreepItemIconResourceCache.GemRed10;
            }

            return CreepItemIconResourceCache.GemRed1;
        }

        public override string GetDescription(int level) {
            return _descCache[level];
        }

        public override void ApplyModification(int level, CreepStatModification stage1, CreepStatModification stage2) {
            stage2.AddHpScale(_statScales[level - 1] / 100f);
        }

        protected override ResourceRequirement[] InitUpgradeCosts() {
            return new[] { 
                new ResourceRequirement(green: 100),
                new ResourceRequirement(green: 125, red: 50),

                new ResourceRequirement(green: 150, red: 100),
                new ResourceRequirement(green: 175, red: 150),

                new ResourceRequirement(green: 200, red: 200),
                new ResourceRequirement(green: 225, red: 250, diamond: 100),

                new ResourceRequirement(green: 250, red: 300, diamond: 200),
                new ResourceRequirement(green: 275, red: 350, diamond: 300),

                new ResourceRequirement(green: 300, red: 400, diamond: 400),
                new ResourceRequirement(green: 325, red: 450, diamond: 500),
            };
        }
    }







    public class CreepSquad {
        CreepDefinition baseDefinition;
        public CreepDefinition actualDefinition { get; private set; }

        public CreepSquad parentSquad { get; private set; }
        CreepSquad deathSplitSquad;
        CreepSquad carrierSquad;

        public bool isChild => isDeathSpawn || isCarrierSpawn;
        public bool isDeathSpawn { get; private set; }
        public bool isCarrierSpawn { get; private set; }


        bool deathSplitActive;
        bool carrierActive;

        //**********************************************************************************************
        // Collections
        //**********************************************************************************************
        public CreepLoadout loadout = new CreepLoadout();
        //List<IPlayerItem> allAttachments = new List<IPlayerItem>();
        //List<ICreepDefinitionModifier> level1Modifier = new List<ICreepDefinitionModifier>();
        //List<ICreepDefinitionModifier> level2Modifier = new List<ICreepDefinitionModifier>();
        //List<ICreepDefinitionModifier> level3Modifier = new List<ICreepDefinitionModifier>();

        //**********************************************************************************************
        // Upgrades
        //**********************************************************************************************

        GlobalCreeepUpgrades globalUpgrades;
        CreepStatModification levelModifiers;

        CreepStatModification stage1 = new CreepStatModification();
        CreepStatModification stage2 = new CreepStatModification();

        public CreepSquad(CreepDefinition def, GlobalCreeepUpgrades globalUpgrades, CreepStatModification levelModifiers) {
            this.levelModifiers = levelModifiers;
            baseDefinition = def;
            actualDefinition = def.CreateCopy();
            this.globalUpgrades = globalUpgrades;

            Recalculate();
        }

        public void AddModifier(IPlayerItem item) {
            //allAttachments.Add(item);
            //if (item is CreepAttatchment atch) {
            //    GetLevelList(atch.GetLevel()).Add(atch);
            //    Recalculate();
            //}
        }
        
        public void RemoveItem(int index) {
            //var item = allAttachments[index];
            //allAttachments.RemoveAt(index);
            //if (item is CreepAttatchment atch) {
            //    GetLevelList(atch.GetLevel()).Remove(atch);
            //    Recalculate();
            //}
        }
        
        public int NumAttachments() {
            return 0;
            //return allAttachments.Count;
        }

        public IPlayerItem GetAttachment(int index) {
            return null;
            //if (index >= allAttachments.Count) {
            //    return null;
            //}
            //return allAttachments[index];
        }
       
        public void Recalculate() {
            stage1.Reset();
            stage2.Reset();
            actualDefinition.CopyFrom(baseDefinition);

            globalUpgrades.ApplyModification(stage1);
            loadout.GetApplication(stage1, stage2);


            levelModifiers?.Apply(actualDefinition);
            stage1.Apply(actualDefinition);
            stage2.Apply(actualDefinition);

            //foreach (var l in level1Modifier) {
            //    l.ApplyModification(stage1);
            //}

            //foreach (var l in level2Modifier) {
            //    l.ApplyModification(stage2);
            //}

            //levelModifiers?.Apply(actualDefinition);
            //stage1.Apply(actualDefinition);
            //stage2.Apply(actualDefinition);

            //// death split child
            //actualDefinition.deathSplitDefinition = null;
            //int deathSplitCount = stage1.deathSpawnLevel + stage2.deathSpawnLevel;
            //deathSplitActive = false;
            //if (!isChild && deathSplitCount > 0) {
            //    if (deathSplitSquad == null) {
            //        deathSplitSquad = new CreepSquad(CreepSelectionUtility.GetRandomNewCreep(), globalUpgrades, null);
            //        deathSplitSquad.parentSquad = this;
            //        deathSplitSquad.baseDefinition.speed *= .8f;
            //        deathSplitSquad.baseDefinition.radius /= 2f;
            //        deathSplitSquad.isDeathSpawn = true;
            //        deathSplitSquad.baseDefinition.glowColor = UnityEngine.Color.black;
            //    }

            //    deathSplitSquad.baseDefinition.hp = baseDefinition.hp / (1f + deathSplitCount);
            //    deathSplitSquad.baseDefinition.moneyReward = baseDefinition.moneyReward / (1f + deathSplitCount);
            //    deathSplitSquad.baseDefinition.count = deathSplitCount;
            //    deathSplitSquad.baseDefinition.spawnRate = 0;

            //    deathSplitSquad.Recalculate();
            //    actualDefinition.deathSplitDefinition = deathSplitSquad.actualDefinition;
            //    deathSplitActive = true;
            //}

            //// carrier child
            //actualDefinition.carrierDefinition = null;
            //int carrierLevel = stage1.carrierSpawnLevel + stage2.carrierSpawnLevel;
            //carrierActive = false;
            //if (!isChild && carrierLevel > 0) {
            //    if (carrierSquad == null) {
            //        carrierSquad = new CreepSquad(CreepSelectionUtility.GetRandomNewCreep(), globalUpgrades, null);
            //        carrierSquad.parentSquad = this;
            //        carrierSquad.baseDefinition.radius /= 2f;
            //        carrierSquad.isCarrierSpawn = true;
            //        carrierSquad.baseDefinition.glowColor = UnityEngine.Color.white;
            //    }
            //    carrierSquad.baseDefinition.speed = baseDefinition.speed * 1.5f;

            //    carrierSquad.baseDefinition.hp = baseDefinition.hp / 40f * (10 + carrierLevel);
            //    carrierSquad.baseDefinition.moneyReward = baseDefinition.moneyReward / 2;
            //    carrierSquad.baseDefinition.count = 1;
            //    carrierSquad.baseDefinition.spawnRate = 0.08f + 0.02f * carrierLevel;

            //    carrierSquad.Recalculate();
            //    actualDefinition.carrierDefinition = carrierSquad.actualDefinition;
            //    carrierActive = true;
            //}
        }

        public CreepSquad GetDeathSplitSquad() {
            return deathSplitActive ? deathSplitSquad : null;
        }

        public CreepSquad GetCarrierSquad() {
            return carrierActive ? carrierSquad : null;
        }
    }
}
