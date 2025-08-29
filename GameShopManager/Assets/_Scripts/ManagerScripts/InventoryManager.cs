using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private Dictionary<GameItem, int> backroomInventory = new Dictionary<GameItem, int>();
    [Header("Starting Items")]
    public GameItem[] startingItems;
    public int[] startingQuantities; // must match startingItems length

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < startingItems.Length; i++)
        {
            AddToBackroom(startingItems[i], startingQuantities[i]);
        }
    }
    // Fixed AddToBackroom
    public bool AddToBackroom(GameItem item, int amount)
    {
        if (backroomInventory.ContainsKey(item))
        {
            backroomInventory[item] += amount;
        }
        else
        {
            backroomInventory[item] = amount;
        }
        return true;
    }

    public bool RemoveFromBackroom(GameItem item, int amount)
    {
        if (backroomInventory.ContainsKey(item) && backroomInventory[item] >= amount)
        {
            backroomInventory[item] -= amount;
            return true;
        }
        return false;
    }

    public int GetBackroomCount(GameItem item)
    {
        if (backroomInventory.ContainsKey(item))
            return backroomInventory[item];
        return 0;
    }

    public Dictionary<GameItem, int> GetInventorySnapshot()
    {
        return new Dictionary<GameItem, int>(backroomInventory);
    }

    // =============================
    // Auto-stock all shelves at day start
    // =============================
    public void StockShelvesAtStart()
    {
        Shelf[] shelves = FindObjectsByType<Shelf>(FindObjectsSortMode.None);

        foreach (Shelf shelf in shelves)
        {
            while (shelf.GetItemCount() < shelf.capacity)
            {
                bool stockedSomething = false;

                foreach (var kvp in GetInventorySnapshot())
                {
                    GameItem item = kvp.Key;
                    int quantity = kvp.Value;

                    if (quantity > 0)
                    {
                        if (RemoveFromBackroom(item, 1))
                        {
                            shelf.StockItem(item);
                            stockedSomething = true;
                            break; // move to next shelf slot
                        }
                    }
                }

                // If nothing left in backroom, stop trying
                if (!stockedSomething) break;
            }
        }
    }
}
