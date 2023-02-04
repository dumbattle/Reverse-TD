using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Core {
    public class ResourceType {
        public static ResourceType green { get; } = new ResourceType();
        public static ResourceType red { get; } = new ResourceType();
        public static ResourceType blue { get; } = new ResourceType();
        public static ResourceType yellow { get; } = new ResourceType();
        public static ResourceType diamond { get; } = new ResourceType();

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
                CreepAttachment_Tier1_HP.Get(),
                CreepAttachment_Tier1_SPD.Get(),
                CreepAttachment_Tier1_SpawnRate.Get(),
                CreepAttachment_Tier1_Count.Get()
            );
        }
    }

    public class CreepLoadoutSlot {
        public CreepAttachmentInstance currentAttactment { get; } = new CreepAttachmentInstance();
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

       
        public void Recalculate() {
            stage1.Reset();
            stage2.Reset();
            actualDefinition.CopyFrom(baseDefinition);

            globalUpgrades.ApplyModification(stage1);
            loadout.GetApplication(stage1, stage2);


            levelModifiers?.Apply(actualDefinition);
            stage1.Apply(actualDefinition);
            stage2.Apply(actualDefinition);
        }

        public CreepSquad GetDeathSplitSquad() {
            return deathSplitActive ? deathSplitSquad : null;
        }

        public CreepSquad GetCarrierSquad() {
            return carrierActive ? carrierSquad : null;
        }
    }
}
