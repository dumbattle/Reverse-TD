using System.Collections.Generic;
using System;
using System.Collections;

namespace LPE {
    public class ObjectPool<T> where T : class {
        Dictionary<T, Item> returnDict = new Dictionary<T, Item>();
        Func<T> _constructor;
        LinkedList<Item> freeItems = new LinkedList<Item>();
        int warningCount;
     

        public ObjectPool(Func<T> objCreater, int warningCount = 1000) {
            _constructor = objCreater;
            this.warningCount = warningCount;
        }

        public T Get() {
            if (freeItems.First != null) {
                var n = freeItems.First;

                freeItems.RemoveFirst();

                return n.Value.obj;
            }

            var newItem = CreateItem();

            return Get();
        }

        public void Return(T t) {
            var n = returnDict[t].node;
            freeItems.AddLast(n);
        }

        Item CreateItem() {
            T t = _constructor();
            Item i = new Item(t);

            returnDict.Add(t, i);
            freeItems.AddLast(i.node);

            if (returnDict.Count % warningCount == 0) {
                UnityEngine.Debug.LogWarning($"ObjectPool<{typeof(T).Name}> capacity reached {returnDict.Count}");
            }
            return i;
        }


        class Item {
            public T obj;
            public LinkedListNode<Item> node;

            public Item(T t) {
                obj = t;
                node = new LinkedListNode<Item>(this);
            }
        }
    }
}