using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    [HideInInspector] public UnityAction tutorialEnd;
    [HideInInspector] public UnityAction tutorialChanged;

    [SerializeField] private float timeToGoOn;
    [SerializeField] private float timeToStartTutorial;

    [SerializeField] private GameObject tutorialDisplay;
    [SerializeField] private SpriteRenderer textDisplay;
    [SerializeField] private Animator text_a;

    public List<Sprite> tutorialMain = new List<Sprite>();
    public List<Sprite> tutorialExtraItems = new List<Sprite>();
    public List<Sprite> tutorial3 = new List<Sprite>();
    public List<Sprite> tutorial4 = new List<Sprite>();
    public List<Sprite> tutorial5 = new List<Sprite>();
    public List<Sprite> tutorialEndScene = new List<Sprite>();

    [SerializeField] private float timeToSwapSpriteInAnimation;
    [SerializeField] private float timeToSwapSpriteDelayInAnimation;

    [SerializeField] private AudioSource frog_audioSource;
    [SerializeField] private Vector2 minMaxFrogAudioPitchVariation;

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
        //Invoke(nameof(StartMainTutorialResolver), timeToStartTutorial);
    }

    private void StartMainTutorialResolver()
    {
        StartTutorial(tutorialMain);
    }

    public void StartTutorial(List<Sprite> tutorial)
    {
        if (tutorialChanged != null)
            tutorialChanged.Invoke();
        tutorialDisplay.SetActive(true);
        textDisplay.sprite = tutorial[0]; // First Sprite
        PlayAudio();
        currentTutorial = tutorial;
        currentIndex = 0;
        time = 0f;
        inAnimation = false;
        Timer.instance?.TogglePause();
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
                if (tutorialEnd != null)
                    tutorialEnd.Invoke();
                if (tutorialChanged != null)
                    tutorialChanged.Invoke();
                currentTutorial = null;
                Timer.instance?.TogglePause();
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
        PlayAudio();
        yield return new WaitForSeconds(timeToSwapSpriteInAnimation);
        inAnimation = false;
    }

    private void PlayAudio()
    {
        float randomPitch = Random.Range(minMaxFrogAudioPitchVariation.x, minMaxFrogAudioPitchVariation.y);
        frog_audioSource.pitch = randomPitch;
        frog_audioSource.Play();
    }
}
