using UnityEngine;
using Utils;

namespace Gameplay.Field
{
    public class FieldObject
    {
        private Vector3 _position;
        private ElementType _elementType;
        private int _colorInt;
        private Color _color;

        public Vector3 Position
        {
            get => _position;
            set => _position = value;
        }

        public ElementType ElementType
        {
            get => _elementType;
            set => _elementType = value;
        }

        public int ColorInt
        {
            get => _colorInt;
            set => _colorInt = value;
        }

        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        public FieldObject(Vector3 position, ElementType elementType, int colorInt, Color color)
        {
            _position = position;
            _elementType = elementType;
            _colorInt = colorInt;
            _color = color;
        }
        
        public FieldObject(Vector3 position, ElementType elementType, Color color, int precision = 1)
        {
            _position = position;
            _elementType = elementType;
            _colorInt = color.ToIntWithSomePrecision(precision);
            _color = color;
        }
    }

    public enum ElementType
    {
        Cyllinder
    }
}
