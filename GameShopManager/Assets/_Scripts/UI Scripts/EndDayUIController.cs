using UnityEngine;
using TMPro;

public class EndDayUIController : MonoBehaviour
{
    public static EndDayUIController Instance;

    [Header("UI Elements")]
    public GameObject panel;
    public TextMeshProUGUI moneyText;

    [Header("Hire Buttons")]
    public GameObject hireStockClerkButton;
    public GameObject hireCashierButton;
    public GameObject hireManagerButton;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void OpenEndDayPanel()
    {
        panel.SetActive(true);
        UpdateUI();
    }

    public void CloseEndDayPanel()
    {
        panel.SetActive(false);
    }

    void UpdateUI()
    {
        moneyText.text = "Money: $" + GameManager.Instance.money;

        // Hide hire buttons if already hired
        hireStockClerkButton.SetActive(!WorkerManager.Instance.hasStockClerk);
        hireCashierButton.SetActive(!WorkerManager.Instance.hasCashier);

        // Manager unlocks only if clerk + cashier are hired
        hireManagerButton.SetActive(
            WorkerManager.Instance.hasStockClerk && WorkerManager.Instance.hasCashier
        );
    }

    public void OnHireStockClerk()
    {
        if (GameManager.Instance.money >= 500)
        {
            GameManager.Instance.money -= 500;
            WorkerManager.Instance.HireStockClerk();
            UpdateUI();
        }
    }

    public void OnHireCashier()
    {
        if (GameManager.Instance.money >= 1000)
        {
            GameManager.Instance.money -= 1000;
            WorkerManager.Instance.HireCashier();
            UpdateUI();
        }
    }

    public void OnHireManager()
    {
        if (GameManager.Instance.money >= 5000)
        {
            GameManager.Instance.money -= 5000;
            Debug.Log("Manager hired! Second store unlocked.");
            // TODO: Load second store scene or expand current shop
            UpdateUI();
        }
    }
}
