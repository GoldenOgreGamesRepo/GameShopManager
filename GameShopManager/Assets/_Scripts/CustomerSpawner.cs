using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject customerPrefab;
    public Transform spawnPoint;     // entrance location
    public Transform counterPoint;   // where customers go to checkout
    public Shelf[] shelves;          // assign shelves in Inspector

    [Header("Spawn Timing")]
    public float spawnIntervalMin = 5f;
    public float spawnIntervalMax = 10f;

    private float nextSpawnTime;
    public bool canSpawn = true;
    void Start()
    {
        ScheduleNextSpawn();
    }

    void Update()
    {
        if (!canSpawn) return;
        if (Time.time >= nextSpawnTime && GameManager.Instance.IsStoreOpen())
        {
            SpawnCustomer();
            ScheduleNextSpawn();
        }
    }

    void SpawnCustomer()
    {
        GameObject customerObj = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);

        CustomerAI customer = customerObj.GetComponent<CustomerAI>();
        customer.targetShelf = shelves[Random.Range(0, shelves.Length)];
        customer.counterPoint = counterPoint;
    }

    void ScheduleNextSpawn()
    {
        nextSpawnTime = Time.time + Random.Range(spawnIntervalMin, spawnIntervalMax);
    }
    public void StopSpawning()
    {
        canSpawn = false;
    }
}
