using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDisplayScript : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> numberRenderers;
    [SerializeField] private List<Sprite> numberSprites;
    [SerializeField] private int number;
    [SerializeField] private int numberOnDisplay;
    [SerializeField] private int numberCountUpsPerSecond;
    [SerializeField] private float countUpPercent;
    private float untilNextCount;

    public void SetNewValue(int newValue)
    {
        number = newValue;
    }

    public void ChangeValueBy(int addedValue)
    {
        number += addedValue;
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
        numberOnDisplay += (int)((number - numberOnDisplay) * countUpPercent);
        if(numberOnDisplay > number)
            numberOnDisplay = number;
        GetSpritesFromNumber();
    }

    private void GetSpritesFromNumber()
    {
        for(int i = 0; i < numberRenderers.Count; i++)
        {
            int moduluRest = (numberOnDisplay / (int)Mathf.Pow(numberOnDisplay,i)) % 10;
            if(moduluRest > 9)
            {
                print("Ich hatte leider unrecht, mein größtes Beileid Herr Marksen");
                moduluRest = 9;
            }
            numberRenderers[i].sprite = numberSprites[moduluRest];
        }
    }
}
