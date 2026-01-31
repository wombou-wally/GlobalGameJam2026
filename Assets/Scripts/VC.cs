using System.Collections.Generic;
using UnityEngine;

public class VC
{
    public float facilities = 50;
    public float employees = 400;
    public float money = 20000000;

    List<Deal> activeDeals = new List<Deal>();

    public void QuarterlyReport()
    {
        Debug.Log("______________Quarterly Report______________");
        for (int i = 0; i < activeDeals.Count; i++)
        {
            Debug.Log($"Deal with {activeDeals[i].companyName}: Board Happiness: {activeDeals[i].board_happiness:F2}");
            Debug.Log($"    Generates per quarter - Facilities: {activeDeals[i].facilities_generated * activeDeals[i].board_happiness}, " +
                              $"Employees: {activeDeals[i].employees_generated * activeDeals[i].board_happiness}, " +
                              $"Money: {activeDeals[i].money_generated * activeDeals[i].board_happiness}");
            facilities += (activeDeals[i].facilities_generated * activeDeals[i].board_happiness);
            employees += (activeDeals[i].employees_generated * activeDeals[i].board_happiness);
            money += (activeDeals[i].money_generated * activeDeals[i].board_happiness);
        }

        Debug.Log("Quarterly Report Bottom Line:");
        Debug.Log($"Facilities: {facilities}, Employees: {employees}, Money: {money}");
    }

    public void AddDeal(Deal deal)
    {
        facilities -= deal._facilities_offered;
        employees -= deal._employees_offered;
        money -= deal._money_offered;
        activeDeals.Add(deal);
    }
}