using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ScriptableObjects.Core
{
    public class GameManager
    {
        private int _currentLevel = 0;
        private ZenjectSceneLoader _loader;

        public int CurrentLevel => _currentLevel;

        public GameManager(ZenjectSceneLoader loader)
        {
            _loader = loader;
        }

        public void LoadNextLevel()
        {
            _currentLevel++;
            _loader.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
