using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltAnimation : MonoBehaviour
{
    public static ConveyorBeltAnimation instance;
    private bool moving;
    [SerializeField] private float repeatAfter;
    private float speed;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartBelt(1f);
    }

    private void Update()
    {
        if (!moving) { return; }
        if ((speed > 0 && transform.position.x >= repeatAfter) || (speed < 0 && transform.position.x <= repeatAfter))
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }

        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    public void StartBelt(float speedSet)
    {
        moving = true;
        speed = speedSet;
    }

    public void StopBelt()
    {
        moving = false;
    }
}
