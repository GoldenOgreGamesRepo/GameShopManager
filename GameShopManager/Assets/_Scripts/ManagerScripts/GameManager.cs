using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 100;
    public float dayLength = 120f; // seconds
    private float timer;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        timer = dayLength;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            EndDay();
        }
    }
    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log("Money: " + money);
    }
    void EndDay()
    {
        Debug.Log("Day Over! Total money: " + money);
        // TODO: SHow inventory & restocking UI 
    }
}
