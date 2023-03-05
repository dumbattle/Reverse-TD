using UnityEngine;
using UnityEngine.UI;
using LPE;



namespace Core {
    [System.Serializable]
    public struct CreepMenu_SubmenuButtons {
        [SerializeField] Sprite unselectedSprite;
        [SerializeField] Sprite selectedSprite;

            
        [Space]
        [SerializeField] Entry stats;
        [SerializeField] Entry attachment;
        [SerializeField] Entry overview;

        //************************************************************************************
        // Query
        //************************************************************************************

        public bool StatsClicked() {
            return stats.button.Clicked;
        }
        public bool AttachmentClicked() {
            return attachment.button.Clicked;
        }
        public bool SummaryClicked() {
            return overview.button.Clicked;
        }


        [System.Serializable]
        struct Entry { 
            public LPEButtonBehaviour button;
            public Image frame;
        }
    }
}
