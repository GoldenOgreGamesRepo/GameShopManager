using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Day Settings")]
    public float dayLength = 120f; // seconds
    private float timer;
    public bool storeOpen = false;

    [Header("UI")]
    public TextMeshProUGUI dayTimerText;
    public TextMeshProUGUI moneyText;

    [Header("Money")]
    public int money = 100;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartDay();
    }

    void Update()
    {
        if (!storeOpen) return;

        timer -= Time.deltaTime;
        UpdateTimerUI();

        if (timer <= 0)
        {
            EndDay();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        dayTimerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void StartDay()
    {
        timer = dayLength;
        storeOpen = true;
        Debug.Log("Store is now OPEN!");
        InventoryManager.Instance.StockShelvesAtStart();
    }

    public void EndDay()
    {
        storeOpen = false;
        Debug.Log("Store is now CLOSED!");
        CustomerSpawner[] spawners = FindObjectsByType<CustomerSpawner>(FindObjectsSortMode.None);
        foreach (var spawner in spawners)
        {
            spawner.StopSpawning();
        }

        // Open End Day UI
        EndDayUIController.Instance.OpenEndDayPanel();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log("Money: " + money);
    }

    public bool IsStoreOpen()
    {
        return storeOpen;
    }
}
