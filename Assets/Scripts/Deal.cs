 using System;
 using System.Collections.Generic;
 using Unity.VisualScripting;
 using UnityEngine;
 using Random = System.Random;

 public class Deal
    {
        
        public string companyName = "UNINITIALIZED";
        public float board_happiness = 0;
        //these are what the deal offers per quarterly update...multiply by teh happiness 
        //of the board to get actual resources gained
        public float money_generated;
        public float facilities_generated;
        public float employees_generated;
        float base_money;
        float base_facilities;
        float base_employees;
        public float _money_offered = 0;
        public float _facilities_offered = 0;
        public float _employees_offered = 0;

        public List<BoardMember> boardMembers;
        BoardMember MakeRandomBoardMember()
        {
            Array values = Enum.GetValues(typeof(BoardMemberType));
            Random random = MyRandom.Instance;
            BoardMemberType randomType = (BoardMemberType)values.GetValue(random.Next(values.Length));
            return new BoardMember(randomType);
        }

        string GenerateRandomCompanyName()
        {
            string[] prefixes = { "Tech", "Info", "Data", "Net", "Sys", "Global", "Next", "Future", "Prime", "Alpha" };
            string[] suffixes = { "Solutions", "Systems", "Technologies", "Corporation", "Enterprises", "Dynamics", "Innovations", "Labs", "Works", "Studios", "Foundations",
            "Factories", "Trucking", "Warehouses", "Shipping"};
            Random random = MyRandom.Instance;
            string prefix = prefixes[random.Next(prefixes.Length)];
            string suffix = suffixes[random.Next(suffixes.Length)];
            return prefix + " " + suffix;
        }

        public Deal()
        {
            Random random = MyRandom.Instance;
            float base_money = (float)random.NextDouble() * 10000000f + 500000f;
            float base_facilities = (float)random.NextDouble() * 10f + 1;
            float base_employees = (float)random.NextDouble() * 20f + 5; ;
            _money_offered = 0;
            _facilities_offered = 0;
            _employees_offered = 0;
            boardMembers = new List<BoardMember>();
            boardMembers.Add(MakeRandomBoardMember());
            boardMembers.Add(MakeRandomBoardMember());
            boardMembers.Add(MakeRandomBoardMember());
            for (int i = 0; i < boardMembers.Count; i++)
            {
                boardMembers[i].SetTargets(base_money, base_facilities, base_employees);
            }

            money_generated = base_money / 10;
            facilities_generated = base_facilities / 10;
            employees_generated = base_employees / 10;

            companyName = GenerateRandomCompanyName();
        }

        public bool MakeProposal(float offered_money, float offered_facilities, float offered_employees)
        {
            _money_offered = offered_money;
            board_happiness = 0;
            for (int i = 0; i < boardMembers.Count; i++)
            {
                boardMembers[i].UpdateHappiness(offered_money,
                    offered_facilities,
                    offered_employees);
                board_happiness += boardMembers[i].happiness;
            }
            board_happiness /= boardMembers.Count;
            bool accepted = true;
            for (int i = 0; i < boardMembers.Count; i++)
            {
                if (boardMembers[i].happinessLevel == HappinessLevel.Mad ||
                    boardMembers[i].happinessLevel == HappinessLevel.Unhappy)
                {
                    accepted = false;
                    break;
                }
            }
            return accepted;
        }

        public void PrintMembers()
        {
            for (int i = 0; i < boardMembers.Count; i++)
            {
                Debug.Log($"Board Member {i + 1} Type: {boardMembers[i].bmtype}, Happiness: {boardMembers[i].happinessLevel}");
            }
        }
    }//end Deal class  