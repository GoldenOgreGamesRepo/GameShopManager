using System.Collections.Generic;
using UnityEngine;

public class CheckoutSystem : MonoBehaviour
{
    public static CheckoutSystem Instance;

    private Queue<(CustomerAI, GameItem)> checkoutQueue = new Queue<(CustomerAI, GameItem)>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddCustomer(CustomerAI customer, GameItem item)
    {
        checkoutQueue.Enqueue((customer, item));
    }

    public bool HasCustomer()
    {
        return checkoutQueue.Count > 0;
    }

    public (CustomerAI, GameItem) PeekNextCustomer()
    {
        if (checkoutQueue.Count > 0)
            return checkoutQueue.Peek();
        return (null, null);
    }

    public void ProcessNextCustomer()
    {
        if (checkoutQueue.Count > 0)
        {
            var (customer, item) = checkoutQueue.Dequeue();
            GameManager.Instance.AddMoney(item.salePrice);
            customer.FinishCheckout();
        }
    }
}
