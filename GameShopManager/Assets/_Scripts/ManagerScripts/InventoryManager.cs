using UnityEngine;
using System.Collections.Generic;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private Dictionary<GameItem, int> backroomInventory = new Dictionary<GameItem, int>();

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(Instance);
    }
    /// <summary>
    /// Add items to the back room.
    /// </summary>
    /// <param name="item">Item</param>
    /// <param name="amount">Quantity</param>
    /// <returns></returns>
    public bool AddToBackroom(GameItem item, int amount)
    {
        if (!backroomInventory.ContainsKey(item) && backroomInventory[item] >= amount)
        {
            backroomInventory[item] -= amount;
            return true;
        }
        return false;
    }
    public bool RemoveFromBackroom(GameItem item, int amount)
    {
        if(backroomInventory.ContainsKey(item) && backroomInventory[item] >= amount)
        {
            backroomInventory[item] -= amount;
            return true;
        }
        return false;
    }
    int GetBackroomCount(GameItem item)
    {
        if (backroomInventory.ContainsKey(item))
        {
            return backroomInventory[item];
        }
        return 0;
    }
    public Dictionary<GameItem, int> GetInventorySnapshot()
    {
        return new Dictionary<GameItem, int>(backroomInventory);
    }

}
