using System.Collections;
using Gameplay.Field;
using UnityEngine;

namespace Gameplay.Levels
{
    [CreateAssetMenu(fileName = "ImageLevelConfig", menuName = "TowerColor/Configs/Levels/ImageLevelConfig")]
    public class ImageLevelConfig : LevelBase
    {
        [SerializeField] private Texture2D _levelTex;

        public override IEnumerable GetPositions()
        {
            int n = Mathf.FloorToInt(Mathf.PI * _bigR / _smallR);
            float angle = 2 * Mathf.PI / (n + 1);
            var bigR = 1.1f*_smallR * (n) / Mathf.PI;
            _height = _levelTex.height;
            bool hasOffset = false;
            for (int h = 0; h < _height; h += 1)
            {
                float startAngle = hasOffset ? angle / 2f : 0f;
                float endAngle = 2f * Mathf.PI + startAngle;
                hasOffset = !hasOffset;
                for (int i = 0; i<= n; i++)
                {
                    float a = startAngle + angle * i;
                    var positions = new Vector3(1.1f * bigR * Mathf.Sin(a), h, 1.1f * bigR * Mathf.Cos(a));
                    yield return new FieldObject(positions, ElementType.Cyllinder, 
                        _levelTex.GetPixel(n - i, h), 10);
                }
            }
        }
    }
}
