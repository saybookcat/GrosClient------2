using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framewrok.LRUCache
{
    public class Node<K,D>
    {
        public K Key { get; private set; }

        public D Data { get; private set; }

        public Node<K,D> Previous { get; set; }

        public Node<K,D> Next { get; set; }

        public Node(K key, D data)
        {
            this.Key = key;
            this.Data = data;
        }
    }
}
