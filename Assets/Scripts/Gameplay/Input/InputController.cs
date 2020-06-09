using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;
namespace Gameplay.Input
{
    public class InputController : IInputController 
    {
        private GameObject _inputCanvas;
        private CameraController _cameraController;

        public event Action<Vector3, Collider> OnObjectClick = delegate(Vector3 vector3, Collider collider) {  };
        public event Action<Vector2> OnDrag = delegate(Vector2 vector2) {  };

        public InputController(StringPrefabPairContainer prefabContainer, CameraController cameraController)
        {
            _inputCanvas = GameObject.Instantiate(prefabContainer.Get("InputCanvas"));
            _cameraController = cameraController;
            var eventTrigger = _inputCanvas.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { Raycast(eventData); });
            eventTrigger.triggers.Add(entry);
            
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.Drag;
            entry2.callback.AddListener((eventData) => { Drag(eventData); });
            eventTrigger.triggers.Add(entry2);
        }

        private void Drag(BaseEventData eventData)
        {
            OnDrag.Invoke(((PointerEventData)eventData).delta);
        }

        private void Raycast(BaseEventData eventData)
        {
            RaycastHit hit;
            Ray ray = _cameraController.Camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                OnObjectClick.Invoke(hit.point, hit.collider);
            }
        }

    }
}