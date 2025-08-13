using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator pipe_a;
    [SerializeField] private float pipe_time;

    [SerializeField] private float flower_time;

    [SerializeField] private Animator conveyor_a;
    [SerializeField] private Animator checklist_a;
    [SerializeField] private float conveyor_time;
    [SerializeField] private float conveyor_speed;


    private void Start()
    {
        StartCoroutine(NewPlant());
    }

    public IEnumerator NewPlant()
    {
        yield return new WaitForSeconds(pipe_time);
        pipe_a.SetTrigger("Spawn");
        yield return new WaitForSeconds(flower_time);
        GameObject flower = FlowerCreation.instance.CreateFlower();
        flower.GetComponent<Animator>().SetTrigger("Drop");
        yield return new WaitForSeconds(conveyor_time);
        conveyor_a.SetTrigger("First");
        checklist_a.SetTrigger("Swap");
    }
}
