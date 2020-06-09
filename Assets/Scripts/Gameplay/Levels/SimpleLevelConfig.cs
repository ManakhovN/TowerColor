using System.Collections;
using Gameplay.Field;
using UnityEngine;

namespace Gameplay.Levels
{
    [CreateAssetMenu(fileName = "SimpleLevelConfig", menuName = "TowerColor/Configs/Levels/SimpleLevelConfig")]
    public class SimpleLevelConfig : LevelBase
    {
        [SerializeField] private Color[] _colors;

        public override IEnumerable GetPositions()
        {
            int n = Mathf.FloorToInt(Mathf.PI * _bigR / _smallR);
            float angle = 2 * Mathf.PI / (n + 1);
            var bigR = 1.1f*_smallR * (n) / Mathf.PI;
            bool hasOffset = false;
            for (float h = 0; h < _height; h += 1f)
            {
                float startAngle = hasOffset ? angle / 2f : 0f;
                float endAngle = 2f * Mathf.PI + startAngle;
                hasOffset = !hasOffset;
                for (float a = startAngle; a < endAngle; a += angle)
                {
                    var positions = new Vector3(1.1f * bigR * Mathf.Sin(a), h, 1.1f * bigR * Mathf.Cos(a));
                    var r = Random.Range(0, _colors.Length);
                    yield return new FieldObject(positions, ElementType.Cyllinder, _colors[r], 10);
                }
            }
        }
    }
}