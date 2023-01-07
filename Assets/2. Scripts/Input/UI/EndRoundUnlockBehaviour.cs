using UnityEngine;
using System.Collections.Generic;
using LPE;

namespace Core {
    public class EndRoundUnlockBehaviour : MonoBehaviour {
        /*
         * Relies on 'HorizontalLayoutGroup' and 'ContentSizeFitter' to position entries; 
         */
        const float OVERSHOOT_SCALE = 0.2f;


        [SerializeField] EndRoundUnlock_Entry entrySrc;
        public LPEButtonBehaviour continueButton;

        [Header("Parameteres")]
        [SerializeField] int numFramesForEntry;
        [SerializeField] int numFramesOvershootCorrection;
        [SerializeField] int entryFramDelta;


        int activeCount = 0;
        List<EndRoundUnlock_Entry> _entries = new List<EndRoundUnlock_Entry>();
        AnimType animType = AnimType.none;
        int animationFrame;

        void Awake() {
            entrySrc.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(false);
        }

        //****************************************************************************************************
        // Update
        //****************************************************************************************************

        void Update() {
            switch (animType) {
                case AnimType.none:
                    break;
                case AnimType.unlock:
                    UnlockAnim();
                    break;
                case AnimType.close:
                    Close();
                    break;
                case AnimType.hide:
                    break;
                case AnimType.unhide:
                    break;
            }
            animationFrame++;
        }

        //----------------------------------------------------------------------------------------------------
        // Helpers
        //----------------------------------------------------------------------------------------------------

        void UnlockAnim() {
            bool done = true;
            for (int i = 0; i < activeCount; i++) {
                int startFrame = i * entryFramDelta;
                float dif = animationFrame - startFrame;

                // grow
                if (dif <= numFramesForEntry) {
                    float t = Mathf.Clamp01(dif / numFramesForEntry);
                    t = t - 1;
                    t = t * t;
                    t = 1 - t;
                    _entries[i].SetScale(t * (1 + OVERSHOOT_SCALE));
                }
                // overshoot correction
                else {
                    float t = Mathf.Clamp01((dif - numFramesForEntry) / numFramesOvershootCorrection);
                    t = (1 - t * t) * OVERSHOOT_SCALE;
                    _entries[i].SetScale(1 + t);
                }
                if (dif < numFramesForEntry + numFramesOvershootCorrection) {
                    done = false;
                }
            }

            if (done) {
                animType = AnimType.none;
                continueButton.gameObject.SetActive(true);
                continueButton.transform.localScale = new Vector3(1, 1, 1);
            }
        }


        void Close() {
            float t;
            if (animationFrame <= numFramesOvershootCorrection) {
                t = Mathf.Clamp01((float)animationFrame / numFramesOvershootCorrection);
                t = t - 1;
                t = t * t;
                t = 1 - t;
                t = 1 + t * OVERSHOOT_SCALE;
            }
            else {
                t = Mathf.Clamp01((float)(animationFrame - numFramesOvershootCorrection) / numFramesOvershootCorrection);
                t = (1 - t * t);
                t = t * (1 + OVERSHOOT_SCALE);
            }

            for (int i = 0; i < activeCount; i++) {
                _entries[i].SetScale(t);
            }
            continueButton.transform.localScale = new Vector3(t, t, 1);


            // done
            if (animationFrame > numFramesOvershootCorrection + numFramesForEntry) {
                animType = AnimType.none;

                for (int i = 0; i < activeCount; i++) {
                    _entries[i].gameObject.SetActive(false);
                }
                continueButton.gameObject.SetActive(false);
                activeCount = 0;
            }
        }


        //****************************************************************************************************
        // Control
        //****************************************************************************************************

        public void AddEntry(Sprite sprite, string name) {
            if (_entries.Count <= activeCount) {
                var e = Instantiate(entrySrc, entrySrc.transform.parent);
                _entries.Add(e);
                e.gameObject.SetActive(false);
            }

            var entry = _entries[activeCount];
            entry.icon.sprite = sprite;
            entry.nameText.text = name;
            activeCount++;
        }
  

        public void StartUnlockAnimation() {
            animType = AnimType.unlock;
            animationFrame = 0;
        }

        public void StartCloseAnimation() {
            animType = AnimType.close;
            animationFrame = 0;
        }

        public bool AnimationDone() {
            return animType == AnimType.none;
        }



        enum AnimType {
            none,
            unlock,
            close,
            hide,
            unhide
        }
    }
}
