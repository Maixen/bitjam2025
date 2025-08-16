using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    [SerializeField] private float timeToGoOn;
    [SerializeField] private float timeToStartTutorial;

    [SerializeField] private GameObject tutorialDisplay;
    [SerializeField] private SpriteRenderer textDisplay;
    [SerializeField] private Animator text_a;

    public List<Sprite> tutorialMain = new List<Sprite>();
    public List<Sprite> tutorialExtraItems = new List<Sprite>();
    public List<Sprite> tutorialEndLoose = new List<Sprite>();
    public List<Sprite> tutorialEndWin = new List<Sprite>();
    public List<Sprite> tutorialMinigameWater = new List<Sprite>();
    public List<Sprite> tutorialMinigameBug = new List<Sprite>();
    public List<Sprite> tutorialMinigameSoul = new List<Sprite>();

    [SerializeField] private float timeToSwapSpriteInAnimation;
    [SerializeField] private float timeToSwapSpriteDelayInAnimation;

    private List<Sprite> currentTutorial;
    private int currentIndex;
    private float time;

    private bool inAnimation;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Invoke(nameof(StartMainTutorialResolver), timeToStartTutorial);
    }

    private void StartMainTutorialResolver()
    {
        StartTutorial(tutorialMain);
    }

    public void StartTutorial(List<Sprite> tutorial)
    {
        tutorialDisplay.SetActive(true);
        textDisplay.sprite = tutorial[0]; // First Sprite
        currentTutorial = tutorial;
        currentIndex = 0;
        time = 0f;
        inAnimation = false;
    }

    private void Update()
    {
        if (inAnimation) { return; }
        if (currentTutorial == null || currentIndex >= currentTutorial.Count) { tutorialDisplay.SetActive(false); return; }
        time += Time.unscaledDeltaTime;

        if (time > timeToGoOn || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            time = 0f;
            currentIndex++;
            if (currentIndex >= currentTutorial.Count)
            {
                currentTutorial = null;
                tutorialDisplay.SetActive(false);
            }
            else
            {
                inAnimation = true;
                StartCoroutine(Swap());
            }
        }
    }

    private IEnumerator Swap()
    {
        text_a.SetTrigger("Swap");
        yield return new WaitForSeconds(timeToSwapSpriteInAnimation);
        textDisplay.sprite = currentTutorial[currentIndex];
        yield return new WaitForSeconds(timeToSwapSpriteInAnimation);
        inAnimation = false;
    }
}
