using System;
using Gameplay.Field;
using Gameplay.Levels;
using ScriptableObjects.Core;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class FieldObjectsSpawner
    {
        private LevelsContainer _levelsContainer;
        private TowerElementMonoBehaviour.Factory _towerElementsFactory;
        private StringPrefabPairContainer _prefabsContainer;
        private GameObject _rootGameObject;
        private GameManager _gameManager;

        public event Action<TowerElementMonoBehaviour> OnBallSpawned = delegate { };

        public FieldObjectsSpawner(LevelsContainer levelsContainer, TowerElementMonoBehaviour.Factory towerElementsFactory, StringPrefabPairContainer prefabsContainer, GameManager gameManager)
        {
            _levelsContainer = levelsContainer;
            _towerElementsFactory = towerElementsFactory;
            _prefabsContainer = prefabsContainer;
            _gameManager = gameManager;
            _rootGameObject = new GameObject("Root");
            _rootGameObject.transform.position = Vector3.zero;
        }

        public void SpawnAllBalls()
        {
            foreach (FieldObject obj in _levelsContainer.GetLevel(_gameManager.CurrentLevel).GetPositions())
            {
                Spawn(obj);
            }
        }

        public void Spawn(FieldObject obj)
        {
            var primitive = _towerElementsFactory.Create(_prefabsContainer.Get("Cyllinder"));
            primitive.FieldObject = obj;
            primitive.transform.position = obj.Position;
            primitive.transform.SetParent(_rootGameObject.transform);
            primitive.IsKinematic = true;
            primitive.IsColored = true;
//            primitive.transform.localScale = Vector3.one * _levelConfig.BallRadius;
            OnBallSpawned(primitive);
        }
    }
}