using System;
using System.Collections;
using System.Collections.Generic;

namespace VimCore.Runtime.DependencyManagement
{
    public class Filter<T>: ICollection<T>, IEnumerator<T>
    {
        private readonly HashSet<T> _data = new();
        private readonly List<T> _list =  new();

        public int Count => _data.Count;
        public bool IsReadOnly => false;
        
        public bool Remove(T item)
        {
            if (!_data.Remove(item)) return false;
            _list.Remove(item);
            return true;
        }
        

        public void Add(T item)
        {
            if (!_data.Add(item)) return;
            _list.Add(item);
        }

        public void Clear()
        {
            _data.Clear();
            _list.Clear();
        }

        public bool Contains(T item) => _data.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => throw new NotImplementedException();

        public IEnumerator<T> GetEnumerator()
        {
            _enumerator = _list.Count;
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator() => this;
        
        private int _enumerator;
        bool IEnumerator.MoveNext() => 0 <= --_enumerator;
        void IEnumerator.Reset() => _enumerator = _list.Count;
        T IEnumerator<T>.Current => _list[_enumerator];
        object IEnumerator.Current => _list[_enumerator];
        void IDisposable.Dispose() { }
    }
}