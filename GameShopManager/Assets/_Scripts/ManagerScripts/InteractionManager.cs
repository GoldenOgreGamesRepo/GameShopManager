using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public Camera mainCameral;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCameral.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
               // Look for interactable components
               var shelf = hit.collider.GetComponent<Shelf>();
                if(shelf != null)
                {
                    ShelfUIController.Instance.OpenShelfUI(shelf);
                    return;
                }
                var customer = hit.collider.GetComponent<CustomerAI>();
                if(customer != null)
                {
                    if (customer.HasItem())
                    {
                        CheckoutUIController.Instance.OpenCheckout(customer, customer.GetItem());
                    }
                }
            }
        }
    }
}
