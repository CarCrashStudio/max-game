using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool Entered { get; }
    bool Active { get; }
    void OnInteractableEnter(IInteracter interacter);
    void OnInteractableExit(IInteracter interacter);

    void Interact(GameObject interacter);
}
