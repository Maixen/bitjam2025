using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    static public ColorChanger instance;
    [SerializeField] private int currentPalette;
    [SerializeField] private bool paletteChangeAllowed;
    [Space]
    [SerializeField] private List<Color> primaryColors;
    [SerializeField] private List<Color> backgroundColors;
    [Space]
    [SerializeField] private List<Color> primaryMinigameColors;
    [SerializeField] private List<Color> backgroundMinigameColors;
    [Space]
    [SerializeField] private Material primaryMaterial;
    [SerializeField] private Material backgroundMaterial;

    private void Awake()
    {
        instance = this;
    }

    public void ChangePalette(int paletteIndex)
    {
        if (paletteIndex >= primaryColors.Count)
            return;
        currentPalette = paletteIndex;
        primaryMaterial.SetColor("_TargetColor", primaryColors[currentPalette]);
        backgroundMaterial.SetColor("_TargetColor", backgroundColors[currentPalette]);
    }

    public void NextPalette()
    {
        if (!paletteChangeAllowed)
            return;
        if(++currentPalette >= primaryColors.Count)
            currentPalette = 0;
        primaryMaterial.SetColor("_TargetColor", primaryColors[currentPalette]);
        backgroundMaterial.SetColor("_TargetColor", backgroundColors[currentPalette]);
    }

    public void SetMinigamePalette(int paletteindex)
    {
        if (!paletteChangeAllowed)
            return;
        primaryMaterial.SetColor("_TargetColor", primaryMinigameColors[paletteindex]);
        backgroundMaterial.SetColor("_TargetColor", backgroundMinigameColors[paletteindex]);
    }
}
