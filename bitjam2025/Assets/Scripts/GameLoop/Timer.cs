using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    [SerializeField] private bool isPaused;
    [SerializeField] private float totalTimeSurvived;
    [Space]
    [SerializeField] private SpriteRenderer clockRenderer;
    [SerializeField] private Sprite[] clockSprites;
    [SerializeField] private int currentClockSprite;
    [SerializeField] private bool isEndless;
    [SerializeField] private int endlessStartNeededMoney;
    [SerializeField] private float neededMoneyGrowthPercent;
    [SerializeField] private int maxGrowthReachedAtRound;
    [Space]
    [SerializeField] private int roundNum;
    [SerializeField, Range(1,5)] private int maxFixedRoundNum;
    [SerializeField] private float timeBetweenRounds;
    [SerializeField] private float timeUntilnextRound;
    [SerializeField] private List<int> fixedRoundNeededMoneyList;

    private void Awake()
    {
        instance = this;
        maxFixedRoundNum = fixedRoundNeededMoneyList.Count;
        isPaused = false;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
    }

    public bool AdvanceRound()
    {
        if(isEndless)
            return true;
        roundNum++;
        if(roundNum >= maxFixedRoundNum)
            return false;
        return true;
    }

    public int GetNextRoundGoal()
    {
        if (isEndless)
            return GetRoundMoneyEndless();
        return fixedRoundNeededMoneyList[roundNum];
    }

    private int GetRoundMoneyEndless()
    {
        int round = roundNum;
        if(roundNum > maxGrowthReachedAtRound)
            round = maxGrowthReachedAtRound;
        int newRoundMoney = endlessStartNeededMoney * (int)Mathf.Pow(1 + neededMoneyGrowthPercent, round);
        return newRoundMoney;
    }

    private void Update()
    {
        if(GameManager.Instance.gameIsDone || isPaused)
            return;
        if(timeUntilnextRound < timeBetweenRounds)
        {
            timeUntilnextRound += Time.deltaTime;
            totalTimeSurvived += Time.deltaTime;
            if (currentClockSprite != GetSpriteFromTime(timeUntilnextRound / timeBetweenRounds))
            {
                currentClockSprite = GetSpriteFromTime(timeUntilnextRound / timeBetweenRounds);
                clockRenderer.sprite = clockSprites[currentClockSprite];
                //tick sound
            }
            return;
        }
        timeUntilnextRound = 0;
        MoneyControl.Instance.Payday();
    }

    private int GetSpriteFromTime(float percentOfTimePassed)
    {
        float amountOfSprites = clockSprites.Length;
        for(int i = 1; i < clockSprites.Length; i++)
        {
            if(percentOfTimePassed < (1 / amountOfSprites) * i)
                return i-1;
        }
        return 0;
    }

    public int GetTimeSurvived()
    {
        return (int)totalTimeSurvived;
    }
}
