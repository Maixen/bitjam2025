using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Animator pipe_a;
    [SerializeField] private float pipe_time;

    [SerializeField] private float flower_time;

    [SerializeField] private Animator conveyor_a;
    [SerializeField] private Animator checklist_a;
    [SerializeField] private float conveyor_time;
    [SerializeField] private float conveyor_speed;

    [SerializeField] private float conveyorLast_time;
    [SerializeField] private float conveyorBack_time;

    [SerializeField] private float cooldown_time;
    private GameObject flower;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(NewPlant());
    }

    public IEnumerator NewPlant()
    {
        yield return new WaitForSeconds(pipe_time);
        pipe_a.SetTrigger("Spawn");
        yield return new WaitForSeconds(flower_time);
        flower = FlowerCreation.instance.CreateFlower();
        flower.GetComponent<Animator>().SetTrigger("Drop");
        yield return new WaitForSeconds(conveyor_time);
        conveyor_a.SetTrigger("First");
        checklist_a.SetTrigger("Swap");
    }

    public IEnumerator GetRidOfPlant(bool plantWasSold)
    {
        if (plantWasSold)
        {
            flower.GetComponent<Animator>().SetTrigger("Sell");
            yield return new WaitForSeconds(conveyorLast_time);
            conveyor_a.SetTrigger("Last");
        }
        else
        {
            flower.GetComponent<Animator>().SetTrigger("Dump");
            yield return new WaitForSeconds(conveyorBack_time);
            conveyor_a.SetTrigger("Back");
        }
        yield return new WaitForSeconds(cooldown_time);
        Destroy(flower);
        flower = null;
        StartCoroutine(NewPlant());
    }
}
