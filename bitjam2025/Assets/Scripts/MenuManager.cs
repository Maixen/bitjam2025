using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] private Animator animator;
    [SerializeField] private float animatorTurnOffTime;

    [SerializeField] private string normalSceneName;
    [SerializeField] private string endlessSceneName;

    [SerializeField] private GameObject options;
    [SerializeField] private GameObject credits;

    [SerializeField] private AudioSource audioSourceOnStart;

    private bool onMainMenu;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ResetMenu();
    }

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
                audioSourceOnStart.Play();
            }
        }
    }

    private void TurnOffAnimator()
    {
        animator.enabled = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(normalSceneName);
    }

    public void EndlessMode()
    {
        SceneManager.LoadScene(endlessSceneName);
    }

    public void Options()
    {
        ResetMenu();
        options.SetActive(true);
    }

    public void Credits()
    {
        ResetMenu();
        credits.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void ResetMenu()
    {
        options.SetActive(false);
        credits.SetActive(false);
    }
}
