using UnityEngine;
using UnityEngine.AI;
public class CustomerAI : MonoBehaviour
{
    public Shelf targetShelf;
    public Transform counterPoint;
    private enum State {
        Entering,
        Browsing,
        GoingToCounter,
        WaitingCheckout,
        Leaving
    }
    private State currentState;
    private GameItem selectedItem;

    void Start()
    {
        currentState = State.Entering;
        ChooseShelf();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState) {
            case State.Entering:
                GoToShelf();
                break;
            case State.Browsing:
                GrabItem();
                break;
            case State.GoingToCounter:
                GoToCounter();
                break;
            case State.WaitingCheckout:
            // wait untiy CheckoutSystem processes this customer
            case State.Leaving:
                LeaveStore();
                break;
        }

    }
    void ChooseShelf()
    {
        currentState = State.Browsing;
    }
    void GoToShelf()
    {
        // move character TO DO:
        currentState = State.Browsing;
    }
    void GrabItem()
    {
        if (targetShelf != null && targetShelf.GetItemCount() > 0)
        {
            selectedItem = targetShelf.TakeItem();
        }
        currentState = State.GoingToCounter;
    }
    void GoToCounter()
    {
        transform.position = counterPoint.position;
        CheckoutSystem.Instance.AddCustomer(this, selectedItem);
        currentState = State.WaitingCheckout;
    }
    public void FinishCheckout()
    {
        currentState = State.Leaving;
    }
    void LeaveStore()
    {
        Destroy(gameObject);
    }
}
