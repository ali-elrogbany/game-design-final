using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObjectController : MonoBehaviour
{
    private float moveSpeed = 10f;

    void Update()
    {
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
}
