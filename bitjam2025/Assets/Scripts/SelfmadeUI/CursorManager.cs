using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private ParticleSystem.EmissionModule emission;
    private Vector2 previousMousePos;
    [SerializeField] private float trailActivateTreshhold;
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
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(mousePos);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 1);
            //print(hit.collider.gameObject.name);
            if (hit.collider == null)
                return;
            if(hit.collider.CompareTag("Checkbox"))
            {
                hit.collider.gameObject.GetComponent<CheckBoxInteract>().WasClicked();
                return;
            }
            if(hit.collider.CompareTag("Sell"))
            {
                FlowerCheck.instance.CheckFlower(true);
                return;
            }
            if(hit.collider.CompareTag("Dump"))
            {
                FlowerCheck.instance.CheckFlower(false);
                return;
            }
            if (hit.collider.CompareTag("MinigameAccess"))
            {
                print("WaterGame hit");
                hit.collider.gameObject.GetComponent<SetMinigameView>().ToggleMinigame();
                return;
            }
            if (hit.collider.CompareTag("WaterGame"))
            {
                print("WaterGame hit");
                hit.collider.gameObject.GetComponent<WaterMinigame>().StartMinigame();
                return;
            }
        }
    }
}
