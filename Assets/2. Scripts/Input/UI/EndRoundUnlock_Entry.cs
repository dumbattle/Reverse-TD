using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Core {
    public class EndRoundUnlock_Entry : MonoBehaviour {
        public Image icon;
        public TextMeshProUGUI nameText;

    
        public void SetScale(float t01) {
            gameObject.SetActive(true);
            transform.localScale = new Vector3(t01, t01, 1);
        }
    }
}
