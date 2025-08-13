using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoxInteract : MonoBehaviour
{
    [SerializeField] int checklistIndex;
    [SerializeField] private bool checked_Off = false;
    [SerializeField] private GameObject checkIcon;

    private void Start()
    {
        checkIcon.SetActive(checked_Off);
    }

    public void WasClicked()
    {
        checked_Off = !checked_Off;
        checkIcon.SetActive(checked_Off);
    }

    public bool GetValue()
    {
        return checked_Off;
    }

    public int GetIndex()
    {
        return checklistIndex;
    }

    public void ResetValue()
    {
        checked_Off = false;
        checkIcon.SetActive(checked_Off);
    }
}
