using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [Header("Prefabs & Spawn Points")]
    public GameObject customerPrefab;
    public Transform[] spawnPoints;
    public Shelf[] shelves;
    public Transform counterPoint;

    [Header("Spawn Timing")]
    public float minSpawnTime = 3f;
    public float maxSpawnTime = 8f;

    private float nextSpawnTime;
    private bool canSpawn = true;

    void Start()
    {
        ScheduleNextSpawn();
    }

    void Update()
    {
        if (!canSpawn || !GameManager.Instance.IsStoreOpen()) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnCustomer();
            ScheduleNextSpawn();
        }
    }

    private void ScheduleNextSpawn()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }

    private void SpawnCustomer()
    {
        if (customerPrefab == null)
        {
            Debug.LogWarning("CustomerSpawner: No customer prefab assigned!");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("CustomerSpawner: No spawn points assigned!");
            return;
        }

        if (shelves == null || shelves.Length == 0)
        {
            Debug.LogWarning("CustomerSpawner: No shelves assigned!");
            return;
        }

        if (counterPoint == null)
        {
            Debug.LogWarning("CustomerSpawner: No counter point assigned!");
            return;
        }

        // Pick random spawn point and shelf
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Shelf targetShelf = shelves[Random.Range(0, shelves.Length)];

        // Instantiate customer
        GameObject customerGO = Instantiate(customerPrefab, spawn.position, Quaternion.identity);
        CustomerAI ai = customerGO.GetComponent<CustomerAI>();
        if (ai != null)
        {
            ai.targetShelf = targetShelf;
            ai.counterPoint = counterPoint;
        }
        else
        {
            Debug.LogWarning("Customer prefab missing CustomerAI component!");
        }
    }
}
