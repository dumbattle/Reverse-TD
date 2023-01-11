using UnityEngine;


namespace Core {
    public interface IPlayerItem {
        string GetName();
        string GetDescription();
        Sprite GetIcon();
    }

}
