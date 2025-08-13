using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlowerCheck : MonoBehaviour
{
    public static FlowerCheck instance;
    [SerializeField] private bool plantExists;
    [SerializeField] private bool[] assumptions;
    [SerializeField] private bool[] correctAnswers;
    [SerializeField] private List<CheckBoxInteract> allCheckboxes;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        assumptions = new bool[5];
        allCheckboxes = gameObject.GetComponentsInChildren<CheckBoxInteract>().ToList();
    }

    public void FlowerNowExists()
    {
        plantExists = true;
    }

    public void CheckFlower(bool flowerWasSold)
    {
        if (!plantExists)
            return;
        plantExists = false;
        GetAssumptions();
        int badAmount = GetAmountOff(new bool[5]);
        if (flowerWasSold && badAmount != 0)
        {
            print("Fehlerhaftes Sellen");
            //Sell animation starten
            return;
        }
        if (flowerWasSold)
        {
            print("Korrektes Sellen");
            //Sell Animation starten
            return;
        }
        if (!flowerWasSold && badAmount == 0)
        {
            print("Fehlerhaftes Dumpen");
            //Dump animation starten
            return;
        }
        if(!flowerWasSold && GetAmountOff(assumptions) != 0)
        {
            print("Teils Fehlerhaftes Dumpen");
            //Dump animation starten, weniger Punkte?
            return;
        }
        print("Korrektes Dumpen");
        //Dump animation starten
        return;
    }

    private int GetAmountOff(bool[] arrayToCheck)
    {
        int amount = 0;
        for (int i = 0; i < assumptions.Length; i++)
        {
            if (arrayToCheck != correctAnswers)
                amount++;
        }
        return amount;
    }

    public void GetAssumptions()
    {
        for (int i = 0;i < allCheckboxes.Count;i++)
        {
            assumptions[allCheckboxes[i].GetIndex()] = allCheckboxes[i].GetValue();
            allCheckboxes[i].ResetValue();
        }
    }
}
