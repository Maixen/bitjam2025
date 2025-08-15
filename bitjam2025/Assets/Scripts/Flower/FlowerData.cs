using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerData : MonoBehaviour
{
    public static FlowerData instance;
    [SerializeField] private SpriteRenderer vaseSprite;
    [SerializeField] private SpriteRenderer stemSprite;
    [SerializeField] private SpriteRenderer leafLSprite;
    [SerializeField] private SpriteRenderer leafRSprite;
    [SerializeField] private SpriteRenderer blossomSprite;
    [SerializeField] private WaterMinigame waterMinigame;
    [SerializeField] private ParasiteMinigame parasiteMinigame;
    [SerializeField] private SoulMinigame soulMinigame;

    private void Awake()
    {
        instance = this;
    }

    public void GetFlowerData(List<Sprite> flowerSprites, bool[] parts)
    {
        SetFlowerPartSprite(ref vaseSprite, flowerSprites[0]);
        SetFlowerPartSprite(ref stemSprite, flowerSprites[1]);
        SetFlowerPartSprite(ref leafLSprite, flowerSprites[2]);
        SetFlowerPartSprite(ref leafRSprite, flowerSprites[3]);
        SetFlowerPartSprite(ref blossomSprite, flowerSprites[4]);
    }

    private void SetFlowerPartSprite(ref SpriteRenderer sR, Sprite newSprite)
    {
        sR.sprite = newSprite;
    }

    public void ActivateMinigame(int id)
    {
        if(id == 0)
            waterMinigame.gameObject.SetActive(!waterMinigame.gameObject.activeSelf);
        if (id == 1)
            parasiteMinigame.gameObject.SetActive(!parasiteMinigame.gameObject.activeSelf);
        if (id == 2)
            soulMinigame.gameObject.SetActive(!soulMinigame.gameObject.activeSelf);
    }

    public void UnlockMinigame(int id)
    {
        if (id == 0)
            waterMinigame.IsPlayable();
        if (id == 1)
            parasiteMinigame.IsPlayable();
        if (id == 2)
            soulMinigame.IsPlayable();
            return;
    }
}
