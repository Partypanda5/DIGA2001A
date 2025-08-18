using UnityEngine;

public class Mover : MonoBehaviour
{
    public float moveDistance = 2f;
    public float moveSpeed = 2f;
    private bool isMoving = false;
    private Vector3 targetPos;

    public void TriggerMoveUp()
    {
        if (isMoving) return;
        targetPos = transform.position + Vector3.up * moveDistance;
        isMoving = true;
    }

    private void Update()
    {
        if (!isMoving) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) <= 0.001f)
        {
            isMoving = false;
        }
    }
}
