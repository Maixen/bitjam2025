using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Animator pipe_a;
    [SerializeField] private float pipe_time;
    [Space]
    [SerializeField] private float flower_time;
    [Space]
    [SerializeField] private Animator conveyor_a;
    [SerializeField] private Animator checklist_a;
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
    [SerializeField] private float sell_offset;
    [SerializeField] private ParticleSystem dump_ps;
    [SerializeField] private float dump_offset;
    [Space]
    [SerializeField] private List<SetMinigameView> minigameButtons;
    [Space]
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
        Invoke(nameof(CreatePS), create_offset);
        yield return new WaitForSeconds(flower_time);
        flower = FlowerCreation.instance.CreateFlower();
        flower.GetComponent<Animator>().SetTrigger("Drop");
        yield return new WaitForSeconds(conveyor_time);
        FlowerCheck.instance.FlowerNowExists();
        for (int i = 0; i < minigameButtons.Count; i++)
            minigameButtons[i].ResetButton();
        ColorChanger.instance.NextPalette();
        conveyor_a.SetTrigger("First");
        checklist_a.SetTrigger("Swap");
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
        }
        else
        {
            flower.GetComponent<Animator>().SetTrigger("Dump");
            Invoke(nameof(DumpPS), dump_offset);
            yield return new WaitForSeconds(conveyorBack_time);
            conveyor_a.SetTrigger("Back");
        }
        yield return new WaitForSeconds(cooldown_time);
        Destroy(flower);
        flower = null;
        StartCoroutine(NewPlant());
    }

    private void CreatePS()
    {
        create_ps.Play();
    }

    private void SellPS()
    {
        sell_ps.Play();
    }

    private void DumpPS()
    {
        dump_ps.Play();
    }
}
