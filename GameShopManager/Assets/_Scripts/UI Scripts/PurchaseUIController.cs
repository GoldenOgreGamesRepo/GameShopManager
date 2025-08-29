using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseUIController : MonoBehaviour
{
    public static PurchaseUIController Instance;

    [Header("UI References")]
    public GameObject panel;
    public Transform gridParent;
    public GameObject slotPrefab;
    public TextMeshProUGUI moneyText;

    public GameItem[] allAvailableItems;  // assign in Inspector

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void OpenPurchasePanel()
    {
        panel.SetActive(true);
        RefreshUI();
    }

    public void ClosePurchasePanel()
    {
        panel.SetActive(false);
    }

    void RefreshUI()
    {
        foreach (Transform child in gridParent) Destroy(child.gameObject);

        moneyText.text = $"Money: ${GameManager.Instance.money}";

        foreach (GameItem item in allAvailableItems)
        {
            GameObject slot = Instantiate(slotPrefab, gridParent);
            slot.transform.Find("ItemImage").GetComponent<Image>().sprite = item.coverArt;
            slot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = $"{item.itemName} - ${item.purchaseCost}";

            Button buyButton = slot.transform.Find("BuyButton").GetComponent<Button>();
            buyButton.onClick.AddListener(() =>
            {
                if (GameManager.Instance.money >= item.purchaseCost)
                {
                    GameManager.Instance.money -= item.purchaseCost;
                    InventoryManager.Instance.AddToBackroom(item, 1);
                    RefreshUI();
                }
            });
        }
    }
}
