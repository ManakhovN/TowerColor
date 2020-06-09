using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Field;
using Gameplay.Input;
using UnityEngine;
using Utils;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class FieldObjectsController : ITickable
    {
        public List<TowerElementMonoBehaviour> _objs = new List<TowerElementMonoBehaviour>();

        public List<List<int>> _byHeight = new List<List<int>>();

        public Dictionary<int, List<int>> _byColor = new Dictionary<int, List<int>>();

        private CameraController _cameraController;
        private FieldObjectsSpawner _fieldObjectsSpawner;
        private IInputController _inputController;
        private bool _isPaused = true;

        public event Action<float> OnProgressChanged = delegate { };

        public bool IsPaused
        {
            get => _isPaused;
            set => _isPaused = value;
        }

        public FieldObjectsController(FieldObjectsSpawner fieldObjectsSpawner, CameraController cameraController,
            IInputController inputController)
        {
            _fieldObjectsSpawner = fieldObjectsSpawner;
            _fieldObjectsSpawner.OnBallSpawned += RegisterBall;
            _fieldObjectsSpawner.SpawnAllBalls();
            _cameraController = cameraController;
            _inputController = inputController;
            _cameraController.CurrentHeight = _byHeight.Count;
        }

        public List<Vector3> ObjectHit(TowerElementMonoBehaviour obj)
        {
            var id = _objs.IndexOf(obj);
            if (id == -1)
                return null;

            var chainOfObjects = GetSimilarObjectsAround(id);
            List<Vector3> result = new List<Vector3>();
            foreach (int index in chainOfObjects)
            {
                _objs[index].gameObject.SetActive(false);
                result.Add(_objs[index].transform.position);
            }

            CheckProgress();

            return result;
        }

        private void RegisterBall(TowerElementMonoBehaviour towerElementMonoBehaviour)
        {
            _objs.Add(towerElementMonoBehaviour);
            var position = towerElementMonoBehaviour.FieldObject.Position;
            int height = Mathf.FloorToInt(position.y);
            if (height >= _byHeight.Count)
                for (int i = _byHeight.Count; i < height + 1; i++)
                    _byHeight.Add(new List<int>());

            var colorInt = towerElementMonoBehaviour.FieldObject.ColorInt;
            if (!_byColor.ContainsKey(colorInt))
                _byColor.Add(colorInt, new List<int>());

            _byColor[colorInt].Add(_objs.Count - 1);
            _byHeight[height].Add(_objs.Count - 1);
        }

        public void Tick()
        {
            if (_isPaused)
                return;

            _cameraController.MoveCameraToHeight(_byHeight.Count);
            for (int height = _byHeight.Count - 1; height >= 0; height--)
            {
                var currentHeight = _byHeight[height];
                if (height > _byHeight.Count - 8)
                {
                    ProcessActiveObjects(currentHeight, height);
                }
            }

            foreach (var outRanged in _objs.Where(x => x.IsInteractable && x.transform.position.y < -4))
            {
                outRanged.gameObject.SetActive(false);
                CheckProgress();
            }
        }

        public void DisableAllObjectsUnderHeight()
        {
            for (int height = _byHeight.Count - 1; height >= 0; height--)
            {
                var currentHeight = _byHeight[height];
                if (height <= _byHeight.Count - 8)
                {
                    for (int i = currentHeight.Count - 1; i >= 0; i--)
                    {
                        var obj = currentHeight[i];
                        var currentObj = _objs[obj];
                        currentObj.IsInteractable = false;
                    }
                }
            }
        }

        private void ProcessActiveObjects(List<int> currentHeight, int height)
        {
            int availableObjs = 0;
            for (int i = currentHeight.Count - 1; i >= 0; i--)
            {
                var obj = currentHeight[i];
                var currentObj = _objs[obj];
                if (!currentObj.IsInteractable)
                    currentObj.IsInteractable = true;

                if (height - currentObj.transform.position.y > 2 || currentObj.gameObject.activeSelf == false)
                {
                    currentHeight.RemoveAt(i);
                    CheckProgress();
                }
                else
                    availableObjs++;
            }

            if (availableObjs == 0)
            {
                _byHeight.RemoveAt(height);
                CheckProgress();
            }
        }

        public List<int> GetSimilarObjectsAround(int id)
        {
            var color = _objs[id].FieldObject.ColorInt;
            var allObjects = _byColor[color].Where(x => x != id && _objs[x].IsInteractable).ToList();
            List<int> result = new List<int>() {id};
            for (int i = 0; i < result.Count; i++)
            {
                var resObj = _objs[result[i]];
                var resTransform = resObj.transform;
                var smallR = resTransform.localScale.x * 1.5f;
                for (int j = allObjects.Count - 1; j >= 0; j--)
                {
                    int newId = allObjects[j];
                    var compObj = _objs[newId];


                    var compTransform = compObj.transform;
                    if ((resTransform.position - compTransform.position).sqrMagnitude < smallR * smallR)
                    {
                        allObjects.RemoveAt(j);
                        result.Add(newId);
                    }
                }
            }

            return result;
        }

        public int GetRandomColor()
        {
            var arr = _objs.Where(x => x.IsInteractable).ToArray();
            if (arr.Length > 0)
                return arr[Random.Range(0, arr.Length)].FieldObject.ColorInt;
            
            var colors = _byColor.Keys.ToList();
            return colors[Random.Range(0, colors.Count)];
        }

        public void CheckProgress()
        {
            int active = 0;
            for (int i = 0; i < _objs.Count; i++)
            {
                if (_objs[i].gameObject.activeSelf)
                    active++;
            }

            OnProgressChanged(1f - active / (float) _objs.Count);
        }
    }
}