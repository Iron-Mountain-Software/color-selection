using UnityEngine;
using UnityEngine.UI;

namespace SpellBoundAR.ColorSelection
{
    [RequireComponent(typeof(Graphic))]
    public class SelectedColorGraphic : MonoBehaviour
    {
        [SerializeField] private Graphic graphic;
        [SerializeField] private ColorSelector colorSelector;

        private void Awake()
        {
            if (!graphic) graphic = GetComponent<Graphic>();
            if (!colorSelector) colorSelector = GetComponentInParent<ColorSelector>();
        }
        
        private void OnValidate()
        {
            if (!graphic) graphic = GetComponent<Graphic>();
            if (!colorSelector) colorSelector = GetComponentInParent<ColorSelector>();
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
            if (graphic) graphic.color = colorSelector 
                ? colorSelector.SelectedColor
                : Color.clear;
        }
    }
}