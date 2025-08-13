using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        instance = this;
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
        print("Flower was printed");
        FlowerCheck.instance.FlowerNowExists();
        FlowerCheck.instance.GetAnswers(badParts);
        return newFlower;
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
