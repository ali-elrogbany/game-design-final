using System.Collections.Generic;
using UnityEngine;

public class FloorMovement : MonoBehaviour
{
    public static FloorMovement instance;

    public GameObject floorSegmentPrefab;
    public int numberOfSegments = 45;
    public float segmentLength = 1.0f;
    public float movementSpeed = 5.0f;
    public Transform player;

    private Queue<GameObject> floorSegments = new Queue<GameObject>();
    private float lastSegmentZPosition;
    private bool isMoving = true; 

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void Start()
    {
        InitializeFloor();
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveFloorSegments();
        }
    }

    private void InitializeFloor()
    {
        for (int i = 0; i < numberOfSegments; i++)
        {
            Vector3 position = new Vector3(0, 0, i * segmentLength);
            GameObject segment = Instantiate(floorSegmentPrefab, position, Quaternion.identity, transform);
            floorSegments.Enqueue(segment);

            lastSegmentZPosition = position.z;
        }
    }

    private void MoveFloorSegments()
    {
        int count = floorSegments.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject segment = floorSegments.Dequeue(); // Get the next segment
            segment.transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);

            if (segment.transform.position.z < player.position.z - segmentLength)
            {
                Destroy(segment);
                InstantiateNewSegment();
            }
            else
            {
                floorSegments.Enqueue(segment);
            }
        }
    }

    private void InstantiateNewSegment()
    {
        Vector3 newPosition = new Vector3(0, 0, lastSegmentZPosition);
        GameObject newSegment = Instantiate(floorSegmentPrefab, newPosition, Quaternion.identity, transform);

        floorSegments.Enqueue(newSegment);
    }

    public void StopMovement()
    {
        isMoving = false;
    }
}
