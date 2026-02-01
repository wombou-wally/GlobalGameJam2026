using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
   public class SliderGroup : MonoBehaviour
   {
      private RectTransform _rT; 
      
      [SerializeField] 
      private ResourceSlider sliderPrefab;

      private Dictionary<ResourceType, ResourceSlider> _sliders;
      
      private void Awake()
      {
         _rT = GetComponent<RectTransform>();
         _sliders = new Dictionary<ResourceType, ResourceSlider>();
         Init();
      }

      private void OnEnable()
      {
         GameController.OnNewDealStarted += OnNewDealStarted;
      }

      private void OnDisable()
      {
         GameController.OnNewDealStarted -= OnNewDealStarted;
      }

      private void OnNewDealStarted()
      {
         _sliders[ResourceType.Money].Refresh(GameController.Instance.playerVC.money);
         _sliders[ResourceType.Facilities].Refresh(GameController.Instance.playerVC.facilities);
         _sliders[ResourceType.Personnel].Refresh(GameController.Instance.playerVC.employees);
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
               instance.Init((ResourceType) types.GetValue(i));
               _sliders.Add((ResourceType) types.GetValue(i), instance);
            } 
         }
      } 
      
      public List<ResourceSlider> GetSliders(){
         return _sliders.Values.ToList();
      }
   }
}