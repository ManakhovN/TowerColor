using System;
using Core;
using UnityEngine;
using Zenject;

namespace UI
{
    public class ScreenManager : MonoBehaviour
    {
        [SerializeField] private GameObject _startScreen;
        [SerializeField] private GameObject _endScreen;
        [SerializeField] private GameObject _gameHUD;

        public GameObject StartScreen => _startScreen;

        public GameObject EndScreen => _endScreen;

        public GameObject GameHud => _gameHUD;

        [Inject]
        public void Initialize(LevelManager levelManager)
        {
            DisableAll();
            _startScreen.SetActive(true);
            levelManager.OnStateChange += StateChange;
        }

        private void StateChange(LevelState obj)
        {
            switch (obj)
            {
                case LevelState.Start:
                    _startScreen.SetActive(false);
                    break;
                case LevelState.Game:
                    _startScreen.SetActive(false);
                    _gameHUD.SetActive(true);
                    break;
                case LevelState.End: 
                    _gameHUD.SetActive(false);
                    _endScreen.SetActive(true);
                    break;
            }
        }

        public void DisableAll()
        {
            _startScreen.SetActive(false);
            _gameHUD.SetActive(false);
            _endScreen.SetActive(false);
        }
    }
}