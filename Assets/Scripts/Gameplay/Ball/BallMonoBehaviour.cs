using System;
using Gameplay.Field;
using UnityEngine;
using Utils;

namespace Gameplay.Ball
{
    public class BallMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Rigidbody _rigidbody;

        public event Action<TowerElementMonoBehaviour> OnCollide = delegate(TowerElementMonoBehaviour collision) { };
        public int CurrentColor { get; set; }

        public void OnValidate()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void SetColor(int color)
        {
            _meshRenderer.material.SetColor("_Color", color.ToColor());
            CurrentColor = color;
        }

        public void OnCollisionEnter(Collision other)
        {
            var towerMB = other.collider.GetComponent<TowerElementMonoBehaviour>();
            if (towerMB)
                OnCollide.Invoke(towerMB);
        }
    }
}