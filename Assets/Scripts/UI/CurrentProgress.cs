using Core;
using Src.UI.Scripts;
using UnityEngine;
using Zenject;

namespace UI
{
    public class CurrentProgress : MonoBehaviour
    {
        private LevelManager _levelManager;
        private ProgressBar _progressBar;

        [Inject]
        public void Initialize(LevelManager levelManager)
        {
            _progressBar = GetComponent<ProgressBar>();
            _levelManager = levelManager;
            _levelManager.OnProgressChanged += UpdateView;
            UpdateView(0f);
        }

        private void UpdateView(float obj)
        {
            _progressBar.RefreshView(obj, $"{Mathf.Floor(obj*100)}%");
        }
    }
}
