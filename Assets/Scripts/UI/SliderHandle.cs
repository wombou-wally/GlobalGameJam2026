using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class SliderHandle: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool _pointerUpTriggered = false;
        
        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && _pointerUpTriggered)
            {
                // TODO - add audio trigger here
                Debug.Log("Pointer is up");
                _pointerUpTriggered = false;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
           // Intentionally blank 
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pointerUpTriggered = true;
        }
    }
}