using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    private ParticleSystem.EmissionModule emission;
    private Vector2 previousMousePos;
    [SerializeField] private Vector2 cursorOffset;
    [SerializeField] private float trailActivateTreshhold;
    [SerializeField] private LayerMask menuMask;

    [SerializeField] private AudioSource button_audioSource;
    [SerializeField] private AudioSource checkbox_audioSource;
    [SerializeField] private AudioSource plant_audioSource;
    [SerializeField] private AudioSource plant_audioSource2;

    void Start()
    {
        Cursor.visible = false;
        emission = gameObject.GetComponentInChildren<ParticleSystem>().emission;
        previousMousePos = Vector2.zero;
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        if (mousePos.x < 0 || mousePos.x > Screen.width || mousePos.y < 0|| mousePos.y > Screen.height)
        {
            Cursor.visible = true;
            emission.enabled = false;
            return;
        }
        if((mousePos - previousMousePos).magnitude > trailActivateTreshhold)
            emission.enabled = true;
        else
            emission.enabled = false;
            previousMousePos = mousePos;
        Cursor.visible = false;
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(mousePos) + cursorOffset;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 1);
            //print(hit.collider.gameObject.name);
            if (hit.collider == null)
                return;
            if (GameManager.Instance == null)
                return;
            if (GameManager.Instance.gameIsDone)
            {
                if (hit.collider.CompareTag("Restart"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    return;
                }
                if (hit.collider.CompareTag("Menu"))
                {
                    SceneManager.LoadScene("Menu");
                    return;
                }
                return;
            }
            if (hit.collider.CompareTag("Checkbox"))
            {
                if (!GameManager.Instance.IsMinigamePlayed())
                {
                    checkbox_audioSource.Play();
                    hit.collider.gameObject.GetComponent<CheckBoxInteract>().WasClicked();
                }
                return;
            }
            if(hit.collider.CompareTag("Sell"))
            {
                if (!GameManager.Instance.IsMinigamePlayed())
                {
                    button_audioSource.Play();
                    FlowerCheck.instance.CheckFlower(true);
                }
                return;
            }
            if(hit.collider.CompareTag("Dump"))
            {
                if (!GameManager.Instance.IsMinigamePlayed())
                {
                    button_audioSource.Play();
                    FlowerCheck.instance.CheckFlower(false);
                }
                return;
            }
            if (hit.collider.CompareTag("MinigameAccess"))
            {
                print("Minigame Button pressed");
                if (!GameManager.Instance.IsMinigamePlayed())
                    hit.collider.gameObject.GetComponent<SetMinigameView>().ToggleMinigame();
                return;
            }
            if (hit.collider.CompareTag("WaterGame"))
            {
                print("WaterGame hit");
                if (!GameManager.Instance.IsMinigamePlayed())
                {
                    plant_audioSource2.Play();
                    hit.collider.gameObject.GetComponent<WaterMinigame>().StartMinigame();
                }
                return;
            }
            if (hit.collider.CompareTag("BugGame"))
            {
                print("BugGame hit");
                if (!GameManager.Instance.IsMinigamePlayed())
                {
                    plant_audioSource.Play();
                    hit.collider.gameObject.GetComponent<ParasiteMinigame>().StartMinigame();
                }
                return;
            }
            if (hit.collider.CompareTag("Bug"))
            {
                print("Bug hit");
                hit.collider.gameObject.GetComponent<BugBehaviour>().GotClicked();
                return;
            }
            if (hit.collider.CompareTag("SoulGame"))
            {
                print("SoulGame hit");
                if (!GameManager.Instance.IsMinigamePlayed())
                {
                    plant_audioSource2.Play();
                    hit.collider.gameObject.GetComponent<SoulMinigame>().StartMinigame();
                }
                return;
            }
        }
    }
}
