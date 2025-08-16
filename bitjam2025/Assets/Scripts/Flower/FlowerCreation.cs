using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlowerCreation : MonoBehaviour
{
    public static FlowerCreation instance;

    [SerializeField] private GameObject flowerPrefab;
    [SerializeField] private Vector2 spawnPos;
    [SerializeField] private Vector2 spawnSize;
    [Space]
    [SerializeField] private List<Sprite> vaseGood;
    [SerializeField] private List<Sprite> stemGood;
    [SerializeField] private List<Sprite> leafGood;
    [SerializeField] private List<Sprite> blossomGood;
    [Space]
    [SerializeField] private List<Sprite> vaseBad;
    [SerializeField] private List<Sprite> stemBad;
    [SerializeField] private List<Sprite> leafBad;
    [SerializeField] private List<Sprite> blossomBad;
    [Space]
    [SerializeField, Range(0f,1f)] private float chanceOfBadFlower;
    [SerializeField, Range(1,3)] private int maxBadFlowerParts;
    [SerializeField] private bool minigamesAllowed;
    [SerializeField, Range(0, 3)] private int maxMinigames;
    [SerializeField] GameObject tools;


    private void Awake()
    {
        instance = this;
        if(!minigamesAllowed)
            tools.SetActive(false);
    }

    public GameObject CreateFlower()
    {
        print("Started flower printing");
        bool[] badParts = new bool[5];
        if (CheckIfBad(Random.Range(0f, 1f)))
            badParts = DistributeBadParts(Random.Range(1,maxBadFlowerParts + 1));
        List<Sprite> flowerSprites = GetRandomFlowerSprites(badParts);
        GameObject newFlower = Instantiate(flowerPrefab, spawnPos, Quaternion.identity);
        newFlower.transform.localScale = spawnSize;
        newFlower.GetComponent<FlowerData>().GetFlowerData(flowerSprites, badParts);
        if (minigamesAllowed)
            TurnOnMinigames(SelectRandomMinigames(Random.Range(1, maxMinigames)));
        print("Flower was printed");
        FlowerCheck.instance.GetAnswers(badParts);
        return newFlower;
    }

    private bool[] SelectRandomMinigames(int amountOfGames)
    {
        bool[] games = new bool[3] {false,false,false};
        for(int i = 0; i< amountOfGames; i++)
        {
            int random = Random.Range(0,3);
            if (games[random])
            {
                i--;
                continue;
            }
            games[random] = true;
            FlowerData.instance.UnlockMinigame(random);
        }
        FlowerCheck.instance.SetMinigamesToBeBeaten(games);
        return games;
    }

    private void TurnOnMinigames(bool[] games)
    {
        for(int i = 0; i < games.Length; i++)
        {
            
        }
    }

    List<Sprite> GetRandomFlowerSprites(bool[] isBadPart)
    {
        List<Sprite> sprites = new List<Sprite>();
        if (isBadPart[0])
            sprites.Add(GetRandomSpriteFromList(ref vaseBad));
        else
            sprites.Add(GetRandomSpriteFromList(ref vaseGood));
        if (isBadPart[1])
            sprites.Add(GetRandomSpriteFromList(ref stemBad));
        else
            sprites.Add(GetRandomSpriteFromList(ref stemGood));
        if (isBadPart[2])
            sprites.Add(GetRandomSpriteFromList(ref leafBad));
        else
            sprites.Add(GetRandomSpriteFromList(ref leafGood));
        if (isBadPart[3])
            sprites.Add(GetRandomSpriteFromList(ref leafBad));
        else
            sprites.Add(GetRandomSpriteFromList(ref leafGood));
        if (isBadPart[4])
            sprites.Add(GetRandomSpriteFromList(ref blossomBad));
        else
            sprites.Add(GetRandomSpriteFromList(ref blossomGood));
        return sprites;
    }

    Sprite GetRandomSpriteFromList(ref List<Sprite> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    bool[] DistributeBadParts(int amount)
    {
        int badPartsInserted = 0;
        bool[] parts = new bool[5];
        while (badPartsInserted < amount)
        {
            int partToBeGottenBad = Random.Range(0,parts.Length);
            if (parts[partToBeGottenBad] == true)
                continue;
            parts[partToBeGottenBad] = true;
            badPartsInserted++;
        }
        return parts;
    }


    bool CheckIfBad(float randomNumber)
    {
        if (randomNumber < chanceOfBadFlower)
            return true;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnPos, 1f);
    }
}
