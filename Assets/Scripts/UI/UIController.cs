using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    // This class manages updates to BoardMemberUI instances, company name text, clock, deal button, sliders, etc. 
    public class UIController : MonoBehaviour
    {
        public static UIController Instance { get; private set; }
        
        [SerializeField] private List<BoardMemberData> _memberData;
        
        [SerializeField] private TextMeshProUGUI companyName;

        [SerializeField] private List<BoardMemberUI> boardMembers;

        [SerializeField] private Clock clock;

        [SerializeField] private Button dealBtn;

        [SerializeField] private SliderGroup sliders;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
            Init(); 
        }

        private void OnEnable()
        {
            GameController.OnDealUpdated += HandleProposalUpdates;
            GameController.OnNewDealStarted += OnNewDealStarted; 
        }

        private void OnDisable()
        {
            GameController.OnDealUpdated -= HandleProposalUpdates;
            GameController.OnNewDealStarted -= OnNewDealStarted;
        }

        /// <summary>
        /// TODO - build this out to do proper updates - just testing scriptable objects here - David M.
        /// </summary>
        public void Init()
        {
            // copy scriptable objects 
            var boardMemberCopies = new List<BoardMemberData>();
            
            foreach (var memberData in _memberData)
            {
                boardMemberCopies.Add(Instantiate(memberData));    
            }
            
            Utils.Shuffle(boardMemberCopies);
            
            for (var i = 0; i < boardMembers.Count; i++)
            {
               boardMembers[i].Init(boardMemberCopies[i]); 
            }
        }

        public List<Tuple<ResourceType,float>> GetCurrentSliderValues()
        {
            List<Tuple<ResourceType, float>> values = new List<Tuple<ResourceType, float>>();
            sliders.GetSliders().ForEach(s => values.Add(new Tuple<ResourceType, float>(s.GetResourceType(), s.GetValue())));
            return values;
        }

        private void HandleProposalUpdates()
        {
            // TODO - this is where we'll update the UI to reflect current happiness level of the board members - David M.

            for (var index = 0; index < GameController.Instance.currentDeal.boardMembers.Count; index++)
            {
                var happiness = GameController.Instance.currentDeal.boardMembers[index].happinessLevel;
                Debug.Log($"Happiness is {happiness}");
                boardMembers[index].SetMask((int) happiness);
            }
        }

        private void OnNewDealStarted()
        {
            Debug.Log($"The current company name is: {GameController.Instance.currentDeal.companyName}");
            companyName.text = GameController.Instance.currentDeal.companyName;
        }
    }
}