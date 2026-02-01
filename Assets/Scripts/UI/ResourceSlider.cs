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

        [SerializeField] private Image handle; 
        
        private string GetStringFormat()
        {
            return _t switch
            {
                ResourceType.Money => "C",
                ResourceType.Personnel or ResourceType.Facilities => "N0",
                _ => ""
            };
        }

        private Color GetColor()
        {
            Debug.Log($"GetColor for type: {_t}");
            return _t switch
            {
                ResourceType.Money => new Color(62.0f, 145.0f, 32.0f, 255.0f),
                ResourceType.Personnel => new Color(156.0f, 40.0f, 14.0f, 255.0f),
                ResourceType.Facilities => new Color(12.0f, 50.0f, 148.0f, 255.0f),
                _ => Color.white
            };
        }

        public ResourceType GetResourceType()
        {
            return _t;
        }

        public float GetValue()
        {
            return slider.value;
        }

        public void Refresh(float maxValue)
        {
            slider.maxValue = maxValue;
            slider.normalizedValue = 0.5f; // let's start off in the middle of the range
            currentValue.text = slider.value.ToString(GetStringFormat());
        }
        
        public void Init(ResourceType t)
        {
            _t = t; 
        
            if (slider != null)
            {
                // start amount is the max value
                slider.maxValue = 0;
                slider.minValue = 0;
                slider.value = 0;
            }

            if (resourceName != null)
            {
                resourceName.text = t.ToString();
            }

            if (sliderFill != null)
            { 
                sliderFill.color = GetColor();
            }

            if (handle != null)
            {
                handle.color = GetColor();
            }

            slider.onValueChanged.AddListener(TriggerChangeEvent);
        }

        private void TriggerChangeEvent(float change)
        {
            OnSliderValueChanged?.Invoke(_t, change);
        }
        
        private void Update()
        {
            currentValue.text = slider.value.ToString(GetStringFormat());
        }
       
    }
}