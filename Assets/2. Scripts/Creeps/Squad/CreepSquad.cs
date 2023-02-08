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
    public class ResourceAmount {
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


        public ResourceAmount(float green = 0, float red = 0, float blue = 0, float yellow = 0, float diamond = 0) {
            this.green = green;
            this.red = red;
            this.blue = blue;
            this.yellow = yellow;
            this.diamond = diamond;
        }

        /// <summary>
        /// Does this contain at least the resources specified in req
        /// </summary>
        public bool Satisfies(ResourceAmount req) {
            return
                green >= req.green &&
                red >= req.red &&
                blue >= req.blue &&
                yellow >= req.yellow &&
                diamond >= req.diamond;
        }
    }

    public class ResourceCollection : ResourceAmount {
        public new float this[ResourceType r] {
            get {
                return base[r];
            }
            set {
                if (r == ResourceType.green) {
                    green = value;
                    return;
                }
                if (r == ResourceType.red) {
                    red = value;
                    return;
                }
                if (r == ResourceType.blue) {
                    blue = value;
                    return;
                }
                if (r == ResourceType.yellow) {
                    yellow = value;
                    return;
                }
                if (r == ResourceType.diamond) {
                    diamond = value;
                    return;
                }

                throw new System.ArgumentException($"INVALID RESOURCE TYPE: '{r}'");
            }
        }

        public void Reset() {
            green = 0;
            red = 0;
            blue = 0;
            yellow = 0;
            diamond = 0;
        }

        public void Add(ResourceAmount r) {
            green += r[ResourceType.green];
            red += r[ResourceType.red];
            blue += r[ResourceType.blue];
            yellow += r[ResourceType.yellow];
            diamond += r[ResourceType.diamond];
        }
        public void Spend(ResourceAmount cost) {
            green -= cost[ResourceType.green];
            red -= cost[ResourceType.red];
            blue -= cost[ResourceType.blue];
            yellow -= cost[ResourceType.yellow];
            diamond -= cost[ResourceType.diamond];
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
