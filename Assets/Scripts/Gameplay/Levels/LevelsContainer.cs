using UnityEngine;

namespace Gameplay.Levels
{
    [CreateAssetMenu(fileName = "LevelsContainer", menuName = "TowerColor/Configs/Levels/LevelsContainer")]
    public class LevelsContainer : ScriptableObject
    {
        [SerializeField] private LevelBase[] _levels;

        [SerializeField] private LevelBase _default;
        public ILevelConfig GetLevel(int id)
        {
            if (id >= 0 && id < _levels.Length)
                return _levels[id];
            return _default;
        }
    }
}