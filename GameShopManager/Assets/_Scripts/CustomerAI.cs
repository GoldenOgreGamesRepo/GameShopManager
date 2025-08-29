using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    public Shelf targetShelf;
    public Transform counterPoint;
    public float moveSpeed = 2f;

    private enum State { Entering, Browsing, GoingToCounter, WaitingCheckout, Leaving }
    private State currentState;

    private GameItem selectedItem;
    private Vector3 targetPos;

    void Start()
    {
        currentState = State.Entering;
        Debug.Log("Shelf item count at start: " + targetShelf.GetItemCount());
        if (targetShelf != null)
            targetPos = targetShelf.transform.position;
        else
        {
            Debug.LogWarning($"{name}: No target shelf assigned, leaving immediately.");
            currentState = State.Leaving;
            targetPos = new Vector3(-10, 0, 0);
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Entering:
                MoveTo(targetPos, () => currentState = State.Browsing);
                break;

            case State.Browsing:
                GrabItem();
                break;

            case State.GoingToCounter:
                if (counterPoint != null)
                {
                    MoveTo(counterPoint.position, () =>
                    {
                        if (CheckoutSystem.Instance != null && selectedItem != null)
                        {
                            CheckoutSystem.Instance.AddCustomer(this, selectedItem);
                        }
                        else
                        {
                            Debug.LogWarning($"{name}: CheckoutSystem or item missing.");
                        }
                        currentState = State.WaitingCheckout;
                    });
                }
                else
                {
                    Debug.LogWarning($"{name}: No counter assigned, leaving store.");
                    currentState = State.Leaving;
                }
                break;

            case State.WaitingCheckout:
                // wait until CheckoutSystem calls FinishCheckout()
                break;

            case State.Leaving:
                MoveTo(new Vector3(-10, 0, 0), () => Destroy(gameObject));
                break;
        }
    }

    void MoveTo(Vector3 destination, System.Action onArrive)
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            onArrive?.Invoke();
        }
    }

    void GrabItem()
    {
        if (targetShelf != null && targetShelf.GetItemCount() > 0)
        {
            selectedItem = targetShelf.TakeItem();
        }

        if (selectedItem != null)
            currentState = State.GoingToCounter;
        else
        {
            Debug.Log($"{name}: Could not grab item, leaving store.");
            currentState = State.Leaving;
        }
    }

    public void FinishCheckout()
    {
        currentState = State.Leaving;
    }

    public GameItem GetItem() => selectedItem;
    public bool HasItem() => selectedItem != null;
}
