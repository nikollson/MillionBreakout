
using System.Collections;
using System.Collections.Generic;

namespace Stool.CSharp
{
    class DictionarizedLinkedList<T> : IEnumerable<T>
    {
        private LinkedList<T> _list;
        private Dictionary<T, LinkedListNode<T>> _dictionary;

        public int Count { get { return _list.Count; } }

        public DictionarizedLinkedList()
        {
            _list = new LinkedList<T>();
            _dictionary = new Dictionary<T, LinkedListNode<T>>();
        }

        public void Add(T value)
        {
            var node = _list.AddLast(value);
            _dictionary.Add(value, node);
        }

        public void Remove(T value)
        {
            var node = _dictionary[value];
            _list.Remove(node);
            _dictionary.Remove(value);
        }

        public void Clear()
        {
            _list.Clear();
            _dictionary.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
