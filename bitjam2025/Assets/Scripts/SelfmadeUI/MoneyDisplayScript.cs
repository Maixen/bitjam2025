using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class MoneyDisplayScript : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> numberRenderers;
    [SerializeField] private List<Sprite> numberSprites;
    [SerializeField] private int number;
    [SerializeField] private int numberOnDisplay;
    [SerializeField, Range(0.1f,10)] private float numberCountUpsPerSecond;
    [SerializeField] private float countUpPercent;
    private float untilNextCount;
    bool valueJustGotChanged = false;


    private void Start()
    {
        untilNextCount = 0;
    }
    public void SetNewValue(int newValue)
    {
        number = newValue;
        if (newValue < 0)
            newValue = 0;
    }

    public void ChangeValueBy(int addedValue)
    {
        number += addedValue;
        valueJustGotChanged = true;
        if (addedValue < 0)
            addedValue = 0;
    }

    public int GetValue()
    {
        return number;
    }

    private void Update()
    {
        if (number == numberOnDisplay)
            return;
        if(untilNextCount < 1/numberCountUpsPerSecond)
        {
            untilNextCount += Time.deltaTime;
            return;
        }
        untilNextCount = 0;
        float numberAddedToDisplay = ((float)(number - numberOnDisplay) * countUpPercent);
        if(numberAddedToDisplay < 0 && numberAddedToDisplay > -1) 
            numberAddedToDisplay = -1;
        if (numberAddedToDisplay > 0 && numberAddedToDisplay < 1)
            numberAddedToDisplay = 1;
        numberOnDisplay += (int)numberAddedToDisplay;
        int[] digits = new int[5];
        int[] digitsWithNoGoodArray = IntToIntArray(numberOnDisplay);
        if (digitsWithNoGoodArray.Length > 5)
            digitsWithNoGoodArray = new int[5] {9,9,9,9,9};
        for (int i = 0; i < digitsWithNoGoodArray.Length; i++)
            digits[i] = digitsWithNoGoodArray[i];
        for(int i = 0; i < digits.Length; i++)
        {
            print(i + ": " + digits[i]);
            numberRenderers[i].sprite = numberSprites[digits[i]];
        }
    }

    private int[] IntToIntArray(int num)
    {
        if (num == 0)
            return new int[5] { 0,0,0,0,0 };

        List<int> digits = new List<int>();

        for (; num != 0; num /= 10)
            digits.Add(num % 10);

        int[] array = digits.ToArray();

        return array;
    }
}
