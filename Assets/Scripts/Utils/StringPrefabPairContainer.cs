using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    [CreateAssetMenu(fileName = "StringPrefabPairContainer", menuName = "TowerColor/Configs/StringPrefabPairContainer")]
    public class StringPrefabPairContainer : ScriptableObject
    {
        [SerializeField] private StringPrefabPair[] _prefabs;

        Dictionary<string, GameObject> _cache = new Dictionary<string, GameObject>();
        public GameObject Get(string name)
        {
            if (_cache.TryGetValue(name, out GameObject prefab))
                return prefab;
            _cache.Add(name, _prefabs.FirstOrDefault(x => x.Name.Equals(name))?.Prefab);
            return _cache[name];
        }
    }

    [Serializable]
    public class StringPrefabPair
    {
        [SerializeField] private string _name;

        [SerializeField] private GameObject _prefab;

        public string Name => _name;

        public GameObject Prefab => _prefab;

        public StringPrefabPair(string name, GameObject prefab)
        {
            _name = name;
            _prefab = prefab;
        }
    }
}