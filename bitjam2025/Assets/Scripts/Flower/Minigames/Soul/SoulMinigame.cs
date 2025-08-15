using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SoulMinigame : MonoBehaviour
{
    [SerializeField] private GameObject pelletPrefab;
    [SerializeField] private GameObject scissorPrefab;
    private GameObject scissor;
    [SerializeField] private ParticleSystem sweatParticles;
    [Space]
    [SerializeField] private bool playable;
    [SerializeField] private bool gameHasStarted;
    [Space]
    [SerializeField] private Vector2 scissorSpawnPos;
    [SerializeField] private Vector2 pelletsSpawnAmountRange;
    [SerializeField] private int pelletsLeftToSpawn;
    [SerializeField] private int pelletsAlive;
    [SerializeField] private Vector2 pelletsSpawnTimeRange;
    [SerializeField] private float timeUntilNextPellet;
    [SerializeField] private float pelletsSpawnRadius;
    [SerializeField] private Vector2 pelletsSpeedRange;
    [SerializeField] private List<Vector2> pelletspawnPlaces;
    private Animator plantAnimator;



    //<3
    private float RandomizeFromRange(Vector2 range)
    {
        return Random.Range(range.x, range.y);
    } //<3

    public void IsPlayable()
    {
        playable = true;
        sweatParticles.gameObject.SetActive(true);
        plantAnimator = FlowerData.instance.gameObject.GetComponent<Animator>();
}

    public void StartMinigame()
    {
        if (gameHasStarted || !playable)
            return;
        GameManager.Instance.MinigameWasToggled();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        timeUntilNextPellet = ResetSpawnTime();
        pelletsLeftToSpawn = (int)RandomizeFromRange(pelletsSpawnAmountRange);
        scissor = Instantiate(scissorPrefab,scissorSpawnPos,Quaternion.identity);
        gameHasStarted = true;
        plantAnimator.SetTrigger("Soul");
    }

    private void Update()
    {
        if (!gameHasStarted)
            return;
        if (timeUntilNextPellet > 0)
        {
            timeUntilNextPellet -= Time.deltaTime;
            return;
        }
        if (pelletsLeftToSpawn < 1)
            return;
        SpawnPellet();
    }

    private void Win()
    {
        //Game is won, duh
        gameHasStarted = false;
        GameManager.Instance.AddMoney(10);
        GameManager.Instance.MinigameWasToggled();
        gameObject.SetActive(false);
        playable = false;
        sweatParticles.gameObject.SetActive(false);
        plantAnimator.SetTrigger("End");
        Destroy(scissor);
    }

    private float ResetSpawnTime()
    {
        return RandomizeFromRange(pelletsSpawnTimeRange);
    }

    private void SpawnPellet()
    {
        GameObject newPellet = Instantiate(pelletPrefab, GetPelletSpawnPos((int)RandomizeFromRange(new Vector2(0,pelletspawnPlaces.Count -1))), Quaternion.identity);
        newPellet.GetComponent<PelletMove>().SetupPellet(this, scissorSpawnPos, RandomizeFromRange(pelletsSpeedRange));
        pelletsLeftToSpawn--;
        pelletsAlive++;
        timeUntilNextPellet = ResetSpawnTime();
    }

    private Vector2 GetPelletSpawnPos(int randomNumber)
    {
        return (pelletspawnPlaces[randomNumber] * pelletsSpawnRadius) + scissorSpawnPos;
    }

    public void PelletWasMissed()
    {
        print("Missed a Pellet");
        pelletsLeftToSpawn++;
        pelletsAlive--;
    }

    public void PelletWasCaught()
    {
        print("Caught a Pellet");
        pelletsAlive--;
        scissor.GetComponent<Animator>().SetTrigger("Cut");
        if (pelletsAlive == 0 && pelletsLeftToSpawn == 0)
            Win();
    }
}
