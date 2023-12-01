using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Collections.ObjectModel
{
    public struct Pair<T>
    {
        public int id;
        public T item;

        public Pair(int id, T item)
        {
            this.id = id;
            this.item = item;
        }
    }

    public class ObservableCollection<T> : IEnumerable<Pair<T>>
    {
        public ObservableCollection()
        {
            items = new Dictionary<int, Pair<T>>();
        }

        public T this[int id]
        {
            get => items[id].item;
            set => Change(id, value);
        }

        public Dictionary<int, Pair<T>> items;

        public event Action<int, T> onItemAdded;
        public event Action<int, T> onItemRemoved;
        public event Action<int, T> onItemChanged;
        public event Action onCollectionChanged;

        public void Change(int id, T item)
        {
            if (items.TryGetValue(id, out Pair<T> pair))
            {
                items[id] = new Pair<T>(id, item);
                onItemChanged?.Invoke(id, item);
                onCollectionChanged?.Invoke();
            }

            else
            {
                throw new Exception($"[ObservableCollection] - Change");
            }
        }

        public bool Contains(int id)
        {
            return items.ContainsKey(id);
        }

        public void Add(int id, T item)
        {
            if (items.TryAdd(id, new Pair<T>(id, item)) == false)
            {
                throw new Exception($"[ObservableCollection] - Add");
            }

            onItemAdded?.Invoke(id, item);
            onCollectionChanged?.Invoke();
        }

        public void Remove(int id)
        {
            if (items.TryGetValue(id, out Pair<T> pair) == false)
            {
                throw new Exception($"[ObservableCollection] - Remove");
            }

            items.Remove(id);
            onItemRemoved?.Invoke(id, pair.item);
            onCollectionChanged?.Invoke();
        }

        public IEnumerator<Pair<T>> GetEnumerator()
        {
            return items.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.Values.GetEnumerator();
        }
    }
}
