using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerMover : MonoBehaviour
{
    public static FlowerMover instance;

    public Vector2 positionAfterDrop;
    public Vector2 positionNormal;
    public Vector2 positionEnd;

    public void Normal()
    {

    }

    public void End(bool result)
    {

    }

    private IEnumerator MoveOverTime(Vector3 targetPosition, float duration)
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Fortschritt von 0 bis 1
            float t = elapsedTime / duration;
            // Smooth interpolation (SmootherStep oder SmoothStep wirkt natürlicher als nur Lerp)
            t = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(startPos, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // sicherstellen, dass das Ziel erreicht wird
    }
}
