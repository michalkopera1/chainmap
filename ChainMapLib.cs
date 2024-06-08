using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ChainMapLib
{
    public class ChainMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly List<IDictionary<TKey, TValue>> _maps;
        private readonly Dictionary<TKey, TValue> _mainMap;

        public ChainMap(params IDictionary<TKey, TValue>[] maps)
        {
            _mainMap = new Dictionary<TKey, TValue>();
            _maps = new List<IDictionary<TKey, TValue>> { _mainMap };
            _maps.AddRange(maps);
        }

        public TValue this[TKey key]
        {
            get
            {
                foreach (var map in _maps)
                {
                    if (map.TryGetValue(key, out var value))
                    {
                        return value;
                    }
                }
                throw new KeyNotFoundException();
            }
            set => _mainMap[key] = value;
        }

        public ICollection<TKey> Keys => _maps.SelectMany(m => m.Keys).Distinct().ToList();

        public ICollection<TValue> Values => _maps.SelectMany(m => m.Values).Distinct().ToList();

        public int Count => Keys.Count;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            if (_mainMap.ContainsKey(key))
            {
                throw new ArgumentException("Key already exists in the main map.");
            }
            _mainMap.Add(key, value);
        }

        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        public bool TryAdd(TKey key, TValue value)
        {
            if (_mainMap.ContainsKey(key))
            {
                return false;
            }
            _mainMap.Add(key, value);
            return true;
        }

        public void Clear() => _mainMap.Clear();

        public bool Contains(KeyValuePair<TKey, TValue> item) => ContainsKey(item.Key) && this[item.Key].Equals(item.Value);

        public bool ContainsKey(TKey key) => _maps.Any(map => map.ContainsKey(key));

        public bool Remove(TKey key) => _mainMap.Remove(key);

        public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);

        public bool TryGetValue(TKey key, out TValue value)
        {
            foreach (var map in _maps)
            {
                if (map.TryGetValue(key, out value))
                {
                    return true;
                }
            }
            value = default;
            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _maps.SelectMany(map => map).Distinct().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => throw new NotImplementedException();

        public void AddDictionary(IDictionary<TKey, TValue> dictionary, int index)
        {
            if (index < 0 || index >= _maps.Count)
            {
                _maps.Add(dictionary);
            }
            else
            {
                _maps.Insert(index, dictionary);
            }
        }

        public void RemoveDictionary(int index)
        {
            if (index > 0 && index < _maps.Count)
            {
                _maps.RemoveAt(index);
            }
        }

        public void ClearDictionaries()
        {
            _maps.Clear();
            _maps.Add(_mainMap);
        }

        public int CountDictionaries => _maps.Count;

        public IList<IDictionary<TKey, TValue>> GetDictionaries() => _maps.AsReadOnly();

        public IDictionary<TKey, TValue> GetDictionary(int index)
        {
            if (index < 0 || index >= _maps.Count)
            {
                throw new IndexOutOfRangeException();
            }
            return _maps[index];
        }
        public IDictionary<TKey, TValue> GetMainDictionary() => _mainMap;

        public IDictionary<TKey, TValue> Merge()
        {
            var merged = new Dictionary<TKey, TValue>();
            foreach (var map in _maps.AsEnumerable().Reverse())
            {
                foreach (var kvp in map)
                {
                    if (!merged.ContainsKey(kvp.Key))
                    {
                        merged[kvp.Key] = kvp.Value;
                    }
                }
            }
            return merged;
        }
    }
}
