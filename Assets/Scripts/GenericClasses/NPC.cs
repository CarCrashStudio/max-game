using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class NPC : MonoBehaviour, IInteractable
{
    bool entered = false;
    bool active = false;
    public bool Entered => entered;
    public bool Active => active;

    protected Rigidbody2D myRigidBody => gameObject.GetComponent<Rigidbody2D>();
    protected Animator animator => gameObject.GetComponent<Animator>();

    public abstract void Interact(GameObject interacter);

    public void OnInteractableEnter(IInteracter interacter)
    {
        Debug.Log(interacter);
        if (interacter == null) { return; }
        interacter.currentInteractable = this;

        entered = true;
    }
    public void OnInteractableExit(IInteracter interacter)
    {
        Debug.Log(interacter);
        if (interacter == null) { return; }

        entered = false;
        interacter.currentInteractable = null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // do something
        if (collision.CompareTag("Player"))
        {
            OnInteractableEnter(collision.GetComponent<IInteracter>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // do something
            OnInteractableExit(collision.GetComponent<IInteracter>());
        }
    }

    /* Indicate interaction is possible (speech bubble or something above player)
     * 
     * 
     * Derivitives
     * Depending on the NPC, different functions and UI will be needed
     */
}
