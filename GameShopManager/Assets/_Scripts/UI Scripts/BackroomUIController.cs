using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackroomUIController : MonoBehaviour
{
    public static BackroomUIController Instance;

    [Header("UI References")]
    public GameObject panel;
    public Transform gridParent;
    public GameObject slotPrefab;

    private Shelf targetShelf;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void OpenBackroom(Shelf shelf)
    {
        targetShelf = shelf;
        panel.SetActive(true);
        RefreshUI();
    }

    public void CloseBackroom()
    {
        panel.SetActive(false);
        targetShelf = null;
    }

    void RefreshUI()
    {
        foreach (Transform child in gridParent) Destroy(child.gameObject);

        var snapshot = InventoryManager.Instance.GetInventorySnapshot();
        foreach (var entry in snapshot)
        {
            GameItem item = entry.Key;
            int count = entry.Value;

            GameObject slot = Instantiate(slotPrefab, gridParent);
            slot.transform.Find("ItemImage").GetComponent<Image>().sprite = item.coverArt;
            slot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = $"{item.itemName} x{count}";

            Button stockBtn = slot.transform.Find("StockButton").GetComponent<Button>();
            stockBtn.onClick.AddListener(() =>
            {
                if (targetShelf.StockItem(item))
                {
                    RefreshUI();
                    ShelfUIController.Instance.RefreshUI();
                }
            });
        }
    }
}

