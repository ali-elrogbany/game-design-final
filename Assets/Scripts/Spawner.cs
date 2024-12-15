using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    [Header("Spawner Settings")]
    [SerializeField] private float minObstacleSpawnRate = 1f;
    [SerializeField] private float maxObstacleRate = 3f;
    [SerializeField] private float minPowerupSpawnRate = 5f;
    [SerializeField] private float maxPowerupSpawnRate = 10f;
    [SerializeField] private float minCollectableSpawnRate = 3f;
    [SerializeField] private float maxCollectableSpawnRate = 5f;
    [SerializeField] private float spawnChanceThreshold = 0.5f;

    [Header("Spawned Object Settings")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("Spawnable Objects")]
    [SerializeField] private SpawnableObjectsSO obstacles;
    [SerializeField] private SpawnableObjectsSO collectables;
    [SerializeField] private SpawnableObjectsSO powerUps;

    [Header("Spawn Markers")]
    [SerializeField]private Transform[] markers;

    [Header("Coroutines")]
    private IEnumerator obstacleSpawningCoroutine;
    private IEnumerator powerupSpawningCoroutine;
    private IEnumerator collectableSpawningCoroutine;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        obstacleSpawningCoroutine = SpawnObstacles();
        powerupSpawningCoroutine = SpawnPowerups();
        collectableSpawningCoroutine = SpawnCollectable();

        StartCoroutine(obstacleSpawningCoroutine);
        StartCoroutine(powerupSpawningCoroutine);
        StartCoroutine(collectableSpawningCoroutine);
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            if (obstacles.prefabs.Count > 0)
            {
                foreach (Transform marker in markers)
                {
                    GameObject randomObstacle = obstacles.prefabs[Random.Range(0, obstacles.prefabs.Count)];

                    if (Random.value > spawnChanceThreshold)
                    {
                        if (!IsPositionOccupied(marker.position, randomObstacle))
                        {
                            GameObject instantiatedObstacle = Instantiate(randomObstacle, marker.position, Quaternion.identity);
                            instantiatedObstacle.GetComponent<SpawnableObjectController>()?.SetMoveSpeed(moveSpeed);
                            instantiatedObstacle.transform.position = new Vector3(instantiatedObstacle.transform.position.x, instantiatedObstacle.transform.position.y + instantiatedObstacle.GetComponent<SpawnableObjectController>().yOffset, instantiatedObstacle.transform.position.z);
                        }
                    }
                }
            }

            float waitTime = Random.Range(minObstacleSpawnRate, maxObstacleRate);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator SpawnPowerups()
    {
        while (true)
        {
            if (powerUps.prefabs.Count > 0)
            {
                foreach (Transform marker in markers)
                {
                    GameObject randomPowerup = powerUps.prefabs[Random.Range(0, powerUps.prefabs.Count)];

                    if (Random.value > spawnChanceThreshold)
                    {
                        if (!IsPositionOccupied(marker.position, randomPowerup))
                        {
                            GameObject instantiatedPowerup = Instantiate(randomPowerup, marker.position, Quaternion.identity);
                            instantiatedPowerup.GetComponent<SpawnableObjectController>()?.SetMoveSpeed(moveSpeed);
                            instantiatedPowerup.transform.position = new Vector3(instantiatedPowerup.transform.position.x, instantiatedPowerup.transform.position.y + instantiatedPowerup.GetComponent<SpawnableObjectController>().yOffset, instantiatedPowerup.transform.position.z);
                        }
                    }
                }
            }

            float waitTime = Random.Range(minPowerupSpawnRate, maxPowerupSpawnRate);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator SpawnCollectable()
    {
        while (true)
        {
            if (collectables.prefabs.Count > 0)
            {
                foreach (Transform marker in markers)
                {
                    GameObject randomCollectable = collectables.prefabs[Random.Range(0, collectables.prefabs.Count)];

                    if (Random.value > spawnChanceThreshold)
                    {
                        if (!IsPositionOccupied(marker.position, randomCollectable))
                        {
                            GameObject instantiatedCollectable = Instantiate(randomCollectable, marker.position, Quaternion.identity);
                            instantiatedCollectable.GetComponent<SpawnableObjectController>()?.SetMoveSpeed(moveSpeed);
                            instantiatedCollectable.transform.position = new Vector3(instantiatedCollectable.transform.position.x, instantiatedCollectable.transform.position.y + instantiatedCollectable.GetComponent<SpawnableObjectController>().yOffset, instantiatedCollectable.transform.position.z);
                        }
                    }
                }
            }

            float waitTime = Random.Range(minCollectableSpawnRate, maxCollectableSpawnRate);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void StopSpawining()
    {
        StopCoroutine(obstacleSpawningCoroutine);
        StopCoroutine(powerupSpawningCoroutine);
        StopCoroutine(collectableSpawningCoroutine);
    }

    private bool IsPositionOccupied(Vector3 position, GameObject prefab)
    {
        Collider[] colliders = Physics.OverlapBox(
            position, 
            prefab.GetComponent<Collider>().bounds.extents, 
            Quaternion.identity
        );

        if (colliders.Length > 0)
            Debug.Log("Position Occupied: " + position);

        return colliders.Length > 0;
    }
}
