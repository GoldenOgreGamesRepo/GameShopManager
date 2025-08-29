using UnityEngine;

public class Cashier : MonoBehaviour
{
    public float checkoutInterval = 3f;
    private float nextCheckoutTime;

    void Update()
    {
        if (!WorkerManager.Instance.hasCashier) return;

        if (Time.time >= nextCheckoutTime && CheckoutSystem.Instance.HasCustomer())
        {
            CheckoutSystem.Instance.ProcessNextCustomer();
            nextCheckoutTime = Time.time + checkoutInterval;
        }
    }
}

