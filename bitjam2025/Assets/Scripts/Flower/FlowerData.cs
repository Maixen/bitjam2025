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
        var script = waterMinigame.gameObject;
        if(id == 0)
             script = waterMinigame.gameObject;
        if(id == 1)
            script = parasiteMinigame.gameObject;
        if (script.activeSelf)
            script.SetActive(false);
        else 
            script.SetActive(true);
    }

    public void UnlockMinigame(int id)
    {
        var script = waterMinigame;
        if (id == 0)
            waterMinigame.IsPlayable();
        if (id == 1)
            // später mehr
            return;
    }
}
