using UnityEngine;
using System;
namespace Core {
    public class BasicSpriteAnimationBehaviour : MonoBehaviour {
        public Sprite[] sprites;
        public int[] durations;
        public SpriteRenderer sr;
        public bool randomizeStartFrame;

        int currentFrame;
        int timer;


        void Awake() {
            Init();
            if (durations.Length != sprites.Length) {
                throw new InvalidOperationException("sprites[] and durations[] do not have same length");
            }
        }

        void Update() {
            timer -= 1;

            if (timer <= 0) {
                currentFrame++;
                if (currentFrame >= sprites.Length) {
                    currentFrame = 0;
                }
                sr.sprite = sprites[currentFrame];
                timer += durations[currentFrame];
            }
        }


        public void Init() {
            currentFrame = randomizeStartFrame ? UnityEngine.Random.Range(0, sprites.Length) : 0;
            timer = durations[currentFrame];
            gameObject.SetActive(true);
            sr.sprite = sprites[currentFrame];
        }
    }


}