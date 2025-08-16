using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenBehaviour : MonoBehaviour
{
    [SerializeField] private MoneyDisplayScript[] allDataDisplays;
    [SerializeField] private SpriteRenderer gameEndMessage;
    [SerializeField] private Sprite gameWonSprite;
    [SerializeField] private Sprite gameLostSprite;
    [SerializeField] private GameObject continueButton;


    public void SetupEndScreen(bool won,bool nextLevelExists, int[] achievedData)
    {
        if(won)
            gameEndMessage.sprite = gameWonSprite;
        else
            gameEndMessage.sprite = gameLostSprite;
        continueButton.SetActive(nextLevelExists);
        for(int i = 0; i < allDataDisplays.Length; i++)
        {
            allDataDisplays[i].SetNewValue(achievedData[i]);
            print(achievedData[i]);
        }
    }
}
