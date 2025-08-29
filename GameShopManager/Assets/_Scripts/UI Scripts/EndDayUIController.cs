using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndDayUIController : MonoBehaviour
{
    public static EndDayUIController Instance;

    [Header("UI References")]
    public GameObject panel;
    public TextMeshProUGUI moneyEarnedText;
    public Transform stockGridParent;
    public GameObject slotPrefab;
    public Button buyStockButton;
    public Button nextDayButton;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void OpenSummary(int moneyEarned)
    {
        panel.SetActive(true);
        moneyEarnedText.text = $"Money Earned Today: ${moneyEarned}";
        RefreshStockSummary();
    }

    void RefreshStockSummary()
    {
        foreach (Transform child in stockGridParent) Destroy(child.gameObject);

        var snapshot = InventoryManager.Instance.GetInventorySnapshot();
        foreach (var entry in snapshot)
        {
            GameItem item = entry.Key;
            int count = entry.Value;

            GameObject slot = Instantiate(slotPrefab, stockGridParent);
            slot.transform.Find("ItemImage").GetComponent<Image>().sprite = item.coverArt;
            slot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = $"{item.itemName} x{count}";
        }
    }

    public void CloseSummary()
    {
        panel.SetActive(false);
    }
}
