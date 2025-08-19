using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetMinigameView : MonoBehaviour
{
    enum Minigames
    {
        Water = 0,
        Bugs = 1,
        Soul = 2,
    }
    [SerializeField] private Minigames minigameType;
    [SerializeField] private bool isActive;
    [SerializeField] Animator checkListAnimator;
    [SerializeField] private Vector3 basePos;
    [SerializeField] private float maxWiggle;
    [SerializeField] private bool mayWiggle;

    [SerializeField] private GameObject clickFlowerParticles;

    private Vector2 targetSize;
    private Vector2 defaultSize;

    private void Start()
    {
        basePos = transform.position;
        clickFlowerParticles.SetActive(false);
        defaultSize = transform.localScale;
        targetSize = defaultSize;
    }

    public void ToggleMinigame()
    {
        if (FlowerData.instance == null)
            return;
        if (GameManager.Instance.IsMinigamePlayed())
            return;
        ResetOtherButtons(ref GameManager.Instance.GetMinigameButtons());
        isActive = !isActive;
        if (isActive)
        {
            checkListAnimator.SetTrigger("Hide");
            clickFlowerParticles.SetActive(true);
            targetSize = defaultSize * 1.5f;
        }
        else
        {
            checkListAnimator.SetTrigger("Show");
            checkListAnimator.ResetTrigger("Hide");
            clickFlowerParticles.SetActive(false);
            targetSize = defaultSize;
        }
        FlowerData.instance?.ActivateMinigame((int)minigameType);
        ColorChanger.instance.SetMinigamePalette((int)minigameType, isActive);
    }

    public void ResetButton()
    {
        if(isActive)
            FlowerData.instance?.ActivateMinigame((int)minigameType);
        isActive = false;
        ColorChanger.instance.SetMinigamePalette((int)minigameType, isActive);
    }

    public int GetButtonState()
    {
        return (int)minigameType;
    }

    private void ResetOtherButtons(ref List<SetMinigameView> minigameButtons)
    {
        for (int i = 0; i < minigameButtons.Count; ++i)
        {
            if (minigameButtons[i].GetButtonState() != (int)minigameType)
                minigameButtons[i].ResetButton();
        }
    }

    public void AllowWiggle()
    {
        mayWiggle = true;
    }

    public void StopWiggle()
    {
        mayWiggle = false;
    }

    private void Update()
    {
        transform.localScale = Vector2.Lerp(transform.localScale, targetSize, Time.unscaledDeltaTime * 10f);

        if (!mayWiggle) { clickFlowerParticles.SetActive(false); return; }
        transform.position = basePos + maxWiggle * Random.onUnitSphere;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

}
