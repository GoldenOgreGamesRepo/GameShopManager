using UnityEngine;

public class Counter : MonoBehaviour
{
    void OnMouseDown()
    {
        if (CheckoutSystem.Instance.HasCustomer())
        {
            var (customer, item) = CheckoutSystem.Instance.PeekNextCustomer();
            if (customer != null)
                CheckoutUIController.Instance.OpenCheckout(customer, item);
        }
    }
}
