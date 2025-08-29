using UnityEngine;
using System.Collections.Generic;
public class Shelf : MonoBehaviour
{
    public int capacity = 5; 
    private List<GameItem> itemsOnShelf = new List<GameItem> ();

    public bool StockItem(GameItem item)
    {
        if (itemsOnShelf.Count >= capacity)
        {
            return false;
        }
        if(InventoryManager.Instance.RemoveFromBackroom(item, 1))
        {
            itemsOnShelf.Add(item);
            return true;
        }
        return false;
    }
    public GameItem TakeItem()
    {
        if (itemsOnShelf.Count > 0)
        {
            GameItem item = itemsOnShelf[0];
            itemsOnShelf.RemoveAt(0);
            return item;
        }
        return null;
    }
    public int GetItemCount()
    {
        return itemsOnShelf.Count;
    }
    public List<GameItem> GetItems()
    {
        return new List<GameItem> (itemsOnShelf);
    }
}
