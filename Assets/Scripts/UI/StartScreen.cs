using System;
using Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class StartScreen : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        
        [Inject]
        public void Initialize(LevelManager levelManager)
        {
            _startButton.onClick.RemoveAllListeners();
            _startButton.onClick.AddListener(levelManager.PlayStartScenario);
        }

        public void OnEnable()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.5f);
        }
    }
}
