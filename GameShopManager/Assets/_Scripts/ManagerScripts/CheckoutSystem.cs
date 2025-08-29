using UnityEngine;
using System.Collections.Generic;
public class CheckoutSystem : MonoBehaviour
{
    public static CheckoutSystem Instance;

    private Queue<(CustomerAI, GameItem)> checkoutQueue = new Queue<(CustomerAI, GameItem)>();
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (checkoutQueue.Count > 0 && Input.GetMouseButtonDown(0)) // Left click to checkout
        {
            var (customer, item) = checkoutQueue.Dequeue();
            GameManager.Instance.AddMoney(item.salePrice);
            customer.FinishCheckout();
        }
    }
    public void AddCustomer(CustomerAI customer ,GameItem item)
    {
        checkoutQueue.Enqueue((customer, item));
    }

}
