using UnityEngine;
using UnityEngine.UI;

namespace SpellBoundAR.ColorSelection
{
    [RequireComponent(typeof(Graphic))]
    public class SelectedColorGraphic : MonoBehaviour
    {
        [SerializeField] private ColorSelector colorSelector;
        
        [Header("Cache")]
        private Graphic _graphic;

        private void Awake()
        {
            _graphic = GetComponent<Graphic>();
        }

        private void OnEnable()
        {
            if (colorSelector) colorSelector.OnSelectedColorChanged += Refresh;
            Refresh();
        }
        
        private void OnDisable()
        {
            if (colorSelector) colorSelector.OnSelectedColorChanged -= Refresh;
        }

        private void Refresh()
        {
            if (!_graphic) return;
            _graphic.color = colorSelector ? colorSelector.SelectedColor : Color.clear;
        }

#if UNITY_EDITOR
        
        private void OnValidate()
        {
            if (!colorSelector) colorSelector = GetComponentInParent<ColorSelector>();
        }
        
#endif
        
    }
}
