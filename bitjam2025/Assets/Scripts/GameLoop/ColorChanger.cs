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
    [SerializeField] private Material moneyMaterial;
    [SerializeField] private Material waterMaterial;
    [SerializeField] private Material bugMaterial;
    [SerializeField] private Material soulMaterial;
    [SerializeField] private Material backgroundMaterial;

    [SerializeField] private AudioSource hardSwap_audioSource;
    [SerializeField] private AudioSource lightSwap_audioSource;

    private void Awake()
    {
        instance = this;
        if (PlayerPrefs.GetInt("ChangeColor", 0) == 0)
            paletteChangeAllowed = false;
        else
            paletteChangeAllowed = true;
    }

    public void ChangePalette(int paletteIndex)
    {
        if (paletteIndex >= primaryColors.Count)
            return;
        currentPalette = paletteIndex;
        primaryMaterial.SetColor("_TargetColor", primaryColors[currentPalette]);
        moneyMaterial.SetColor("_Color", primaryColors[currentPalette]);
        waterMaterial.SetColor("_Color", primaryColors[currentPalette]);
        bugMaterial.SetColor("_Color", primaryColors[currentPalette]);
        soulMaterial.SetColor("_Color", primaryColors[currentPalette]);
        backgroundMaterial.SetColor("_TargetColor", backgroundColors[currentPalette]);

        hardSwap_audioSource.Play();
    }

    public void NextPalette()
    {
        if (!paletteChangeAllowed)
            return;
        if(++currentPalette >= primaryColors.Count)
            currentPalette = 0;
        primaryMaterial.SetColor("_TargetColor", primaryColors[currentPalette]);
        moneyMaterial.SetColor("_Color", primaryColors[currentPalette]);
        waterMaterial.SetColor("_Color", primaryColors[currentPalette]);
        bugMaterial.SetColor("_Color", primaryColors[currentPalette]);
        soulMaterial.SetColor("_Color", primaryColors[currentPalette]);
        backgroundMaterial.SetColor("_TargetColor", backgroundColors[currentPalette]);

        hardSwap_audioSource.Play();
    }

    public void SetMinigamePalette(int paletteindex, bool turnOn)
    {
        if (!paletteChangeAllowed)
            return;
        if (!turnOn)
        {
            ChangePalette(currentPalette);
            print("Works");
            return;
        }
        primaryMaterial.SetColor("_TargetColor", primaryMinigameColors[paletteindex]);
        moneyMaterial.SetColor("_Color", primaryMinigameColors[paletteindex]);
        waterMaterial.SetColor("_Color", primaryMinigameColors[paletteindex]);
        bugMaterial.SetColor("_Color", primaryMinigameColors[paletteindex]);
        soulMaterial.SetColor("_Color", primaryMinigameColors[paletteindex]);
        backgroundMaterial.SetColor("_TargetColor", backgroundMinigameColors[paletteindex]);

        lightSwap_audioSource.Play();
    }
}
