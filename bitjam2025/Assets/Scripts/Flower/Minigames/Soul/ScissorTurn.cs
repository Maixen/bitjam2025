using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorTurn : MonoBehaviour
{
    [SerializeField] private float turnDegreesAmountPerClick;
    [SerializeField] private float turnOffsetDegrees;


    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, turnOffsetDegrees);
    }
    private void Update()
    {
        int turnDir = 0;
        if(Input.GetMouseButtonDown(0))
            turnDir = -1;
        if(Input.GetMouseButtonDown(1))
            turnDir = 1;
        if (turnDir == 0)
            return;
        transform.rotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z + (turnDegreesAmountPerClick * turnDir));
    }
}
