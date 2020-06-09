using System;
using UnityEngine;

namespace Gameplay.Input
{
    public interface IInputController
    {
        event Action<Vector3, Collider> OnObjectClick;
        event Action<Vector2> OnDrag;
    }
}
