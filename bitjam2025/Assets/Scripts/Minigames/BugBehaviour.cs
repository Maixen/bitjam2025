using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBehaviour : MonoBehaviour
{
    public ParasiteMinigame minigameParent;

    public void GotClicked()
    {
        minigameParent.BugWasSquashed();
        Destroy(gameObject);
    }
}
