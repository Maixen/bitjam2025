using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMinigame : MonoBehaviour
{
    
    [SerializeField] private bool gameHasStarted;
    public void StartMinigame()
    {
        if (gameHasStarted)
            return;
        
    }
}
