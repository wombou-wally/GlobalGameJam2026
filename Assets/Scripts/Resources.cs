// For now, enums and general utils 
using System.Collections.Generic;
using Random = UnityEngine.Random;

public enum ResourceType
{
    Money = 1,
    Personnel = 2,
    Facilities = 3
}

public enum BoardMemberType 
{
    Owner = 1, // likes all three resources equally
    HR = 2, // likes employees the most
    FacilitiesManager = 3, // likes facilities the most
    Accountant = 4 // likes money the most
}

public enum HappinessLevel
{
    Mad = 0, 
    Unhappy = 1, 
    Ok = 2, 
    Happy = 3 
}

public static class Utils
{
    // Fisher-Yates shuffle
    public static void Shuffle<T>(IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int r = Random.Range(0, i + 1);
            (list[i], list[r]) = (list[r], list[i]);
        }
    }
}