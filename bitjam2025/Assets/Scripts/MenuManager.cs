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

    [SerializeField] private GameObject iconStartGame;
    [SerializeField] private GameObject iconEndlessMode;
    [SerializeField] private GameObject iconOptioins;
    [SerializeField] private GameObject iconCredits;
    [SerializeField] private GameObject iconExit;
    [SerializeField] private GameObject iconColorChange;
    [SerializeField] private Sprite[] iconsForColorChange;

    [SerializeField] private AudioSource audioSourceOnStart;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private float snapRangeForVolumeSliders;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private bool onMainMenu;

    private bool resettable;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ResetMenu();
        ChangeAudioMusic(PlayerPrefs.GetFloat("Music",0));
        ChangeAudioSFX(PlayerPrefs.GetFloat("SFX",0));
        SetChangeColor(PlayerPrefs.GetInt("ChangeColor", 0));
        resettable = true;
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
        ResetMenu(true);
        ResetMenuHover();
        resettable = false;
        options.SetActive(true);
        CancelInvoke(nameof(BufferForReset));
        Invoke(nameof(BufferForReset), 2f);
    }

    public void Credits()
    {
        ResetMenu(true);
        ResetMenuHover();
        resettable = false;
        credits.SetActive(true);
        CancelInvoke(nameof(BufferForReset));
        Invoke(nameof(BufferForReset), 2f);
    }

    public void BufferForReset()
    {
        resettable = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public bool ResetMenu(bool force = false)
    {
        if (!force && !resettable) { return false; }
        options.SetActive(false);
        credits.SetActive(false);
        return true;
    }

    private void ResetMenuHover()
    {
        iconStartGame.SetActive(false);
        iconEndlessMode.SetActive(false);
        iconOptioins.SetActive(false);
        iconCredits.SetActive(false);
        iconExit.SetActive(false);
    }

    public void ChangeAudioMusic(float value)
    {
        value = ReturnSnappedValue(value);
        musicSlider.value = value;
        audioMixer.SetFloat("musicVol", value);
        PlayerPrefs.SetFloat("Music", value);
    }

    public void ChangeAudioSFX(float value)
    {
        value = ReturnSnappedValue(value);
        sfxSlider.value = value;
        audioMixer.SetFloat("sfxVol", value);
        PlayerPrefs.SetFloat("SFX", value);
    }

    public void SetChangeColor(int isAllowed)
    {
        PlayerPrefs.SetInt("ChangeColor", isAllowed);
        iconColorChange.GetComponent<Image>().sprite = iconsForColorChange[isAllowed];
    }

    public void ToggleChangeColor()
    {
        if (PlayerPrefs.GetInt("ChangeColor") == 0)
            SetChangeColor(1);
        else
            SetChangeColor(0);
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
