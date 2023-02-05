using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LPE;


namespace Core {
    public class ScenarioUI_CreepMenu_CreepEntry_Behaviour : MonoBehaviour {
        public LPEButtonBehaviour button;
        [Space]
        public Transform iconRoot;
        public Image icon;
        public Image glowIcon;
        public TextMeshProUGUI countText;

        [Space]
        public Image deathChild;
        public Image deathGlow;
        public Image carrierChild;
        public Image carrierGlow;


        [Space]
        public Image frameImage;
        public Sprite unselectedSprite;
        public Sprite selectedSprite;

        public CreepSquad squad;
    }

}
