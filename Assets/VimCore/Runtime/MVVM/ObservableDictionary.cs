using System;
using System.Collections;
using System.Collections.Generic;

namespace VimCore.Runtime.MVVM
{
    public class ObservableDictionary<TKey, TValue>: IDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _data = new();
        private Action<Dictionary<TKey, TValue>> _onData;

        public event Action<Dictionary<TKey, TValue>> OnData
        {
            add
            {
                _onData -= value;
                _onData += value;
                value?.Invoke(_data);
            }
            remove => _onData -= value;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (_data.TryAdd(item.Key, item.Value)) 
                _onData?.Invoke(_data);
        }

        public void Clear()
        {
            _data.Clear();
            _onData?.Invoke(_data);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (_data.TryGetValue(item.Key, out var value))
                return item.Value.Equals(value);
            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (!_data.Remove(item.Key)) 
                return false;
            _onData?.Invoke(_data);
            return true;
        }

        public int Count => _data.Count;
        
        public bool IsReadOnly => false;
        
        public void Add(TKey key, TValue value)
        {
            if (_data.TryAdd(key, value)) 
                _onData?.Invoke(_data);
        }

        public bool ContainsKey(TKey key) => _data.ContainsKey(key);

        public bool Remove(TKey key)
        {
            if (!_data.Remove(key)) 
                return false;
            _onData?.Invoke(_data);
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value) => _data.TryGetValue(key, out value);

        public TValue this[TKey key]
        {
            get
            {
                if (_data.TryGetValue(key, out var result))
                    return result;
                return default;
            }
            set
            {
                _data[key] = value;
                _onData?.Invoke(_data);
            }
        }

        public ICollection<TKey> Keys => _data.Keys;
        public ICollection<TValue> Values => _data.Values;
    }
}