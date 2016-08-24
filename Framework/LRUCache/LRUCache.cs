using System;
using System.Collections.Generic;
using System.Linq;
using Framewrok.LRUCache;

namespace Framework.LRUCache
{
    public class LRUCache<K,V>
    {
        private readonly int _maxCapacity = 0;
        private readonly Dictionary<K, Node<K, V>> _LRUCache;
        private Node<K, V> _head = null;
        private Node<K, V> _tail = null;
        public LRUCache(int argMaxCapacity)
        {
            _maxCapacity = argMaxCapacity;
            _LRUCache = new Dictionary<K, Node<K,V>>();
        }

        public void Add(K key, V value)
        {
            if (_LRUCache.ContainsKey(key))
            {
                MakeMostRecentlyUsed(_LRUCache[key]);
            }

            if (_LRUCache.Count >= _maxCapacity) RemoveLeastRecentlyUsed();

            Node<K, V> insertedNode = new Node<K, V>(key, value);

            if (_head == null)
            {
                _head = insertedNode;
                _tail = _head;
            }
            else MakeMostRecentlyUsed(insertedNode);

            _LRUCache.Add(key, insertedNode);
        }

        public Node<K, V> Get(K key)
        {
            if (!_LRUCache.ContainsKey(key)) return null;

            MakeMostRecentlyUsed(_LRUCache[key]);

            return _LRUCache[key];
        }


        public int Size()
        {
            return _LRUCache.Count();
        }

        public override string ToString()
        {
            var headReference = _head;

            List<string> items = new List<string>();

            while (headReference != null)
            {
                items.Add(String.Format("[V: {0}]", headReference.Data));
                headReference = headReference.Next;
            }

            return String.Join(",", items);
        }

        /// <summary>
        /// 除去最近最少使用
        /// </summary>
        private void RemoveLeastRecentlyUsed()
        {
            _LRUCache.Remove(_tail.Key);
            _tail.Previous.Next = null;
            _tail = _tail.Previous;
        }


        private void MakeMostRecentlyUsed(Node<K, V> foundItem)
        {
            // Newly inserted item bring to the top
            if (foundItem.Next == null && foundItem.Previous == null)
            {
                foundItem.Next = _head;
                _head.Previous = foundItem;
                if (_head.Next == null) _tail = _head;
                _head = foundItem;
            }
            // If it is the tail than bring it to the top
            else if (foundItem.Next == null && foundItem.Previous != null)
            {
                foundItem.Previous.Next = null;
                _tail = foundItem.Previous;
                foundItem.Next = _head;
                _head.Previous = foundItem;
                _head = foundItem;
            }
            // If it is an element in between than bring it to the top
            else if (foundItem.Next != null && foundItem.Previous != null)
            {
                foundItem.Previous.Next = foundItem.Next;
                foundItem.Next.Previous = foundItem.Previous;
                foundItem.Next = _head;
                _head.Previous = foundItem;
                _head = foundItem;
            }
            // Last case would be to check if it is a head but if it is than there is no need to bring it to the top
        }
    }
}
