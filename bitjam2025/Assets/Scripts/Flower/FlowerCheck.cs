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
    [SerializeField] private bool[] minigamesSupposedToBeat;
    [SerializeField] private bool[] minigamesBeaten;
    [SerializeField] private List<CheckBoxInteract> allCheckboxes;
    [SerializeField] private int totalWrongFlowers;

    private void Awake()
    {
        instance = this;
    }

    public int GetFlowersWrong()
    {
        return totalWrongFlowers;
    }

    private void Start()
    {
        assumptions = new bool[5];
        allCheckboxes = gameObject.GetComponentsInChildren<CheckBoxInteract>().ToList();
        minigamesBeaten = new bool[3];
    }

    public void FlowerNowExists()
    {
        plantExists = true;
        assumptions = new bool[5];
    }

    public void CheckFlower(bool flowerWasSold)
    {
        if (!plantExists)
            return;
        plantExists = false;
        GetAssumptions();
        int badAmount = GetAmountOff(new bool[5]);
        bool correct;
        bool perfection = false;
        if (GetAmountOff(assumptions) != 0 || (badAmount > 0 && flowerWasSold) || (badAmount == 0 && !flowerWasSold))
        {
            //GetPoints(-)
            print("Wrong guess");
            totalWrongFlowers++;
            print(totalWrongFlowers);
            correct = false;
        }
        else
        {
            //GetPoints(+)
            print("Right guess");
            correct = true;
            if (CheckifMinigamesDone())
            {
                perfection = true;
                print("Perfektionist, Punktebonus");
            }
        }
        for ( int i = 0; i < allCheckboxes.Count; i++ )
        {
            allCheckboxes[i].ResetValue();
        }
        MoneyControl.Instance.FlowerReward(correct, perfection);
        StartCoroutine(GameManager.Instance.GetRidOfPlant(flowerWasSold, correct));
        return;
    }

    private bool CheckifMinigamesDone()
    {
        for ( int i = 0;i < minigamesBeaten.Length;i++ )
        {
            print(minigamesSupposedToBeat[i] + " " + minigamesBeaten[i]);
            if (minigamesBeaten[i] != minigamesSupposedToBeat[i])
                return false;
        }
        return true;
    }

    private int GetAmountOff(bool[] arrayToCheck)
    {
        int amount = 0;
        for (int i = 0; i < assumptions.Length; i++)
        {
            //print(i + ": " + arrayToCheck[i] + " vs " + correctAnswers[i]);
            if (arrayToCheck[i] != correctAnswers[i])
                amount++;
        }
        print(amount);
        return amount;
    }

    public void GetAssumptions()
    {
        for (int i = 0;i < allCheckboxes.Count;i++)
        {
            if(allCheckboxes[i].GetIndex() == 2)
            {
                assumptions[2] = allCheckboxes[i].GetValue();
                assumptions[3] = allCheckboxes[i].GetValue();
                continue;
            }
            if (allCheckboxes[i].GetIndex() == 3)
            {
                assumptions[4] = allCheckboxes[i].GetValue();
                continue;
            }
            assumptions[allCheckboxes[i].GetIndex()] = allCheckboxes[i].GetValue();
        }
    }

    public void GetAnswers(bool[] answers)
    {
        if (answers[2]|| answers[3])
        {
            answers[2] = true;
            answers[3] = true;
        }
        correctAnswers = answers;
    }

    public void SetMinigamesToBeBeaten(bool[] minigames)
    {
        minigamesSupposedToBeat = minigames;
    }

    public void MinigameWasBeaten(int minigameIndex)
    {
        minigamesBeaten[minigameIndex] = true;
    }
}
