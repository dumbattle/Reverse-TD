//using UnityEngine;


//namespace Core {
//    public class CreepAttachment_DeathSplit : CreepAttatchment {
//        public override void ApplyModification(CreepStatModification results) {
//            results.deathSpawnLevel++;

//        }

//        public override Sprite GetIcon() {
//            return CreepItemIconResourceCache.shiftCount2Hp;
//        }

//        public override string GetName() {
//            return "Split Module";
//        }

//        public override string GetDescription() {
//            return "Creep will split inot smaller creeps when destroyed";
//        }

//        public override CreepModificationLevel GetLevel() {
//            return CreepModificationLevel.L1;
//        }

//        public override bool Attachable(CreepSquad s) {
//            return !s.isChild;
//        }
//    }
//}
