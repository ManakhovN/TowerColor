using System.Numerics;
using DG.Tweening;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Utils
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private int _offset = -3;

        private int _currentHeight;
        
        public Camera Camera => _camera;

        public int Offset => _offset;

        public int CurrentHeight
        {
            get => _currentHeight;
            set => _currentHeight = value + _offset;
        }

        public void MoveCameraToHeight(int height)
        {
            CurrentHeight = height;
            int result = Mathf.Clamp(height - 3, 0, 10000);
            transform.position = Vector3.Lerp(transform.position, new Vector3(0f, result, 0f), 0.5f*Time.deltaTime); 
        }

        private float animationDuration = 3f;
        public Tween Rotate()
        {
            return transform.DORotate(new Vector3(0,360f, 0f), animationDuration, RotateMode.WorldAxisAdd).SetEase(Ease.Linear);
        }

        public Tween Move()
        {
            return transform.DOMoveY(CurrentHeight, animationDuration);
        }

        public void Rotate(Vector2 vector2)
        {
            transform.Rotate(0f, vector2.x, 0f);
        }
    }
}
