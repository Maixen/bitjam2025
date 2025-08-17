using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteMinigame : MonoBehaviour
{
    [SerializeField] private GameObject bugPrefab;
    [SerializeField] private ParticleSystem bugParticles;
    [Space]
    [SerializeField] private List<Sprite> bugSprites;
    [SerializeField] private bool playable;
    [SerializeField] private bool gameHasStarted;
    [SerializeField] private Vector2 bugSpawnAmountRange;
    [SerializeField] private int bugSpawnCount;
    [SerializeField] private int bugsLeft;
    [SerializeField] private Vector2 bugSpawnTimeRange;
    [SerializeField] private float nextBugSpawnsIn;
    [SerializeField] private Vector2 bugBaseSpawnPos;
    [SerializeField] private Vector2 xBugSpawnBoundaries;
    [SerializeField] private Vector2 yBugSpawnBoundaries;
    [SerializeField] private GameObject border;
    private Animator plantAnimator;
    //<3
    private float RandomizeFromRange(Vector2 range)
    {
        return Random.Range(range.x, range.y);
    } //<3

    public void IsPlayable()
    {
        playable = true;
        bugParticles.gameObject.SetActive(true);
        plantAnimator = FlowerData.instance.gameObject.GetComponent<Animator>();
    }

    public void StartMinigame()
    {
        if (gameHasStarted || !playable)
            return;
        GameManager.Instance.MinigameWasToggled();
        gameHasStarted = true;
        bugSpawnCount = (int)RandomizeFromRange(bugSpawnAmountRange);
        SpawnBug(new Vector2(RandomizeFromRange(xBugSpawnBoundaries), RandomizeFromRange(yBugSpawnBoundaries)));
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        plantAnimator.SetTrigger("Bugs");
        border.SetActive(true);
    }

    private void Update()
    {
        if (bugSpawnCount == 0)
            return;
        nextBugSpawnsIn -= Time.deltaTime;
        if (nextBugSpawnsIn > 0)
            return;
        SpawnBug(new Vector2(RandomizeFromRange(xBugSpawnBoundaries), RandomizeFromRange(yBugSpawnBoundaries)));
    }

    public void BugWasSquashed()
    {
        if (--bugsLeft == 0 && bugSpawnCount == 0)
            Win();
    }

    private float ResetBugSpawnTimer()
    {
        return RandomizeFromRange(bugSpawnTimeRange);
    }

    void SpawnBug(Vector2 spawnPos)
    {
        GameObject newBug = Instantiate(bugPrefab, (Vector2)bugBaseSpawnPos + spawnPos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        newBug.GetComponent<SpriteRenderer>().sprite = bugSprites[Random.Range(0, bugSprites.Count)];
        newBug.GetComponent<BugBehaviour>().minigameParent = this;
        bugSpawnCount--;
        bugsLeft++;
        nextBugSpawnsIn = ResetBugSpawnTimer();
    }

    private void Win()
    {
        GameManager.Instance.AddMoney(10);
        GameManager.Instance.MinigameWasToggled();
        gameHasStarted = false;
        //gameObject.SetActive(false);
        playable = false;
        bugParticles.gameObject.SetActive(false);
        plantAnimator.SetTrigger("End");
        FlowerCheck.instance.MinigameWasBeaten(1);
        FlowerCreation.instance.MinigameButtonStopWiggle(1);
        border.SetActive(false);
    }
    
}
