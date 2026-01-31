using System;

public class BoardMember
    {
        public float happiness = 1f;
        private BoardMemberType _bmtype = BoardMemberType.Owner;

        public BoardMember(BoardMemberType type)
        {
            bmtype = type;
        }

        public BoardMemberType bmtype
        {
            get
            {
                return _bmtype;
            }
            set
            {
                _bmtype = value;
                switch (_bmtype)
                {
                    case BoardMemberType.Owner:
                        money_weight = 1.0f;
                        facilities_weight = 1.0f;
                        employees_weight = 1.0f;
                        break;
                    case BoardMemberType.HR:
                        money_weight = 0.5f;
                        facilities_weight = 0.5f;
                        employees_weight = 2.0f;
                        break;
                    case BoardMemberType.FacilitiesManager:
                        money_weight = 0.5f;
                        facilities_weight = 2.0f;
                        employees_weight = 0.5f;
                        break;
                    case BoardMemberType.Accountant:
                        money_weight = 2.0f;
                        facilities_weight = 0.5f;
                        employees_weight = 0.5f;
                        break;
                }
            }
        }

        float target_money;
        float target_facilities;
        float target_employees;

        float money_weight;
        float facilities_weight;
        float employees_weight;

        public HappinessLevel happinessLevel
        {
            get
            {
                if (happiness < 0.25f)
                    return HappinessLevel.Mad;
                else if (happiness < 0.5f)
                    return HappinessLevel.Unhappy;
                else if (happiness < 0.75f)
                    return HappinessLevel.Ok;
                else
                    return HappinessLevel.Happy;
            }
        }

        public void SetTargets(float money, float facilities, float employees)
        {
            target_money = money;
            target_facilities = facilities;
            target_employees = employees;

            Random random = MyRandom.Instance;

            target_money = random.Next((int)(money * 0.3f), (int)(money * 2.0f));
            target_facilities = random.Next((int)(facilities * 0.3f), (int)(facilities * 2.0f));
            target_employees = random.Next((int)(employees * 0.3f), (int)(employees * 2.0f));
        }

        public void UpdateHappiness(float offered_money, float offered_facilities, float offered_employees)
        {
            float money_diff = offered_money / target_money;
            float facilities_diff = offered_facilities / target_facilities;
            float employees_diff = offered_employees / target_employees;
            happiness = (money_diff * money_weight
                + facilities_diff * facilities_weight
                + employees_diff * employees_weight) / 3.0f;

            //maybe normalize happiness to be between 0 and 1?
            if (happiness > 1.0f)
                happiness = 1.0f;
            else if (happiness < 0.0f)
                happiness = 0.0f;
        }
    };//end BoardMember class    