using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float animatorTurnOffTime;

    private bool onMainMenu;

    private void Update()
    {
        if (onMainMenu)
        {
            // Menu Logic
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                onMainMenu = true;
                animator.SetTrigger("Start");
                Invoke(nameof(TurnOffAnimator), animatorTurnOffTime);
            }
        }
    }

    private void TurnOffAnimator()
    {
        animator.enabled = false;
    }
}
