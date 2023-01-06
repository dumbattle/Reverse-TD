using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace LPE {
    public class LPEButtonBehaviour : MonoBehaviour, IPointerDownHandler, IPointerClickHandler {
        event Action OnDown;
        event Action OnClick;



        void IPointerDownHandler.OnPointerDown(PointerEventData ped) {
            OnDown?.Invoke();
        }
        void IPointerClickHandler.OnPointerClick(PointerEventData ped) {
            OnClick?.Invoke();
        }

        
        
        public void SetClickListener(Action cb) {
            OnClick += cb;
        }
        public void SetDownListener(Action cb) {
            OnDown += cb;
        }
    }
}