using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckoutUIController : MonoBehaviour
{
    public static CheckoutUIController Instance;

    [Header("UI References")]
    public GameObject panel;
    public Image itemImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemPriceText;
    public Button sellButton;

    private CustomerAI currentCustomer;
    private GameItem currentItem;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void OpenCheckout(CustomerAI customer, GameItem item)
    {
        currentCustomer = customer;
        currentItem = item;

        itemImage.sprite = item.coverArt;
        itemNameText.text = item.itemName;
        itemPriceText.text = $"${item.salePrice}";

        panel.SetActive(true);

        sellButton.onClick.RemoveAllListeners();
        sellButton.onClick.AddListener(ProcessSale);
    }

    void ProcessSale()
    {
        GameManager.Instance.AddMoney(currentItem.salePrice);
        currentCustomer.FinishCheckout();
        CloseCheckout();
    }

    public void CloseCheckout()
    {
        panel.SetActive(false);
        currentCustomer = null;
        currentItem = null;
    }
}
