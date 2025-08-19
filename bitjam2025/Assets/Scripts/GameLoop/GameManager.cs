using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Animator pipe_a;
    [SerializeField] private AudioSource pipe_audioSource;
    [SerializeField] private AudioSource pipe_audioSource2;
    [SerializeField] private float pipe_time;
    [Space]
    [SerializeField] private float flower_time;
    [SerializeField] private AudioSource plant_audioSource;
    [SerializeField] private ParticleSystem plant_ps;
    [Space]
    [SerializeField] private Animator conveyor_a;
    [SerializeField] private AudioSource conveyor_audioSource;
    [SerializeField] private Animator checklist_a;
    [SerializeField] private AudioSource checklist_audioSource;
    [SerializeField] private AudioSource checklist_audioSource2;
    [SerializeField] private float conveyor_time;
    [SerializeField] private float conveyor_speed;
    [Space]
    [SerializeField] private float conveyorLast_time;
    [SerializeField] private float conveyorBack_time;
    [Space]
    [SerializeField] private float cooldown_time;
    [Space]
    [SerializeField] private ParticleSystem create_ps;
    [SerializeField] private float create_offset;
    [SerializeField] private ParticleSystem sell_ps;
    [SerializeField] private AudioSource sell_audioSource;
    [SerializeField] private float sell_offset;
    [SerializeField] private ParticleSystem dump_ps;
    [SerializeField] private float dump_offset;
    [Space]
    [SerializeField] private List<SetMinigameView> minigameButtons;
    [Space]
    private GameObject flower;
    [SerializeField] private bool minigameIsPlayed;
    [SerializeField] private MoneyDisplayScript moneyOwnedDiplay;
    public bool gameIsDone;
    [SerializeField] private Animator wallAnimator;
    [SerializeField] private EndScreenBehaviour endScreen;
    [SerializeField] private bool isLevelWithFollowUp;
    [SerializeField, Range(0, 1f)] private float[] tutBadFlowerChances;
    [SerializeField, Range(1, 3)] private int[] tutBadFlowerParts;
    [SerializeField, Range(1, 3f)] private int[] tutMaxMinigames;
    [SerializeField, Range(30,200)] private float[] tutRoundTimes;
    [SerializeField] private GameObject tutorialEndScenePlant;
    [SerializeField] private bool tutorialOn;

    [SerializeField] private Animator camera_a;
    [SerializeField] private GameObject ownFlower;

    [SerializeField] private AudioSource breakThePlant_audioSource;


    private void Awake()
    {
        Instance = this;
        gameIsDone = false;
    }

    private void Start()
    {
        wallAnimator.SetTrigger("Start");
        Timer.instance.TogglePause();
        if (Timer.instance.isEndless)
        {
            Timer.instance.PauseOff();
            StartCoroutine(NewPlant());
            return;
        }
        Invoke(nameof(TutorialInvokeAtStart), 1);
    }

    private void TutorialInvokeAtStart()
    {
        StartNewTutorial(0);
    }

    public void StartNewTutorial(int requiredScene)
    {
        checklist_a.SetTrigger("Hide");
        Timer.instance.TogglePause();
        tutorialOn = true;
        switch (requiredScene)
        {
            case 0:
                TutorialManager.instance.StartTutorial(TutorialManager.instance.tutorialMain);
                TutorialManager.instance.tutorialEnd += StartEndResolver;
                TutorialManager.instance.tutorialChanged += TutorialResolver;
                break;
            case 1:
                FlowerCreation.instance.minigamesAllowed = true;
                TutorialManager.instance.StartTutorial(TutorialManager.instance.tutorialExtraItems);
                TutorialManager.instance.tutorialEnd += DialougeEndResolver;
                TutorialManager.instance.tutorialChanged += TutorialResolver;
                break;
            case 2:
                TutorialManager.instance.StartTutorial(TutorialManager.instance.tutorial3);
                TutorialManager.instance.tutorialEnd += DialougeEndResolver;
                TutorialManager.instance.tutorialChanged += TutorialResolver;
                break;
            case 3:
                TutorialManager.instance.StartTutorial(TutorialManager.instance.tutorial4);
                TutorialManager.instance.tutorialEnd += DialougeEndResolver;
                TutorialManager.instance.tutorialChanged += TutorialResolver;
                break;
            case 4:
                TutorialManager.instance.StartTutorial(TutorialManager.instance.tutorial5);
                TutorialManager.instance.tutorialEnd += DialougeEndResolver;
                TutorialManager.instance.tutorialChanged += TutorialResolver;
                break;
            case 5:
                TutorialManager.instance.StartTutorial(TutorialManager.instance.tutorialEndScene);
                TutorialManager.instance.tutorialEnd += StoryModeEndResolver;
                TutorialManager.instance.tutorialChanged += TutorialResolver;
                break;
        }
    }

    public void StartEndResolver()
    {
        StartCoroutine(NewPlant());
        tutorialOn = false;
        if(!minigameIsPlayed)
        {
            checklist_a.ResetTrigger("Hide");
            checklist_a.SetTrigger("Show");
            checklist_a.ResetTrigger("Show");
            checklist_a.SetTrigger("Show");
        }
    }

    public void DialougeEndResolver()
    {
        checklist_a.SetTrigger("Hide");
        int round = Timer.instance.GetRoundNum();
        tutorialOn = false;
        FlowerCreation.instance.SetupFlowerCreation(tutBadFlowerChances[round], tutBadFlowerParts[round], tutMaxMinigames[round]);
        Timer.instance.ChangeRoundTime(tutRoundTimes[round]);
        Timer.instance.PauseOff();
        checklist_a.ResetTrigger("Hide");
        checklist_a.SetTrigger("Show");
        checklist_a.ResetTrigger("Show");
        checklist_a.SetTrigger("Show");
        MoneyControl.Instance.ResetPlayerMoney();
    }

    public void StoryModeEndResolver()
    {
        checklist_a.SetTrigger("Hide");
        tutorialOn = false;
        FlowerData.instance?.gameObject.SetActive(false);
        FlowerCreation.instance.minigamesAllowed = false;
        gameIsDone = true;
        camera_a.SetTrigger("Zoom");
        ownFlower.SetActive(true);
        Invoke(nameof(ParticleAtFlower), 0.5f);
        Invoke(nameof(GoodEnding), 1.83333333f);
    }

    private void GoodEnding()
    {
        camera_a.SetTrigger("Zoom");
        ParticleAtFlower();
        InitiateGameEnd(true);
    }

    private void ParticleAtFlower()
    {
        dump_ps.Play();
    }

    public void TutorialResolver()
    {
        TutorialManager.instance.tutorialEnd -= StartEndResolver;
        TutorialManager.instance.tutorialChanged -= TutorialResolver;
    }

    public IEnumerator NewPlant()
    {
        yield return new WaitForSeconds(pipe_time);
        pipe_a.SetTrigger("Spawn");
        pipe_audioSource.Play();
        Invoke(nameof(CreatePS), create_offset);
        yield return new WaitForSeconds(flower_time);
        flower = FlowerCreation.instance.CreateFlower();
        flower.GetComponent<Animator>().SetTrigger("Drop");
        plant_audioSource.PlayDelayed(0.1f);
        pipe_audioSource2.Play();
        yield return new WaitForSeconds(0.1f);
        plant_ps.Play();
        yield return new WaitForSeconds(Mathf.Abs(conveyor_time - 0.1f)); // Makes sure its positive :) we never know what Blubbi might do
        FlowerCheck.instance.FlowerNowExists();
        for (int i = 0; i < minigameButtons.Count; i++)
            minigameButtons[i].ResetButton();
        ColorChanger.instance.NextPalette();
        conveyor_a.SetTrigger("First");
        conveyor_audioSource.Play();
        if(!tutorialOn)
        {
            checklist_a.SetTrigger("Swap");
            checklist_audioSource.Play();
            checklist_audioSource.PlayDelayed(0.2f);
        }
    }

    public IEnumerator GetRidOfPlant(bool plantWasSold, bool correct)
    {
        if (plantWasSold)
        {
            flower.GetComponent<Animator>().SetTrigger("Sell");
            if (correct)
                Invoke(nameof(SellPS), sell_offset);
            yield return new WaitForSeconds(conveyorLast_time);
            conveyor_a.SetTrigger("Last");
            conveyor_audioSource.Play();
        }
        else
        {
            flower.GetComponent<Animator>().SetTrigger("Dump");
            Invoke(nameof(DumpPS), dump_offset);
            yield return new WaitForSeconds(conveyorBack_time);
            conveyor_a.SetTrigger("Back");
            conveyor_audioSource.Play();
        }
        yield return new WaitForSeconds(cooldown_time);
        Destroy(flower);
        flower = null;
        if(!gameIsDone)
            StartCoroutine(NewPlant());
    }

    private void CreatePS()
    {
        create_ps.Play();
    }

    private void SellPS()
    {
        sell_ps.Play();
        sell_audioSource.Play();
    }

    private void DumpPS()
    {
        dump_ps.Play();
        breakThePlant_audioSource.Play();
        plant_audioSource.Play();
    }

    public bool IsMinigamePlayed()
    {
        return minigameIsPlayed;
    }

    public void MinigameWasToggled()
    {
        minigameIsPlayed = !minigameIsPlayed;
    }

    public ref List<SetMinigameView> GetMinigameButtons()
    {
        return ref minigameButtons;
    }

    public void AddMoney(int moneyAdded)
    {
        moneyOwnedDiplay.ChangeValueBy(moneyAdded);
    }

    public void InitiateGameEnd(bool wasWon)
    {
        gameIsDone = true;
        wallAnimator.SetTrigger("GameEnd");
        endScreen.SetupEndScreen(wasWon,isLevelWithFollowUp,
            new int[] {Timer.instance.GetRoundNum(),FlowerCheck.instance.GetFlowersWrong(),MoneyControl.Instance.GetBestStreak(),MoneyControl.Instance.GetMoneyEarnedInTotal()});
    }
}
