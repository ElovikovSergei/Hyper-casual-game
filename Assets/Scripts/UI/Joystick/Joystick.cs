using UnityEngine.InputSystem.OnScreen;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace UI.Joystick
{
    public sealed class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public event Action<Vector2> OnInputChangedEvent;

        [SerializeField] private JoystickType _type;

        [Header("Rect components")]
        [SerializeField] private RectTransform _centerRect;
        [SerializeField] private RectTransform _handleRect;
        [SerializeField] private RectTransform _baseRect;

        [Header("Canvas components")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Canvas _canvas;

        [Header("Floating pointer up data")]
        [SerializeField] private bool _hideOnPointerUp;
        [SerializeField] private bool _centralizeOnPointerUp;

        [Header("Other")]
        [SerializeField] private OnScreenStick _stickController;

        private Vector2 _initialPosition;

        public void OnPointerDown(PointerEventData eventData)
        {
            var constructedEventData = new PointerEventData(EventSystem.current);
            constructedEventData.position = _handleRect.position;

            _stickController.OnPointerDown(constructedEventData);

            if (_type == JoystickType.Floating)
            {
                _centerRect.anchoredPosition = GetAnchoredPosition(eventData.position);

                if (_hideOnPointerUp)
                    _canvasGroup.alpha = 1;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_type == JoystickType.Floating)
            {
                if (_centralizeOnPointerUp)
                    _centerRect.anchoredPosition = _initialPosition;

                _canvasGroup.alpha = _hideOnPointerUp ? 0f : 1f;
            }

            var constructedEventData = new PointerEventData(EventSystem.current);
            constructedEventData.position = Vector2.zero;

            _stickController.OnPointerUp(constructedEventData);
            OnInputChangedEvent?.Invoke(_handleRect.anchoredPosition / _stickController.movementRange);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_type == JoystickType.Floating)
                _stickController.OnDrag(eventData);

            OnInputChangedEvent?.Invoke(_handleRect.anchoredPosition / _stickController.movementRange);
        }

        private Vector2 GetAnchoredPosition(Vector2 screenPosition)
        {
            var camera = (_canvas.renderMode == RenderMode.ScreenSpaceCamera) ? _canvas.worldCamera : null;
            var localPoint = Vector2.zero;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, screenPosition, camera, out localPoint))
            {
                var pivotOffset = _baseRect.pivot * _baseRect.sizeDelta;
                return localPoint - (_centerRect.anchorMax * _baseRect.sizeDelta) + pivotOffset;
            }

            return Vector2.zero;
        }

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();

            var center = new Vector2(0.5f, 0.5f);

            _handleRect.anchoredPosition = Vector2.zero;
            _handleRect.anchorMin = center;
            _handleRect.anchorMax = center;
            _handleRect.pivot = center;
            _centerRect.pivot = center;

            _initialPosition = _centerRect.anchoredPosition;

            if (_type == JoystickType.Fixed)
            {
                _centerRect.anchoredPosition = _initialPosition;
                _canvasGroup.alpha = 1;
            }
            else if (_type == JoystickType.Floating)
            {
                _canvasGroup.alpha = _hideOnPointerUp ? 0f : 1f;
            }
        }


        private enum JoystickType : sbyte
        {
            Fixed = 0,
            Floating = 1
        }
    }
}