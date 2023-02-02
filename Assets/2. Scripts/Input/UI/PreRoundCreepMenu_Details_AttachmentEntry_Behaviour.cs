using UnityEngine;
using UnityEngine.UI;


namespace Core {
    public class PreRoundCreepMenu_Details_AttachmentEntry_Behaviour : MonoBehaviour {
        public Sprite selectedSprite;
        public Sprite unselectedSprite;
        public Sprite emptySprite;

        public Image backround;
        public Image icon;
        public LPE.LPEButtonBehaviour button;


        public void SetSelected(bool value) {
            backround.sprite =
                value ? selectedSprite :
                icon.gameObject.activeSelf ? unselectedSprite :
                emptySprite;
        }
    }
}
