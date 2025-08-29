using UnityEngine;

public class StockClerk : MonoBehaviour
{
    public float restockInterval = 5f; // how often clerk checks shelves
    private float nextRestockTime;

    void Update()
    {
        if (!WorkerManager.Instance.hasStockClerk) return;

        if (Time.time >= nextRestockTime)
        {
            RestockShelves();
            nextRestockTime = Time.time + restockInterval;
        }
    }

    void RestockShelves()
    {
        foreach (Shelf shelf in FindObjectsByType<Shelf>(FindObjectsSortMode.None))
        {
            if (shelf.GetItemCount() < 3) // keep shelves filled
            {
                foreach (var kvp in InventoryManager.Instance.GetInventorySnapshot())
                {
                    GameItem gameItem = kvp.Key;
                    int quantity = kvp.Value;

                    if (quantity > 0)
                    {
                        // try to remove from backroom (updates inventory)
                        if (InventoryManager.Instance.RemoveFromBackroom(gameItem, 1))
                        {
                            shelf.StockItem(gameItem);
                            Debug.Log("Stock Clerk restocked " + gameItem.itemName);
                        }
                        break;
                    }
                }
            }
        }
    }

}
