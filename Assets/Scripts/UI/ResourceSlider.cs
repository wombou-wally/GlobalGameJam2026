using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourceSlider : MonoBehaviour
    {
        public static event Action<ResourceType, float> OnSliderValueChanged;

        [SerializeField] private Slider slider;

        private ResourceType _t;

        [SerializeField] private TextMeshProUGUI resourceName;

        [SerializeField] private TextMeshProUGUI currentValue;

        [SerializeField] private Image sliderFill;

        private string GetStringFormat(ResourceType t)
        {
            return t switch
            {
                ResourceType.Money => "C",
                ResourceType.Personnel or ResourceType.Facilities => "N0",
                _ => ""
            };
        }

        private Color GetColor(ResourceType t)
        {
            return t switch
            {
                ResourceType.Money => new Color(0, 200, 0),
                ResourceType.Personnel => new Color(200, 0, 0),
                ResourceType.Facilities => new Color(0, 0, 200),
                _ => Color.white
            };
        }

        public ResourceType GetType()
        {
            return _t;
        }

        public float GetValue()
        {
            return slider.value;
        }

    public void Init(ResourceType t, float startAmount)
        {
            _t = t; 
        
            if (slider != null)
            {
                // start amount is the max value
                slider.maxValue = startAmount;
                slider.minValue = 0;
                slider.value = startAmount;
            }

            if (resourceName != null)
            {
                resourceName.text = t.ToString();
            }

            if (currentValue != null)
            {
                currentValue.text = startAmount.ToString(GetStringFormat(t)); 
            }

            if (sliderFill != null)
            {
                sliderFill.color = GetColor(t);
            }

            slider.onValueChanged.AddListener(TriggerChangeEvent);
        }

        private void TriggerChangeEvent(float change)
        {
            Debug.Log($"Slider change amount: {change}");
            OnSliderValueChanged?.Invoke(_t, change);
        }
        
        private void Update()
        {
            currentValue.text = slider.value.ToString(GetStringFormat(_t)); 
        }
        
    }
}