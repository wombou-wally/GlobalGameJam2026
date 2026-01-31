using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
   public class SliderGroup : MonoBehaviour
   {
      private RectTransform _rT; 
      
      [SerializeField] 
      private ResourceSlider sliderPrefab;

      private List<ResourceSlider> _sliders;
      
      private void Awake()
      {
         _rT = GetComponent<RectTransform>();
         Init();
      }

      // TODO - pass a scriptable obj w/ starting values for each resource type - David M. 
      public void Init()
      {
         // generate some sliders to test the layout 
         if (sliderPrefab != null)
         {
            int typeCount =  Enum.GetNames(typeof(ResourceType)).Length;
            var types = Enum.GetValues(typeof(ResourceType));
            for (int i = 0; i < typeCount; i++)
            {
               var instance = Instantiate(sliderPrefab, _rT);
               instance.Init((ResourceType) types.GetValue(i), 1000);
               _sliders.Add(instance);
            } 
         }
      } 
      
      public List<ResourceSlider> GetSliders(){
         return _sliders;
      }
   }
}