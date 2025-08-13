using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerData : MonoBehaviour
{
    [SerializeField] private SpriteRenderer vaseSprite;
    [SerializeField] private SpriteRenderer stemSprite;
    [SerializeField] private SpriteRenderer leafLSprite;
    [SerializeField] private SpriteRenderer leafRSprite;
    [SerializeField] private SpriteRenderer blossomSprite;

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
}
