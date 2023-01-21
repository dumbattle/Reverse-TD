using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;



namespace Core {
    public class PreRound_CreepMenu_Details_StatEntry_Behaviour : MonoBehaviour {
        public Sprite barFilled;
        public Sprite barEmpty;


        public TextMeshProUGUI valueText;
        public List<Image> bars;


        public void SetBars(float actualValue, float baseValue) {
            var ratio = actualValue / baseValue;
            ratio /= 4f;


            for (int i = 0; i < bars.Count; i++) {
                bars[i].sprite = ratio > (float)i / bars.Count ? barFilled : barEmpty;
            }
        }
    }
}
