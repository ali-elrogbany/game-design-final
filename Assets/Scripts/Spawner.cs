using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    [Header("Spawner Settings")]
    [SerializeField] private float minSpawnRate = 1f;
    [SerializeField] private float maxSpawnRate = 3f;
    [SerializeField] private float spawnChanceThreshold = 0.5f;

    [Header("Spawned Object Settings")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("Spawnable Objects")]
    [SerializeField] private List<GameObject> spawnableObstacles;

    [Header("Spawn Markers")]
    [SerializeField]private Transform[] markers;

    [Header("Coroutines")]
    private IEnumerator spawningCoroutine;

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
        spawningCoroutine = SpawnObstacles();
        StartCoroutine(spawningCoroutine);
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            if (spawnableObstacles.Count > 0)
            {
                foreach (Transform marker in markers)
                {
                    GameObject randomObstacle = spawnableObstacles[Random.Range(0, spawnableObstacles.Count)];

                    if (Random.value > spawnChanceThreshold)
                    {
                        GameObject instantiatedObstacle = Instantiate(randomObstacle, marker.position, Quaternion.identity);
                        instantiatedObstacle.GetComponent<SpawnableObjectController>()?.SetMoveSpeed(moveSpeed);
                    }
                }
            }

            float waitTime = Random.Range(minSpawnRate, maxSpawnRate);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void StopSpawining()
    {
        StopCoroutine(spawningCoroutine);
    }
}
