using System;
using DG.Tweening;
using Gameplay;
using Gameplay.Ball;
using Gameplay.Field;
using Gameplay.Input;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Core
{
    public class LevelManager
    {
        private readonly FieldObjectsController _fieldObjectsController;
        private readonly CameraController _cameraController;
        private readonly StringPrefabPairContainer _prefabContainer;
        private LevelState _levelState = LevelState.Menu;
        private InputController _inputController;
        private Rigidbody _ball;
        private BallMonoBehaviour _ballMB;
        public event Action<float> OnProgressChanged = delegate(float f) { };
        public event Action<LevelState> OnStateChange = delegate(LevelState state) { };

        public LevelState LevelState
        {
            get => _levelState;
            set
            {
                _levelState = value;
                OnStateChange.Invoke(value);
            }
        }

        public LevelManager(FieldObjectsController fieldObjectsController, CameraController cameraController,
            InputController inputController, StringPrefabPairContainer prefabContainer)
        {
            _fieldObjectsController = fieldObjectsController;
            _cameraController = cameraController;
            _inputController = inputController;
            _inputController.OnObjectClick += ObjectClick;
            _inputController.OnDrag += OnDrag;
            _prefabContainer = prefabContainer;
            _fieldObjectsController.OnProgressChanged += OnProgressChangedInvoke;
        }

        private void OnDrag(Vector2 obj)
        {
            _cameraController.Rotate(obj);
        }

        private void OnProgressChangedInvoke(float obj)
        {
            OnProgressChanged.Invoke(obj);
            if (obj > 0.8f)
                LevelState = LevelState.End;
        }

        private void CreateTheBall()
        {
            _ball = Object.Instantiate(_prefabContainer.Get("Ball")).GetComponent<Rigidbody>();
            _ballMB = _ball.GetComponent<BallMonoBehaviour>();
            _ballMB.OnCollide += CheckCollision;
            SetBallPosition();
        }

        private void CheckCollision(TowerElementMonoBehaviour obj)
        {
            if (_ballMB.CurrentColor == obj.FieldObject.ColorInt && obj.IsInteractable)
            {
                var positions = _fieldObjectsController.ObjectHit(obj);
                _ballMB.gameObject.SetActive(false);
                float spawnProbability = Mathf.Clamp(positions.Count, 0, 20) / (float) positions.Count;
                foreach (var position in positions)
                {
                    if (Random.Range(0f, 1f) < spawnProbability)
                        GameObject.Instantiate(_prefabContainer.Get("Explosion"), position, Quaternion.identity, null);
                }
            }
        }

        private void ObjectClick(Vector3 contactPoint, Collider obj)
        {
            if (LevelState == LevelState.Game)
            {
                var towerMonoBeh = obj.GetComponent<TowerElementMonoBehaviour>();
                if (towerMonoBeh != null)
                {
                    _ballMB.transform.SetParent(null);
                    _ball.isKinematic = false;
                    _ball.velocity = (contactPoint - _ball.transform.position).normalized * 40f;
                    Sequence ballThrowSequence = DOTween.Sequence();
                    ballThrowSequence.AppendCallback(() => LevelState = LevelState.WaitForBall);
                    ballThrowSequence.AppendInterval(1f);
                    ballThrowSequence.AppendCallback(SetBallPosition);
                    ballThrowSequence.AppendCallback(() => LevelState = LevelState.Game);
                }
            }
        }

        private void SetBallPosition()
        {
            var camera = _cameraController.Camera;
            _ball.transform.position = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, 4f));
            _ballMB.transform.SetParent(_cameraController.transform);
            _ball.isKinematic = true;
            _ballMB.SetColor(_fieldObjectsController.GetRandomColor());
            _ball.gameObject.SetActive(true);
        }

        public void PlayStartScenario()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => LevelState = LevelState.Start);
            sequence.Append(_cameraController.Rotate());
            sequence.Join(_cameraController.Move());
            sequence.AppendCallback(() => _fieldObjectsController.DisableAllObjectsUnderHeight());
            sequence.AppendCallback(() => _fieldObjectsController.IsPaused = false);
            sequence.AppendCallback(() => LevelState = LevelState.Game);
            sequence.AppendCallback(CreateTheBall);
            sequence.Play();
        }
    }

    public enum LevelState
    {
        Menu,
        Start,
        Game,
        WaitForBall,
        End
    }
}