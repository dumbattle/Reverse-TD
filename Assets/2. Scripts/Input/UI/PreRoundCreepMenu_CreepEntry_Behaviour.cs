using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LPE;


namespace Core {
    public class PreRoundCreepMenu_CreepEntry_Behaviour : MonoBehaviour {
        public Transform iconRoot;
        public Image icon;
        public Image glowIcon;
        public Image childIndicator;
        public LPEButtonBehaviour button;

        //[Space]
        //public Transform hpBarPivot;
        //public Transform spdBarPivot;
        //public Transform countBarPivot;

        public CreepSquad squad;
    }

}
