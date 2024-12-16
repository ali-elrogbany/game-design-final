using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnableObjectController : MonoBehaviour
{
    public float yOffset;

    private float moveSpeed = 10f;
    private bool canMove = true;

    void Update()
    {
        if (!canMove)
            return;
        
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveSpeed * Time.deltaTime);

        if (transform.position.z < -10f)
        {
            Destroy(gameObject);
        }
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void DisableCanMove()
    {
        canMove = false;
    }

    public abstract void OnCollision();
}
