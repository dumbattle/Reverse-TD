using UnityEngine;

namespace Core {
    public abstract class AssetReferenceDictioinary<T> : ScriptableObject{
        public T this[string s] => data[s];
        [SerializeField] Dict data;

        [System.Serializable]
        class Dict : SerializableDictionary<string, T> { }
    }
}
