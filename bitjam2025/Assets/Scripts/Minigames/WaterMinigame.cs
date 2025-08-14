using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMinigame : MonoBehaviour
{
    [SerializeField] private bool playable;
    [SerializeField] private bool gameHasStarted;
    [SerializeField] private Vector3 sliderStartPos;
    [SerializeField] private float sliderTargetYPos;
    [SerializeField] private float sliderMinYPos;
    [SerializeField] private float sliderMaxYPos;
    [SerializeField] private Vector3 waterStartPos;
    [SerializeField] private Vector3 waterEndPos;
    [SerializeField] private Vector3 waterStartFill;
    [SerializeField] private Vector3 waterEndFill;
    [SerializeField] private float fillPercentPerSecondBase;
    [SerializeField] private float maxFillSpeedOffset;
    private float fillPercentPerSecond;
    [SerializeField] private float percentToFill;
    [SerializeField] private float currentFillPercent;
    [SerializeField, Range(0,5)] private float maxPercentOffset;
    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject waterMeter;
    [SerializeField] private ParticleSystem thirstParticles;

    public void Start()
    {
        thirstParticles.gameObject.SetActive(false);
    }
    public void StartMinigame()
    {
        if (gameHasStarted || !playable)
            return;
        gameHasStarted = true;
        slider.transform.localPosition = sliderStartPos;
        percentToFill = Random.Range(0.5f, 0.9f);
        currentFillPercent = 0;
        sliderTargetYPos = sliderMinYPos + (sliderMaxYPos - sliderMinYPos) * percentToFill;
        slider.transform.localPosition = sliderStartPos - Vector3.up * sliderStartPos.y + Vector3.up * sliderTargetYPos;
        water.transform.localPosition = waterStartPos;
        water.transform.localScale = waterStartFill;
        waterMeter.SetActive(true);
        fillPercentPerSecond = fillPercentPerSecondBase * Random.Range(fillPercentPerSecondBase / (maxFillSpeedOffset * 100 + 1), fillPercentPerSecondBase * (maxFillSpeedOffset * 100 + 1));
    }

    public void IsPlayable()
    {
        playable = true;
        thirstParticles.gameObject.SetActive(playable);
    }
    private void CheckResult()
    {
        if(Mathf.Abs(percentToFill - currentFillPercent) < maxPercentOffset)
        {
            //Game is won
            print("Won");
            gameHasStarted = false;
            gameObject.SetActive(false);
            waterMeter.SetActive(false);
            thirstParticles.gameObject.SetActive(false);
            playable = false;
            return;
        }
        return;
    }

    private void Update()
    {
        if (!gameHasStarted)
            return;
        print("game is running");
        float percentGained = fillPercentPerSecond * 0.01f * Time.deltaTime;
        currentFillPercent += percentGained;
        float waterScalesBy = (waterEndPos.y - waterStartPos.y) * percentGained;
        water.transform.localScale += Vector3.up * waterScalesBy;
        water.transform.localPosition += Vector3.up * 0.5f * waterScalesBy;
        if (currentFillPercent >= 1)
        {
            currentFillPercent -= 1;
            water.transform.localPosition = waterStartPos;
            water.transform.localScale = waterStartFill;
        }
            
        if (Input.GetMouseButtonDown(0))
           CheckResult();
    }
}
