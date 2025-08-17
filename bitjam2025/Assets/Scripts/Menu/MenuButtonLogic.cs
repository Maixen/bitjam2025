using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuButtonLogic : MonoBehaviour
{
    [SerializeField] private Vector2 normalPos;
    [SerializeField] private float targetXOffset;

    [SerializeField] private GameObject ownIcon;

    [SerializeField] private string functionName;

    [SerializeField] private AudioSource audioSource;


    private Vector2 targetPosition;
    public float speed;

    private Vector2 extraPos;

    private void Start()
    {
        extraPos = normalPos + Vector2.right * targetXOffset;
        targetPosition = normalPos;
    }

    private void OnMouseEnter()
    {
        audioSource.Play();
        targetPosition = extraPos;
        if(MenuManager.instance.ResetMenu())
        ownIcon.SetActive(true);
    }

    private void OnMouseExit()
    {
        targetPosition = normalPos;
        ownIcon.SetActive(false);
    }

    private void OnMouseDown()
    {
        MenuManager.instance.Invoke(functionName, 0f);
    }

    void Update()
    {
        transform.position = SpringMove(transform.position, targetPosition, speed, Time.deltaTime);
    }

    Vector2 SpringMove(Vector2 current, Vector2 target, float speed, float deltaTime)
    {
        return Vector2.Lerp(current, target, speed * deltaTime);
    }
}
