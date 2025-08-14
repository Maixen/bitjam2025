using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteMinigame : MonoBehaviour
{
    [SerializeField] private GameObject bugPrefab;
    [SerializeField] private ParticleSystem bugParticles;
    [Space]
    [SerializeField] private bool playable;
    [SerializeField] private bool gameHasStarted;
    [SerializeField] private Vector2 bugSpawnAmountRange;
    [SerializeField] private int bugSpawnCount;
    [SerializeField] private int bugsLeft;
    [SerializeField] private Vector2 bugSpawnTimeRange;
    [SerializeField] private float nextBugSpawnsIn;
    [SerializeField] private Vector2 xBugSpawnBoundaries;
    [SerializeField] private Vector2 yBugSpawnBoundaries;

    //<3
    private float RandomizeFromRange(Vector2 range)
    {
        return Random.Range(range.x, range.y);
    } //<3

    public void IsPlayable()
    {
        playable = true;
        bugParticles.gameObject.SetActive(true);
    }

    public void StartMinigame()
    {
        if (gameHasStarted || !playable)
            return;
        gameHasStarted = true;
        bugSpawnCount = (int)RandomizeFromRange(bugSpawnAmountRange);
        SpawnBug(new Vector2(RandomizeFromRange(xBugSpawnBoundaries), RandomizeFromRange(yBugSpawnBoundaries)));
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
        GameObject newBug = Instantiate(bugPrefab, (Vector2)transform.position + spawnPos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        newBug.GetComponent<BugBehaviour>().minigameParent = this;
        bugSpawnCount--;
        bugsLeft++;
        nextBugSpawnsIn = ResetBugSpawnTimer();
    }

    private void Win()
    {
        //Game is won, duh
        gameHasStarted = false;
        gameObject.SetActive(false);
        playable = false;
        bugParticles.gameObject.SetActive(false);
    }
    
}
