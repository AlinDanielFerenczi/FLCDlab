using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace labproject
{
    public struct KeyValue<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
    }

    public class SymbolTable
    {
        private readonly int size;
        private readonly LinkedList<KeyValue<int, string>>[] items;

        public SymbolTable(int size)
        {
            this.size = size;
            items = new LinkedList<KeyValue<int, string>>[size];
        }

        protected int GetArrayPosition(int key)
        {
            int position = key % size;
            return Math.Abs(position);
        }

        public bool Exists(string elem)
        {
            var key = GetHashCode(elem);
            LinkedList<KeyValue<int, string>> linkedList = GetLinkedList(GetArrayPosition(key));

            foreach (KeyValue<int, string> entry in linkedList)
            {
                if (entry.Value.Equals(elem))
                {
                    return true;
                }
            }

            return false;
        }

        private int GetHashCode(string elem)
        {
            return Encoding.ASCII.GetBytes(elem).Aggregate(0, (sum, val) => sum + val) % size;
        }

        public int Add(string elem)
        {
            int key = GetHashCode(elem);
            LinkedList<KeyValue<int, string>> linkedList = GetLinkedList(GetArrayPosition(key));
            KeyValue<int, string> item = new KeyValue<int, string>() { Key = key, Value = elem };
            foreach (KeyValue<int, string> entry in linkedList)
            {
                if (entry.Value.Equals(elem))
                {
                    return key;
                }
            }
            linkedList.AddLast(item);
            return key;
        }

        public string Remove(string elem)
        {
            var key = GetHashCode(elem);
            LinkedList<KeyValue<int, string>> linkedList = GetLinkedList(GetArrayPosition(key));
            bool itemFound = false;
            KeyValue<int, string> foundItem = default;

            foreach (KeyValue<int, string> item in linkedList)
            {
                if (item.Key.Equals(key))
                {
                    itemFound = true;
                    foundItem = item;
                    break;
                }
            }

            if (itemFound)
                linkedList.Remove(foundItem);

            return foundItem.Value;
        }

        protected LinkedList<KeyValue<int, string>> GetLinkedList(int position)
        {
            LinkedList<KeyValue<int, string>> linkedList = items[position];

            if (linkedList == null)
            {
                linkedList = new LinkedList<KeyValue<int, string>>();
                items[position] = linkedList;
            }

            return linkedList;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(items);
        }
    }
}
