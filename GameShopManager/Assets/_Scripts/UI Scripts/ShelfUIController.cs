using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShelfUIController : MonoBehaviour
{
    public static ShelfUIController Instance;

    [Header("UI References")]
    public GameObject panel;
    public Transform gridParent;          // parent transform for item slots
    public GameObject slotPrefab;         // prefab with Image + Text

    private Shelf currentShelf;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void OpenShelfUI(Shelf shelf)
    {
        currentShelf = shelf;
        panel.SetActive(true);
        RefreshUI();
    }

    public void CloseShelfUI()
    {
        panel.SetActive(false);
        currentShelf = null;
    }

    public void RefreshUI()
    {
        foreach (Transform child in gridParent) Destroy(child.gameObject);

        if (currentShelf == null) return;

        List<GameItem> items = currentShelf.GetItems();
        foreach (GameItem item in items)
        {
            GameObject slot = Instantiate(slotPrefab, gridParent);
            slot.transform.Find("ItemImage").GetComponent<Image>().sprite = item.coverArt;
            slot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.itemName;
        }
    }

    // Called from button: open backroom inventory
    public void OpenBackroomUI()
    {
        if (currentShelf != null)
            BackroomUIController.Instance.OpenBackroom(currentShelf);
    }
}
