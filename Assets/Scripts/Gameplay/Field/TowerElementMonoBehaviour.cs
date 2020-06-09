using UnityEngine;
using Zenject;

namespace Gameplay.Field
{
    public class TowerElementMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Rigidbody _rigidbody;
        public FieldObject FieldObject { get; set; }

        private bool _isInteractable = false;

        public bool IsInteractable
        {
            get => _isInteractable && gameObject.activeSelf;
            set
            {
                IsColored = value;
                IsKinematic = !value;
                _isInteractable = value;
            }
        }

        public bool IsKinematic
        {
            set => _rigidbody.isKinematic = value;
            get => _rigidbody.isKinematic;
        }

        public bool IsColored
        {
            set
            {
                if (value)
                    ColorFromConfig();
                else
                    ColorAsGray();
            }
        }

        public void OnValidate()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void ColorAsGray()
        {
            _meshRenderer.material.SetColor("_Color", Color.gray);
        }

        public void ColorFromConfig()
        {
            _meshRenderer.material.SetColor("_Color", FieldObject.Color);
        }


        public class Factory : PlaceholderFactory<UnityEngine.Object, TowerElementMonoBehaviour>
        {
        }
    }
}