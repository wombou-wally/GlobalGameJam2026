using System;
using UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static event Action OnDealUpdated;
    public static event Action OnDealSuccess;
    public static event Action OnNewDealStarted;
    
    public static GameController Instance;

    public VC playerVC;

    public Deal currentDeal;
    
    public void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("GameController instance already exists! - destroying it");
            Destroy(gameObject);
        }

        playerVC = new VC();
        
        Instance = this;
    }

    private void OnEnable()
    {
       ResourceSlider.OnSliderValueChanged += OnSliderChanged; 
    }

    private void OnDisable()
    {
        ResourceSlider.OnSliderValueChanged -= OnSliderChanged;
    }

    private void OnSliderChanged(ResourceType t, float amount)
    {
        var sliderValues = UIController.Instance.GetCurrentSliderValues();
        var offeredMoney = sliderValues.Find(v => v.Item1 == ResourceType.Money).Item2;
        var offeredFacilities = sliderValues.Find(v => v.Item1 == ResourceType.Facilities).Item2;
        var offeredPersonnel =  sliderValues.Find(v => v.Item1 == ResourceType.Personnel).Item2;
        
        // Make sure the current offer isn't null before trying to make a new proposal 

        if (currentDeal != null)
        {
            bool accepted = currentDeal.MakeProposal(offeredMoney, offeredFacilities, offeredPersonnel);

            // TODO - finish handling deal acceptance results - David M. 
            if (accepted)
            {
                playerVC.AddDeal(currentDeal);
                OnDealSuccess?.Invoke();
                
                // TODO - we should trigger another event to clean up the UI (i.e., repopulate the board members, reset the sliders, and generate a new deal instance - DAM 
            }
            OnDealUpdated?.Invoke();
        }
    }
}