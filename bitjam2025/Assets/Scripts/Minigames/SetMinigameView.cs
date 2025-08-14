using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMinigameView : MonoBehaviour
{
    enum Minigames
    {
        Water = 0,
        Bugs = 1,
    }
    [SerializeField] private Minigames minigameType;
    [SerializeField] private bool isActive;

    public void ToggleMinigame()
    {
        isActive = !isActive;
        FlowerData.instance?.ActivateMinigame((int)minigameType);
        ColorChanger.instance.SetMinigamePalette((int)minigameType, isActive);
    }

}
