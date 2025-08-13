using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator pipe_a;
    [SerializeField] private float pipe_time;

    [SerializeField] private float flower_time;

    [SerializeField] private float conveyor_time;
    [SerializeField] private float conveyorEnd_time;

    public IEnumerator NewPlant()
    {
        yield return new WaitForSeconds(pipe_time);
        pipe_a.SetTrigger("Spawn");
        yield return new WaitForSeconds(flower_time);
        GameObject flower = FlowerCreation.instance.CreateFlower();
        yield return new WaitForSeconds(conveyor_time);
        ConveyorBeltAnimation.instance.StartBelt(1f);
        yield return new WaitForSeconds(conveyorEnd_time);
        ConveyorBeltAnimation.instance.StopBelt();
    }
}
