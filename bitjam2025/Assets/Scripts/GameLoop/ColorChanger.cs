using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private int currentPalette;
    [SerializeField] private bool paletteChangeAllowed;
    [Space]
    [SerializeField] private List<Color> primaryColors;
    [SerializeField] private List<Color> backgroundColors;
    [Space]
    [SerializeField] private Material primaryMaterial;
    [SerializeField] private Material backgroundMaterial;

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
}
