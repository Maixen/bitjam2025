using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyControl : MonoBehaviour
{
    public static MoneyControl Instance;
    [SerializeField] private MoneyDisplayScript playerMoney;
    [SerializeField] private MoneyDisplayScript neededMoney;
    [SerializeField] private MoneyDisplayScript streakDisplay;
    [Space]
    [SerializeField] private int moneyByCorrectGuess;
    [SerializeField] private int moneyByWrongGuess;
    [SerializeField] private int moneyPerfectPlantBonus;
    [SerializeField] private int moneyByMinigameBeaten;
    [SerializeField] private int moneyByCorrectStreak;
    [SerializeField] private int streakamount;
    [SerializeField] private int totalEarnedMoney;
    [SerializeField] private int highestStreak;
    [SerializeField] private int maxBonusAtStreak;


    private void Awake()
    {
        Instance = this;
        neededMoney.SetNewValue(Timer.instance.GetNextRoundGoal());
        highestStreak = 0;
    }

    public void MinigameReward()
    {
        GivePlayerMoney(moneyByMinigameBeaten);
    }

    public int GetBestStreak()
    {
        return streakamount;
    }

    public int GetMoneyEarnedInTotal()
    {
        return totalEarnedMoney;
    }
    public void FlowerReward(bool wasCorrect, bool perfectionAchieved)
    {
        if(!wasCorrect)
        {
            GivePlayerMoney(moneyByWrongGuess);
            streakamount = 0;
            streakDisplay.SetNewValue(streakamount);
            return;
        }
        if(!perfectionAchieved)
        {
            GivePlayerMoney(moneyByCorrectGuess);
            return;
        }
        GivePlayerMoney(MoneyGainedOnStreak());
        streakamount++;
        streakDisplay.SetNewValue(streakamount);
        if(streakamount > highestStreak)
            highestStreak = streakamount;
    }

    private int MoneyGainedOnStreak()
    {
        int streakAmountCounted = streakamount;
        if (streakAmountCounted > maxBonusAtStreak)
            streakAmountCounted = maxBonusAtStreak;
        return moneyByCorrectGuess + moneyPerfectPlantBonus + moneyByCorrectStreak * streakAmountCounted;
    }
    private void GivePlayerMoney(int amount)
    {
        totalEarnedMoney += amount;
        playerMoney.ChangeValueBy(amount);
    }

    public void ResetPlayerMoney()
    {
        playerMoney.SetNewValue(0);
    }

    public void Payday()
    {
        if(playerMoney.GetValue() < neededMoney.GetValue())
        {
            //end of Game, you lost
            playerMoney.SetNewValue(0);
            GameManager.Instance.InitiateGameEnd(false);
            return;
        }
        playerMoney.ChangeValueBy(-neededMoney.GetValue());
        print("survived Round");
        if(!Timer.instance.AdvanceRound())
        {
            return;
        }
        neededMoney.SetNewValue(Timer.instance.GetNextRoundGoal());
    }
}
