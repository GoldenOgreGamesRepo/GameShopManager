using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    public static WorkerManager Instance;

    [Header("Workers")]
    public bool hasStockClerk = false;
    public bool hasCashier = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void HireStockClerk()
    {
        hasStockClerk = true;
        Debug.Log("Stock Clerk hired!");
    }

    public void HireCashier()
    {
        hasCashier = true;
        Debug.Log("Cashier hired!");
    }
}
