using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerSelector", menuName = "Scriptable Objects/CustomerSelector", order = 1)]
public class CustomerSelectorSO : ScriptableObject
{
    [SerializeField] private List<CustomerSO> scientistList;
    [SerializeField] private List<CustomerSO> ordinaryNPCList;


    public CustomerSO GetRandomScientist()
    {
        int r = Random.Range(0, scientistList.Count);

        return scientistList[r];
    }


    public CustomerSO GetScientistByName(string name)
    {
        return scientistList.Find(x => x.CustomerName == name);
    }


    public CustomerSO GetRandomOrdinaryNPC()
    {
        int r = Random.Range(0, ordinaryNPCList.Count);

        return ordinaryNPCList[r];
    }
}

