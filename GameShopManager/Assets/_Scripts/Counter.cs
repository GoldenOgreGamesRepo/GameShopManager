using UnityEngine;
using UnityEngine.InputSystem;

public class Counter : MonoBehaviour
{
    public Transform[] counterPositions; // optional: multiple positions for queue
    private int nextPositionIndex = 0;

    // Called by InputManager when player clicks
    public void OnClick()
    {
        if (CheckoutSystem.Instance.HasCustomer())
        {
            var (customer, item) = CheckoutSystem.Instance.PeekNextCustomer();
            if (customer != null)
            {
                // Move customer to next free counter position if using queue spots
                if (counterPositions.Length > 0)
                {
                    customer.targetCounterPosition = counterPositions[nextPositionIndex];
                    nextPositionIndex = (nextPositionIndex + 1) % counterPositions.Length;
                }

                CheckoutSystem.Instance.ProcessNextCustomer();
            }
        }
        else
        {
            Debug.Log("No customers at the counter.");
        }
    }
}
