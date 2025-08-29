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
        if (targetShelf != null) targetPos = targetShelf.transform.position;
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
                MoveTo(counterPoint.position, () =>
                {
                    CheckoutSystem.Instance.AddCustomer(this, selectedItem);
                    currentState = State.WaitingCheckout;
                });
                break;

            case State.WaitingCheckout:
                // just stand there until CheckoutSystem processes
                break;

            case State.Leaving:
                MoveTo(new Vector3(-10, 0, 0), () => Destroy(gameObject)); // walk out of store
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
        currentState = State.GoingToCounter;
    }

    public void FinishCheckout()
    {
        currentState = State.Leaving;
    }

    public GameItem GetItem() => selectedItem;
    public bool HasItem() => selectedItem != null;
}
