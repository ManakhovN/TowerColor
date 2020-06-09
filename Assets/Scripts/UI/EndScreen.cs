using Core;
using DG.Tweening;
using ScriptableObjects.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private Button _nextButton;
        private GameManager _gameManager;

        [Inject]
        public void Initialize(LevelManager levelManager, GameManager gameManager)
        {
            _nextButton.onClick.RemoveAllListeners();
            _nextButton.onClick.AddListener(LoadNextLevel);
            _gameManager = gameManager;
        }

        private void LoadNextLevel()
        {
            _gameManager.LoadNextLevel();
        }

        public void OnEnable()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.5f);
        }
    }
}
