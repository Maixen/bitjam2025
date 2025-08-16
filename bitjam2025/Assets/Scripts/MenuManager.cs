using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private float snapRangeForVolumeSliders;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

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

    public void ChangeAudioMusic(float value)
    {
        value = ReturnSnappedValue(value);
        musicSlider.value = value;
        audioMixer.SetFloat("musicVol", value);
    }

    public void ChangeAudioSFX(float value)
    {
        value = ReturnSnappedValue(value);
        sfxSlider.value = value;
        audioMixer.SetFloat("sfxVol", value);
    }

    private float ReturnSnappedValue(float value)
    {
        if ((value + snapRangeForVolumeSliders >= 0 && value < 0)
         || (value - snapRangeForVolumeSliders <= 0 && value > 0))
        {
            return 0;
        }
        return value;
    }
}
