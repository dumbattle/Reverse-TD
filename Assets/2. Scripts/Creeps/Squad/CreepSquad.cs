using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Core {
    public class CreepStatSet {
        public BasicCreepStat hp { get; private set; } = BasicCreepStat.NewHp();
        public BasicCreepStat spd { get; private set; } = BasicCreepStat.NewSpd();
        public BasicCreepStat count { get; private set; } = BasicCreepStat.NewCount();
        public BasicCreepStat spawnRate { get; private set; } = BasicCreepStat.NewSpawnRate();
        public BasicCreepStat damageMult { get; private set; } = BasicCreepStat.NewDamageMult();
        public BasicCreepStat incomeMult { get; private set; } = BasicCreepStat.NewIncomeMult();
    }

    public class BasicCreepStat {
        static Level[] _hp = {
            new Level(100, new ResourceAmount()),
            new Level(115, new ResourceAmount(green: 50)),
            new Level(130, new ResourceAmount(green: 55)),
            new Level(145, new ResourceAmount(green: 60)),
            new Level(160, new ResourceAmount(green: 65)),

            new Level(175, new ResourceAmount(green: 70)),
            new Level(190, new ResourceAmount(green: 75)),
            new Level(205, new ResourceAmount(green: 80)),
            new Level(220, new ResourceAmount(green: 85)),
            new Level(235, new ResourceAmount(green: 90)),


            new Level(260, new ResourceAmount(green: 95)),
            new Level(285, new ResourceAmount(green: 100)),
            new Level(310, new ResourceAmount(green: 105)),
            new Level(335, new ResourceAmount(green: 110)),
            new Level(360, new ResourceAmount(green: 115)),

            new Level(385, new ResourceAmount(green: 120)),
            new Level(410, new ResourceAmount(green: 125)),
            new Level(435, new ResourceAmount(green: 130)),
            new Level(460, new ResourceAmount(green: 135)),
            new Level(485, new ResourceAmount(green: 140)),


            new Level(520, new ResourceAmount(green: 145)),
            new Level(555, new ResourceAmount(green: 150)),
            new Level(590, new ResourceAmount(green: 155)),
            new Level(625, new ResourceAmount(green: 160)),
            new Level(660, new ResourceAmount(green: 165)),

            new Level(695, new ResourceAmount(green: 170)),
            new Level(730, new ResourceAmount(green: 175)),
            new Level(765, new ResourceAmount(green: 180)),
            new Level(800, new ResourceAmount(green: 185)),
            new Level(835, new ResourceAmount(green: 190)),

        };

        static Level[] _spd = {
            new Level(2.1f, new ResourceAmount()),
            new Level(2.2f, new ResourceAmount(green: 50)),
            new Level(2.3f, new ResourceAmount(green: 55)),
            new Level(2.4f, new ResourceAmount(green: 60)),
            new Level(2.5f, new ResourceAmount(green: 65)),

            new Level(2.6f, new ResourceAmount(green: 70)),
            new Level(2.7f, new ResourceAmount(green: 75)),
            new Level(2.8f, new ResourceAmount(green: 80)),
            new Level(2.9f, new ResourceAmount(green: 85)),
            new Level(3.0f, new ResourceAmount(green: 90)),


            new Level(3.1f, new ResourceAmount(green: 95)),
            new Level(3.2f, new ResourceAmount(green: 100)),
            new Level(3.3f, new ResourceAmount(green: 105)),
            new Level(3.4f, new ResourceAmount(green: 110)),
            new Level(3.5f, new ResourceAmount(green: 115)),

            new Level(3.6f, new ResourceAmount(green: 120)),
            new Level(3.7f, new ResourceAmount(green: 125)),
            new Level(3.8f, new ResourceAmount(green: 130)),
            new Level(3.9f, new ResourceAmount(green: 135)),
            new Level(4.0f, new ResourceAmount(green: 140)),


            new Level(4.1f, new ResourceAmount(green: 145)),
            new Level(4.2f, new ResourceAmount(green: 150)),
            new Level(4.3f, new ResourceAmount(green: 155)),
            new Level(4.4f, new ResourceAmount(green: 160)),
            new Level(4.5f, new ResourceAmount(green: 165)),

            new Level(4.6f, new ResourceAmount(green: 170)),
            new Level(4.7f, new ResourceAmount(green: 175)),
            new Level(4.8f, new ResourceAmount(green: 180)),
            new Level(4.9f, new ResourceAmount(green: 185)),
            new Level(5.0f, new ResourceAmount(green: 190)),

        };

        static Level[] _count = {
            new Level(10, new ResourceAmount()),
            new Level(11, new ResourceAmount(green: 50)),
            new Level(12, new ResourceAmount(green: 55)),
            new Level(13, new ResourceAmount(green: 60)),
            new Level(14, new ResourceAmount(green: 65)),

            new Level(15, new ResourceAmount(green: 70)),
            new Level(16, new ResourceAmount(green: 75)),
            new Level(17, new ResourceAmount(green: 80)),
            new Level(18, new ResourceAmount(green: 85)),
            new Level(19, new ResourceAmount(green: 90)),


            new Level(20, new ResourceAmount(green: 95)),
            new Level(21, new ResourceAmount(green: 100)),
            new Level(22, new ResourceAmount(green: 105)),
            new Level(23, new ResourceAmount(green: 110)),
            new Level(24, new ResourceAmount(green: 115)),

            new Level(25, new ResourceAmount(green: 120)),
            new Level(26, new ResourceAmount(green: 125)),
            new Level(27, new ResourceAmount(green: 130)),
            new Level(28, new ResourceAmount(green: 135)),
            new Level(29, new ResourceAmount(green: 140)),


            new Level(30, new ResourceAmount(green: 145)),
            new Level(31, new ResourceAmount(green: 150)),
            new Level(32, new ResourceAmount(green: 155)),
            new Level(33, new ResourceAmount(green: 160)),
            new Level(34, new ResourceAmount(green: 165)),

            new Level(35, new ResourceAmount(green: 170)),
            new Level(36, new ResourceAmount(green: 175)),
            new Level(37, new ResourceAmount(green: 180)),
            new Level(38, new ResourceAmount(green: 185)),
            new Level(40, new ResourceAmount(green: 190)),

        };

        static Level[] _spawnRate = {
            new Level(2.0f, new ResourceAmount()),
            new Level(2.1f, new ResourceAmount(green: 50)),
            new Level(2.2f, new ResourceAmount(green: 55)),
            new Level(2.3f, new ResourceAmount(green: 60)),
            new Level(2.4f, new ResourceAmount(green: 65)),

            new Level(2.5f, new ResourceAmount(green: 70)),
            new Level(2.6f, new ResourceAmount(green: 75)),
            new Level(2.7f, new ResourceAmount(green: 80)),
            new Level(2.8f, new ResourceAmount(green: 85)),
            new Level(2.9f, new ResourceAmount(green: 90)),


            new Level(3.0f, new ResourceAmount(green: 95)),
            new Level(3.1f, new ResourceAmount(green: 100)),
            new Level(3.2f, new ResourceAmount(green: 105)),
            new Level(3.3f, new ResourceAmount(green: 110)),
            new Level(3.4f, new ResourceAmount(green: 115)),

            new Level(3.5f, new ResourceAmount(green: 120)),
            new Level(3.6f, new ResourceAmount(green: 125)),
            new Level(3.7f, new ResourceAmount(green: 130)),
            new Level(3.8f, new ResourceAmount(green: 135)),
            new Level(3.9f, new ResourceAmount(green: 140)),


            new Level(4.0f, new ResourceAmount(green: 145)),
            new Level(4.2f, new ResourceAmount(green: 150)),
            new Level(4.4f, new ResourceAmount(green: 155)),
            new Level(4.6f, new ResourceAmount(green: 160)),
            new Level(4.8f, new ResourceAmount(green: 165)),

            new Level(5.0f, new ResourceAmount(green: 170)),
            new Level(5.25f, new ResourceAmount(green: 175)),
            new Level(5.5f, new ResourceAmount(green: 180)),
            new Level(5.75f, new ResourceAmount(green: 185)),
            new Level(6.0f, new ResourceAmount(green: 190)),
        };

        static Level[] _damageMult = {
            new Level(1, new ResourceAmount()),
            new Level(1.05f, new ResourceAmount(green: 50)),
            new Level(1.10f, new ResourceAmount(green: 55)),
            new Level(1.15f, new ResourceAmount(green: 60)),
            new Level(1.20f, new ResourceAmount(green: 65)),

            new Level(1.25f, new ResourceAmount(green: 70)),
            new Level(1.30f, new ResourceAmount(green: 75)),
            new Level(1.35f, new ResourceAmount(green: 80)),
            new Level(1.40f, new ResourceAmount(green: 85)),
            new Level(1.45f, new ResourceAmount(green: 90)),


            new Level(1.50f, new ResourceAmount(green: 95)),
            new Level(1.55f, new ResourceAmount(green: 100)),
            new Level(1.60f, new ResourceAmount(green: 105)),
            new Level(1.65f, new ResourceAmount(green: 110)),
            new Level(1.70f, new ResourceAmount(green: 115)),

            new Level(1.75f, new ResourceAmount(green: 120)),
            new Level(1.80f, new ResourceAmount(green: 125)),
            new Level(1.85f, new ResourceAmount(green: 130)),
            new Level(1.90f, new ResourceAmount(green: 135)),
            new Level(1.95f, new ResourceAmount(green: 140)),


            new Level(2.0f, new ResourceAmount(green: 145)),
            new Level(2.1f, new ResourceAmount(green: 150)),
            new Level(2.2f, new ResourceAmount(green: 155)),
            new Level(2.3f, new ResourceAmount(green: 160)),
            new Level(2.4f, new ResourceAmount(green: 165)),

            new Level(2.5f, new ResourceAmount(green: 170)),
            new Level(2.6f, new ResourceAmount(green: 175)),
            new Level(2.7f, new ResourceAmount(green: 180)),
            new Level(2.8f, new ResourceAmount(green: 185)),
            new Level(3.0f, new ResourceAmount(green: 190)),
        };

        static Level[] _incomeMult = {
            new Level(1, new ResourceAmount()),
            new Level(1.05f, new ResourceAmount(green: 50)),
            new Level(1.10f, new ResourceAmount(green: 55)),
            new Level(1.15f, new ResourceAmount(green: 60)),
            new Level(1.20f, new ResourceAmount(green: 65)),

            new Level(1.25f, new ResourceAmount(green: 70)),
            new Level(1.30f, new ResourceAmount(green: 75)),
            new Level(1.35f, new ResourceAmount(green: 80)),
            new Level(1.40f, new ResourceAmount(green: 85)),
            new Level(1.45f, new ResourceAmount(green: 90)),


            new Level(1.50f, new ResourceAmount(green: 95)),
            new Level(1.55f, new ResourceAmount(green: 100)),
            new Level(1.60f, new ResourceAmount(green: 105)),
            new Level(1.65f, new ResourceAmount(green: 110)),
            new Level(1.70f, new ResourceAmount(green: 115)),

            new Level(1.75f, new ResourceAmount(green: 120)),
            new Level(1.80f, new ResourceAmount(green: 125)),
            new Level(1.85f, new ResourceAmount(green: 130)),
            new Level(1.90f, new ResourceAmount(green: 135)),
            new Level(1.95f, new ResourceAmount(green: 140)),


            new Level(2.0f, new ResourceAmount(green: 145)),
            new Level(2.1f, new ResourceAmount(green: 150)),
            new Level(2.2f, new ResourceAmount(green: 155)),
            new Level(2.3f, new ResourceAmount(green: 160)),
            new Level(2.4f, new ResourceAmount(green: 165)),

            new Level(2.5f, new ResourceAmount(green: 170)),
            new Level(2.6f, new ResourceAmount(green: 175)),
            new Level(2.7f, new ResourceAmount(green: 180)),
            new Level(2.8f, new ResourceAmount(green: 185)),
            new Level(3.0f, new ResourceAmount(green: 190)),
        };


        public int currentLevel { get; private set; }

        Level[] data;
        Modification mod1 = new Modification();
        Modification mod2 = new Modification();


        BasicCreepStat() { }


        public ResourceAmount GetCostForNextLevel() {
            if (currentLevel >= data.Length) {
                return null;
            }

            return data[currentLevel].cost;
        }
        public float GetValueForCurrentLevel() {
            var result =  data[currentLevel - 1].value;
            result *= mod1.GetRatio() * mod2.GetRatio();
            return result;
        }
        public void LevelUp() {
            currentLevel++;
        }
        public float GetRatioToBase() {
            return GetValueForCurrentLevel() / data[0].value;
        }



        public static BasicCreepStat NewHp() {
            var result = new BasicCreepStat();
            result.currentLevel = 1;
            result.data = _hp;
            return result;
        }

        public static BasicCreepStat NewSpd() {
            var result = new BasicCreepStat();
            result.currentLevel = 1;
            result.data = _spd;
            return result;
        }

        public static BasicCreepStat NewCount() {
            var result = new BasicCreepStat();
            result.currentLevel = 1;
            result.data = _count;
            return result;
        }

        public static BasicCreepStat NewSpawnRate() {
            var result = new BasicCreepStat();
            result.currentLevel = 1;
            result.data = _spawnRate;
            return result;
        }

        public static BasicCreepStat NewDamageMult() {
            var result = new BasicCreepStat();
            result.currentLevel = 1;
            result.data = _damageMult;
            return result;
        }

        public static BasicCreepStat NewIncomeMult() {
            var result = new BasicCreepStat();
            result.currentLevel = 1;
            result.data = _incomeMult;
            return result;
        }


        struct Level {
            public float value;
            public ResourceAmount cost;

            public Level(float value, ResourceAmount cost) {
                this.value = value;
                this.cost = cost;
            }
        }

        class Modification {
            float numer = 1;
            float denom = 1;

            public void Reset() {
                numer = 1;
                denom = 1;
            }
            public void AddScale(float val) {
                if (val > 0) {
                    numer += val;
                }
                else {
                    denom -= val;
                }
            }

            public float GetRatio() {
                return numer / denom;
            }
        }
    }


    public class CreepSquad {
        public CreepDefinition actualDefinition { get; private set; }

        public CreepSquad parentSquad { get; private set; }
        CreepSquad deathSplitSquad;
        CreepSquad carrierSquad;

        public bool isChild => isDeathSpawn || isCarrierSpawn;
        public bool isDeathSpawn { get; private set; }
        public bool isCarrierSpawn { get; private set; }


        bool deathSplitActive;
        bool carrierActive;

        public SquadRoundStatistics roundStatistics = new SquadRoundStatistics();

        //**********************************************************************************************
        // Stats
        //**********************************************************************************************

        public CreepStatSet stats = new CreepStatSet();
        
        //**********************************************************************************************
        // Collections
        //**********************************************************************************************

        public CreepLoadout loadout = new CreepLoadout();

        //**********************************************************************************************
        // Upgrades
        //**********************************************************************************************


        public CreepSquad(CreepDefinition def) {
            actualDefinition = def.CreateCopy();
            Recalculate();
        }

       
        public void Recalculate() {
            actualDefinition.resourceReward[ResourceType.green] = 100;
            actualDefinition.hp = stats.hp.GetValueForCurrentLevel();
            actualDefinition.speed = stats.spd.GetValueForCurrentLevel();
            actualDefinition.count = stats.count.GetValueForCurrentLevel();
            actualDefinition.spawnRate = stats.spawnRate.GetValueForCurrentLevel();
            actualDefinition.damageScale = stats.damageMult.GetValueForCurrentLevel();
            actualDefinition.incomeScale = stats.incomeMult.GetValueForCurrentLevel();
        }

        public CreepSquad GetDeathSplitSquad() {
            return deathSplitActive ? deathSplitSquad : null;
        }

        public CreepSquad GetCarrierSquad() {
            return carrierActive ? carrierSquad : null;
        }
    }
}
