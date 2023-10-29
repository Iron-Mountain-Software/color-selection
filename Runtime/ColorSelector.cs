using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpellBoundAR.ColorSelection
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Image))]
    public class ColorSelector : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        public event Action OnSelectedColorChanged;

        [Header("Settings")]
        [SerializeField] private bool selectOnStart = true;
        [SerializeField] private Vector2 pixelSelectedOnStart = new (.5f, .5f);
        
        [Header("References")]
        [SerializeField] private RectTransform pointer;
    
        [Header("Cache")]
        private RectTransform _rectTransform;
        private Image _image;
        private Color _selectedColor = Color.white;

        private RectTransform RectTransform
        {
            get
            {
                if (!_rectTransform) _rectTransform = GetComponent<RectTransform>();
                _rectTransform.rotation = Quaternion.identity;
                return _rectTransform;
            }
        }
        
        private Image Image
        {
            get
            {
                if (!_image) _image = GetComponent<Image>();
                _image.preserveAspect = false;
                return _image;
            }
        }

        public Color SelectedColor
        {
            get => _selectedColor;
            private set
            {
                if (_selectedColor == value) return;
                _selectedColor = value;
                OnSelectedColorChanged?.Invoke();
            }
        }

        private void Start() => Reset();

        private void Reset()
        {
            if (selectOnStart) SelectColor(pixelSelectedOnStart.x, pixelSelectedOnStart.y);
        }

        public void OnPointerDown(PointerEventData eventData) => SelectColor(eventData.position);

        public void OnDrag(PointerEventData eventData) => SelectColor(eventData.position);
    
        private void SelectColor(Vector2 touchPosition)
        {
            Rect rect = RectTransform.rect;
            Vector3 position = RectTransform.position;
            Vector3 localScale = RectTransform.localScale;
            float halfWidth = rect.width * localScale.x / 2;
            float halfHeight = rect.height * localScale.y / 2;
            float percentAcross = Mathf.InverseLerp(position.x - halfWidth, position.x + halfWidth, touchPosition.x);
            float percentUp = Mathf.InverseLerp(position.y - halfHeight, position.y + halfHeight, touchPosition.y);
            SelectColor(percentAcross, percentUp);
        }

        private void SelectColor(float percentAcross, float percentUp)
        {
            Texture2D texture = Image.sprite.texture;
            if (texture)
            {
                int x = Mathf.Clamp(Mathf.RoundToInt(texture.width * percentAcross), 0, texture.width);
                int y = Mathf.Clamp(Mathf.RoundToInt(texture.height * percentUp), 0, texture.height);
                SelectedColor = texture.GetPixel(x, y);
            }
            else SelectedColor = Image.color;
            UpdatePointer(percentAcross, percentUp);
        }

        private void UpdatePointer(float percentAcross, float percentUp)
        {
            if (!pointer) return;
            pointer.gameObject.SetActive(true);
            Vector2 anchor = new Vector2(percentAcross, percentUp);
            pointer.anchorMin = anchor;
            pointer.anchorMax = anchor;
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            pixelSelectedOnStart.x = Mathf.Clamp01(pixelSelectedOnStart.x);
            pixelSelectedOnStart.y = Mathf.Clamp01(pixelSelectedOnStart.y);
        }

#endif
        
    }
}
