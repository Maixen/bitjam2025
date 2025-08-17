using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoHover : MonoBehaviour
{
    [SerializeField] private GameObject infoScreen;

    private void Start()
    {
        infoScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            infoScreen.SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        infoScreen.SetActive(true);
    }

    private void OnMouseExit()
    {
        infoScreen.SetActive(false);
    }
}
