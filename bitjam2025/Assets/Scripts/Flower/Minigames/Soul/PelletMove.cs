using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletMove : MonoBehaviour
{
    public SoulMinigame parentMinigame;
    public Vector2 goalPos;
    public float pelletSpeed;
    [SerializeField] private float checkBeginRadius;
    [SerializeField] private LayerMask scissorMask;
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, goalPos, Time.deltaTime * pelletSpeed);
        float distanceFromGoal = ((Vector2)transform.position - goalPos).magnitude;
        if (distanceFromGoal == 0)
        {
            parentMinigame.PelletWasMissed();
            Destroy(gameObject);
        }
        if (distanceFromGoal > checkBeginRadius)
            return;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, scissorMask);
        if (hit.collider == null)
            return;
        if (!hit.collider.CompareTag("Scissor"))
            return;
        parentMinigame.PelletWasCaught();
        Destroy(gameObject);   
    }

    public void SetupPellet(SoulMinigame parent, Vector2 goal, float speed)
    {
        parentMinigame = parent;
        goalPos = goal;
        pelletSpeed = speed;
    }
}
