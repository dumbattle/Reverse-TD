using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Core {
    public class ResourceDisplayBehaviour : MonoBehaviour {
        [SerializeField] RectTransform layoutRect;

        [Space]
        [SerializeField] TextMeshProUGUI gText;
        [SerializeField] TextMeshProUGUI rText;
        [SerializeField] TextMeshProUGUI bText;
        [SerializeField] TextMeshProUGUI yText;
        [SerializeField] TextMeshProUGUI dText;

        [Space]
        [SerializeField] GameObject gRoot;
        [SerializeField] GameObject rRoot;
        [SerializeField] GameObject bRoot;
        [SerializeField] GameObject yRoot;
        [SerializeField] GameObject dRoot;

        public void Display(ScenarioInstance s, ResourceAmount resources) {
            var playerResources = s.playerFunctions.GetCurrentResources();
            gRoot.gameObject.SetActive(false);
            rRoot.gameObject.SetActive(false);
            bRoot.gameObject.SetActive(false);
            yRoot.gameObject.SetActive(false);
            dRoot.gameObject.SetActive(false);
            var g = resources[ResourceType.green];
            var r = resources[ResourceType.red];
            var b = resources[ResourceType.blue];
            var y = resources[ResourceType.yellow];
            var d = resources[ResourceType.diamond];

            if (g > 0) {
                gRoot.gameObject.SetActive(true);
                gText.text = g.ToString();
                gText.color = playerResources[ResourceType.green] >= g ? Color.white : Color.red;
            }

            if (r > 0) {
                rRoot.gameObject.SetActive(true);
                rText.text = r.ToString();
                rText.color = playerResources[ResourceType.red] >= r ? Color.white : Color.red;
            }

            if (b > 0) {
                bRoot.gameObject.SetActive(true);
                bText.text = b.ToString();
                bText.color = playerResources[ResourceType.blue] >= b ? Color.white : Color.red;
            }

            if (y > 0) {
                yRoot.gameObject.SetActive(true);
                yText.text = y.ToString();
                yText.color = playerResources[ResourceType.yellow] >= y ? Color.white : Color.red;
            }

            if (d > 0) {
                dRoot.gameObject.SetActive(true);
                dText.text = d.ToString();
                dText.color = playerResources[ResourceType.diamond] >= d ? Color.white : Color.red;
            }
        }
    }
}
