using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 1);
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
        }
    }
}
